using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoBoutique.API.Controllers
{
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        readonly IUserManagerService _apiUserManager;

        public AuthenticateController(IServiceProvider serviceProvider,
         IUserManagerService apiUserManager,
            IConfiguration configuration) 
        {
            _apiUserManager = apiUserManager;
        }

        [HttpGet]
        [Route("current-user-info")]
        [Authorize]
        public CurrentUser? CurrentUserInfo()
        {
            if (User.Identity != null)
            {
                bool isAuthenticated = User.Identity.IsAuthenticated;
                string? name = User.Identity.Name;
                return new CurrentUser
                {
                    IsAuthenticated = isAuthenticated,
                    UserName = name,
                    Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
                };
            }
            return null;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _apiUserManager.LogIn(request.Username, request.Password);
            if (user == null)
                return StatusCode((int)HttpStatusCode.Unauthorized, "Nom d'utilisateur ou mot de passe incorrect");
             

            var token = await _apiUserManager.GetToken(user);

            return Ok(token);
        }

     
    }
}
