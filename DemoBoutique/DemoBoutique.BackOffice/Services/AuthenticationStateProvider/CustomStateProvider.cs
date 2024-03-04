using DemoBoutique.Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;

namespace DemoBoutique.BackOffice.Services
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authServiceApi;
        private CurrentUser? CurrentUser;

        public CustomStateProvider(AuthService authServiceApi)
        {
            _authServiceApi = authServiceApi;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                CurrentUser = await GetCurrentUser();
                if (CurrentUser != null && CurrentUser.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, CurrentUser!.UserName) }.Concat(CurrentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException found: '{ex}'");

            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<CurrentUser> GetCurrentUser()
        {
            if (CurrentUser != null && CurrentUser.IsAuthenticated)
                return CurrentUser;

            CurrentUser = await _authServiceApi.CurrentUserInfo();
            return CurrentUser!;
        }
    }
}
