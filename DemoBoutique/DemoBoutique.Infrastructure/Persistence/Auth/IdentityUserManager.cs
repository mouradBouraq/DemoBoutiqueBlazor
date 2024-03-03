using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Infrastructure.Persistence.Auth
{
    public class IdentityUserManager : IIdentityUserManager
    {
        const int MAX_FAILED_PASSWORD_ATTEMPS = 5;
        public const int LOCKOUT_DURATION_IN_MINUTES = 30;
        readonly IConfiguration _configuration;
        readonly UserManager<IdentityUser> _userManager;

        readonly RoleManager<IdentityRole> _roleManager;

        public bool IsUserLockedOut { get; set; }
        public int LeftAttempts { get; set; }

        public IdentityUserManager(IConfiguration configuration, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Create(UserIdentity user, string password)
        {
            var identUser = new IdentityUser
            {
                UserName = user.Email,
                NormalizedUserName = user.Email,
                Email = user.Email,
                NormalizedEmail = user.Email
                //UtlisateurId = user.UtlisateurId
            };
            var result = await _userManager.CreateAsync(identUser, password);
            return result;
        }


        public async Task<IdentityResult> Update(UserIdentity user)
        {
            var userExist = await _userManager.FindByIdAsync(user.Id);
            if (userExist != null)
            {
                userExist.UserName = user.Email;
                userExist.NormalizedUserName = user.Email;
                userExist.Email = user.Email;
                userExist.NormalizedEmail = user.Email;
                var result = await _userManager.UpdateAsync(userExist);
               return result;
            }
            else
            {
                return null;
            }

        }
        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            return roleExist;
        }

        public List<string> GetRoles()
        {
            return _roleManager.Roles.Select(r => r.Name).ToList();
        }

       
        public async Task<bool> Delete(string id)
        {
            var identityUser = _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(identityUser.Result);
            return result.Succeeded;
        }

        public async Task<UserIdentity> Find(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && await this.CheckPasswordAsync(user, password))
            {
                var IsLockedOut = user.LockoutEnabled && user.LockoutEnd <= DateTime.UtcNow.AddMinutes(IdentityUserManager.LOCKOUT_DURATION_IN_MINUTES);
                return new UserIdentity(user.Id, user.UserName, IsLockedOut, user.AccessFailedCount);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check password async
        /// </summary>
        /// <param name="pwUser"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<bool> CheckPasswordAsync(IdentityUser pwUser, string password)
        {
            var accountLockedout = _userManager.IsLockedOutAsync(pwUser).Result;
            if (_userManager.SupportsUserLockout && accountLockedout)
            {
                this.LeftAttempts = 0;
                IsUserLockedOut = true;
                return Task.FromResult(false);
            }

            var passwordCheck = _userManager.CheckPasswordAsync(pwUser, password).Result;

            if (_userManager.SupportsUserLockout)
            {
                if (!passwordCheck)
                    this.AccessFailedAsync(pwUser);
                else
                {
                    // set unlocked
                    if (_userManager.IsLockedOutAsync(pwUser).Result)
                        _userManager.SetLockoutEnabledAsync(pwUser, false);

                    // reset counter
                    if (_userManager.GetAccessFailedCountAsync(pwUser).Result > 0)
                        _userManager.ResetAccessFailedCountAsync(pwUser);
                }
            }

            return Task.FromResult(passwordCheck);
        }

        /// <summary>
        /// Increments access failed attempts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IdentityResult> AccessFailedAsync(IdentityUser user)
        {
            var maxFailed = MAX_FAILED_PASSWORD_ATTEMPS - 1;
            if (!_userManager.SupportsUserLockout)
                return _userManager.AccessFailedAsync(user);

            var accessFailedCount = _userManager.GetAccessFailedCountAsync(user);
            if (accessFailedCount.Result >= maxFailed)
            {
                LeftAttempts = 0;
                IsUserLockedOut = true;
                _userManager.SetLockoutEnabledAsync(user, true);
                _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(LOCKOUT_DURATION_IN_MINUTES));
                return _userManager.AccessFailedAsync(user);
            }

            LeftAttempts = maxFailed - accessFailedCount.Result;

            return _userManager.AccessFailedAsync(user);
        }

        public async Task<TokenResponse> GetToken(UserIdentity user)
        {
            var identUser = new IdentityUser
            {
                Id = user.Id,
                UserName = user.Email,
                Email = user.Email
            };
            var userRoles = await _userManager.GetRolesAsync(identUser);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", user.Id),
                    new Claim("userName", user.Email),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenResponse
            {
                Expiration = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<TokenResponse> GetToken(List<Claim> authClaims, DateTime expireIn)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expireIn,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenResponse
            {
                Expiration = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<string?> ValidateToken(string jwtToken, string claimType)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Read the JWT token
            JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(jwtToken);

            // Access token properties
            string issuer = jwtSecurityToken.Issuer;
            string subject = jwtSecurityToken.Subject;
            DateTime? validFrom = jwtSecurityToken.ValidFrom;
            DateTime? validTo = jwtSecurityToken.ValidTo;
            var email = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == claimType);
            return email!.Value;
        }

        public Task<string> GenerateToken(UserIdentity userIdentity, string tokenProvider, string purpose)
        {
            var identUser = new IdentityUser
            {
                UserName = userIdentity.Email,
                //UtlisateurId = userIdentity.UtlisateurId
            };
            return _userManager.GenerateUserTokenAsync(identUser, tokenProvider, purpose);
        }


        public async Task<UserIdentity> FindByEmail(string userName)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            if (user != null)
            {
                var IsLockedOut = user.LockoutEnabled && user.LockoutEnd <= DateTime.UtcNow.AddMinutes(IdentityUserManager.LOCKOUT_DURATION_IN_MINUTES);
                return new UserIdentity(user.Id, user.UserName, IsLockedOut, user.AccessFailedCount);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserIdentity> FindById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var IsLockedOut = user.LockoutEnabled && user.LockoutEnd <= DateTime.UtcNow.AddMinutes(IdentityUserManager.LOCKOUT_DURATION_IN_MINUTES);
                return new UserIdentity(user.Id, user.UserName, IsLockedOut, user.AccessFailedCount);
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GeneratePasswordResetToken(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return string.Empty;

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }


        public async Task<List<UserIdentity>> GetUsersInRoleAsync(string roleName)
        {
            List<UserIdentity> result = new List<UserIdentity>();
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            if (users == null)
                return result;

            foreach (var user in users)
            {
                var IsLockedOut = user.LockoutEnabled && user.LockoutEnd <= DateTime.UtcNow.AddMinutes(IdentityUserManager.LOCKOUT_DURATION_IN_MINUTES);

                var currentUser = new UserIdentity(user.Id, user.UserName, IsLockedOut, user.AccessFailedCount);
                result.Add(currentUser);
            }

            return result;
        }




        public Task<bool> VerifyUserToken(UserIdentity userIdentity, string tokenProvider, string purpose, string token)
        {
            var identUser = new IdentityUser
            {
                Id = userIdentity.Id,
                UserName = userIdentity.Email,
                //UtlisateurId = userIdentity.UtlisateurId
            };
            return _userManager.VerifyUserTokenAsync(identUser, tokenProvider, purpose, token);
        }

        /// <summary>
        /// Read Identity Resources
        /// </summary>
        /// <param name="resName"></param>
        /// <returns></returns>
        public string ReadIdentityResources(string resName)
        {
            var t = typeof(PasswordValidator<IdentityUser>).Assembly.GetType("Microsoft.AspNetCore.Identity");
            var method = t?.GetProperty(resName, bindingAttr: BindingFlags.NonPublic | BindingFlags.Static);
            var valueDefinedByResx = method?.GetMethod?.Invoke(null, null);
            return valueDefinedByResx?.ToString();
        }
    }
}
