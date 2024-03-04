using DemoBoutique.Domain.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;

namespace DemoBoutique.BackOffice.Services
{
    public class AuthService : HttpBaseService<TokenResponse>
    {
        private const string RefreshToken = nameof(RefreshToken);
        private readonly ProtectedLocalStorage _localStorage;
        private readonly NavigationManager _navigation;
        public AuthService(HttpClient httpClient, ProtectedLocalStorage localStorage, NavigationManager navigation, IConfiguration configuration) : base(httpClient, localStorage, configuration)
        {
            _localStorage = localStorage;
            _navigation = navigation;

        }

        public async Task<HttpResponseMessage> LoginAsync(LoginRequest loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authenticate/Login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                await _localStorage.SetAsync(AccessToken, result?.Token);

            }
            return response;
        }
        public async Task<CurrentUser?> CurrentUserInfo()
        {
            ProtectedBrowserStorageResult<string> accessToken;
            try
            {
                await AddHeaders();
                var reponse = await _httpClient.GetFromJsonAsync<CurrentUser>("api/Authenticate/current-User-info");
                return reponse;
            }
            catch (CryptographicException)
            {
                await LogoutAsync();
                return null;
            }
        }


        public async Task LogoutAsync()
        {
            await RemoveAuthDataFromStorageAsync();
            _navigation.NavigateTo("/", true);
        }

        private async Task RemoveAuthDataFromStorageAsync()
        {
            await _localStorage.DeleteAsync(AccessToken);
        }

    }
}
