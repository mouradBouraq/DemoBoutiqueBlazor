using DemoBoutique.Application.Interfaces;
using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace DemoBoutique.Application.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IIdentityUserManager _identityUserManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserManagerService(IIdentityUserManager identityUserManager, IUnitOfWork unitOfWork)
        {
            _identityUserManager = identityUserManager;
            _unitOfWork = unitOfWork;
        }

        //public async Task<string> CreateUserWithRole(UtilisateurModel model)
        //{
        //    _unitOfWork.BeginTransaction();
        //    // Test if the role exist
        //    var roleExit = await RoleExist(model.Role);
        //    if (roleExit)
        //    {
        //        //Test if the user already exist
        //        var userExist = await FindUserByEmail(model.Email);
        //        if (userExist != null)
        //        {
        //            return "L'utilisateur existe déjà";
        //        }

        //        var newUser = new UserIdentity(model.Email, model.NomUtilisateur);

        //        var result = await Create(newUser, model.Password);
        //        if (!string.IsNullOrEmpty(result))
        //        {
        //            _unitOfWork.Rollback();
        //            return result;
        //        }

        //        // add Role
        //        var roleAddedMessage = await AddRoleToUser(newUser, model.Role);
        //        if (!string.IsNullOrEmpty(roleAddedMessage))
        //        {
        //            _unitOfWork.Rollback();
        //            return roleAddedMessage;
        //        }

        //        _unitOfWork.Commit();
        //        var userCreated = await FindUserByEmail(model.Email);
        //        model.Id = userCreated.Id;
        //        return string.Empty;
        //    }
        //    return "Un problème est survenu, réessayer plus tard!";
        //}

       
        public int? FindUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            var pwUser = _identityUserManager.FindById(userId);
            return pwUser != null ? pwUser.Result.UtlisateurId : null;
        }

        public async Task<UserIdentity> FindUserByEmail(string email)
        {
            var pwUser = await _identityUserManager.FindByEmail(email);
            return pwUser;
        }

        public async Task<UserIdentity> FindUserById(string id)
        {
            var pwUser = await _identityUserManager.FindById(id);
            return pwUser;
        }

     
        public async Task<TokenResponse> GetToken(UserIdentity user)
        {
            return await _identityUserManager.GetToken(user);
        }

        public async Task<TokenResponse> GetToken(List<Claim> claims, DateTime expireIn)
        {
            return await _identityUserManager.GetToken(claims, expireIn);
        }

        public async Task<string?> Validate(string token, string claimType)
        {
            return await _identityUserManager.ValidateToken(token, claimType);
        }

        public async Task<UserIdentity> LogIn(string userName, string password)
        {
            return await _identityUserManager.Find(userName, password);
        }


        //public async Task<string> Create(UserIdentity userIdentity, string password)
        //{
        //    var result = await _identityUserManager.Create(userIdentity, password);
        //    if (result.Succeeded)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return result.FirstError;
        //    }
        //}

        //public async Task<string> Update(UserIdentity userIdentity)
        //{
        //    var result = await _identityUserManager.Update(userIdentity);
        //    if (result.Succeeded)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return result.FirstError;
        //    }
        //}

        public List<string> GetRoles()
        {
            return _identityUserManager.GetRoles();
        }

        public async Task<bool> RoleExist(string roleName)
        {
            return await _identityUserManager.RoleExistsAsync(roleName);
        }

        //public async Task<string> AddRoleToUser(UserIdentity user, string roleName)
        //{
        //    var roleExist = await RoleExist(roleName);
        //    if (roleExist)
        //    {
        //        var result = await _identityUserManager.AddRoleToUser(user.Email, roleName);
        //        if (result.Succeeded)
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return result.FirstError;
        //        }
        //    }
        //    else
        //    {
        //        return "Ce role n'exist pas!";
        //    }
        //}

        //public async Task<string> RemoveAllRoleFromUser(UserIdentity user, string roleName)
        //{
        //    var roleExist = await RoleExist(roleName);
        //    if (roleExist)
        //    {
        //        var result = await _identityUserManager.RemoveAllRoleFromUserByEmail(user.Email);
        //        if (result.Succeeded)
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return result.FirstError;
        //        }
        //    }
        //    else
        //    {
        //        return "Ce role n'exist pas!";
        //    }
        //}
        //public async Task<List<UtilisateurModel>> GetUsers()
        //{
        //    return await _identityUserManager.GetUserIdentities();
        //}

        public async Task<List<UserIdentity>> GetUsersInRoleAsync(string roleName)
        {
            return await _identityUserManager.GetUsersInRoleAsync(roleName);
        }

        public Task<bool> ValidateToken(UserIdentity userIdentity, string tokenProvider, string purpose, string token)
        {

            return _identityUserManager.VerifyUserToken(userIdentity, tokenProvider, purpose, token);
        }

        public Task<string> GenerateToken(UserIdentity userIdentity, string tokenProvider, string purpose, string token)
        {
            return _identityUserManager.GenerateToken(userIdentity, tokenProvider, purpose);
        }
    }
}
