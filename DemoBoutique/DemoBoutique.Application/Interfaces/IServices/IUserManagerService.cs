using DemoBoutique.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Interfaces.IServices
{
    public interface IUserManagerService
    {
       /// <summary>
        /// Renvoie le jeton OAuth pour un ticket utilisateur.
        /// </summary>
        /// <param name="ticket">Le ticket pour lequel fournir un jeton d'authentification.</param>
        /// <returns>Un jeton.</returns>
        Task<TokenResponse> GetToken(UserIdentity user);
        /// <summary>
        /// Indique si un jeton est valide.
        /// </summary>
        /// <param name="user">L'utilisateur censé correspondre au jeton.</param>
        /// <param name="token">Le jeton.</param>
        /// <param name="tokenPurpose">La destination du jeton.</param>
        /// <returns><c>true</c> si le jeton est valide, <c>false</c> sinon.</returns>
        Task<bool> ValidateToken(UserIdentity userIdentity, string tokenProvider, string purpose, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserIdentity> LogIn(string userName, string password);

        //Task<string> ResetPassword(string userName, string password, string oldPassword);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userIdentity"></param>
        ///// <param name="password"></param>
        //Task<string> Create(UserIdentity userIdentity, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> RoleExist(string roleName);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="roleName"></param>
        ///// <returns></returns>
        //Task<string> AddRoleToUser(UserIdentity user, string roleName);
        List<string> GetRoles();
        /// <summary>
        /// FindUserId
        /// </summary>
        /// <param name="userId">Current user Id.</param>
        int? FindUserId(string userId);
      
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> DeleteUser(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserIdentity> FindUserByEmail(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserIdentity> FindUserById(string id);

        Task<List<UserIdentity>> GetUsersInRoleAsync(string roleName);

        Task<TokenResponse> GetToken(List<Claim> claims, DateTime expireIn);
        Task<string?> Validate(string token, string claimType);
    }
}
