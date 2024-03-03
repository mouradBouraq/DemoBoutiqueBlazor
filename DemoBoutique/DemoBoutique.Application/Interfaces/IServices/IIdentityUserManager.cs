using DemoBoutique.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Interfaces.IServices
{
    // <summary>
    /// Gestionnaire d'authentification web.
    /// </summary>
    public interface IIdentityUserManager
    {
       
        /// <summary>
        /// Renvoie l'utilisateur correspondant à un login et mot de passe.
        /// </summary>
        /// <param name="userName">Le nom d'utilisateur.</param>
        /// <param name="password">Son mot de passe.</param>
        /// <returns><c>null</c> si <paramref name="userName"/> ou <paramref name="password"/> sont incorrects, l'utilisateur sinon.</returns>
        Task<UserIdentity> Find(string userName, string password);
        /// <summary>
        /// Renvoie l'utilisateur correspondant à un login.
        /// </summary>
        /// <param name="userName">Le nom d'utilisateur.</param>
        Task<UserIdentity> FindByEmail(string userName);

        Task<UserIdentity> FindById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> RoleExistsAsync(string roleName);


        /// <summary>
        /// Supprime un compte d'authentification.
        /// </summary>
        /// <param name="user">Le compte à supprimer.</param>
        /// <returns><c>true</c> si l'opération est réussie.</returns>
        Task<bool> Delete(string id);

        /// <summary>
        /// Génère un jeton de réinitialisation de mot de passe.
        /// </summary>
        /// <param name="userName">Le nom d'utilisateur concerné.</param>
        /// <returns>Un jeton de réinitialisation de mot de passe, ou <c>null</c> si le compte est introuvable.</returns>
        Task<string> GeneratePasswordResetToken(string userName);
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<TokenResponse> GetToken(UserIdentity user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="tokenProvider"></param>
        /// <param name="purpose"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> VerifyUserToken(UserIdentity userIdentity, string tokenProvider, string purpose, string token);

        string ReadIdentityResources(string resName);

        Task<string> GenerateToken(UserIdentity userIdentity, string tokenProvider, string purpose);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetRoles();

        Task<List<UserIdentity>> GetUsersInRoleAsync(string roleName);

        Task<TokenResponse> GetToken(List<Claim> authClaims, DateTime expireIn);
        Task<string?> ValidateToken(string token, string claimType);
    }
}
