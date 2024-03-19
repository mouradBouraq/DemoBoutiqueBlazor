using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DemoBoutique.BackOffice.Services
{
    public class HttpBaseService<T>
    {
        public const string AccessToken = nameof(AccessToken);

        public readonly HttpClient _httpClient;

        public readonly ProtectedLocalStorage _localStorage;
        protected JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNameCaseInsensitive = true
        };
        public HttpBaseService(HttpClient httpClient, ProtectedLocalStorage localStorage, IConfiguration configuration)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
      
        }

        public async Task AddHeaders()
        {
            try
            {
                if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                {
                    var accessToken = await _localStorage.GetAsync<string>(AccessToken);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception found: '{ex}'");
            }
        }


        protected async Task<HttpResponseMessage> Add(string uri, T model)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, model);
            return response;
        }

        protected async Task<HttpResponseMessage> Update(string uri, T model)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, model);
            return response;
        }

        protected async Task<HttpResponseMessage> Add<Y>(string uri, Y model)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, model);
            return response;
        }


        protected async Task<HttpResponseMessage> Edit(string uri, int id, T model)
        {

            return await _httpClient.PutAsJsonAsync($"{uri}{id}", model);
        }

        protected async Task<HttpResponseMessage> Edit<Y>(string uri, int id, Y model)
        {

            return await _httpClient.PutAsJsonAsync($"{uri}{id}", model);
        }


        protected async Task<HttpResponseMessage> EditList(string uri, int id, List<T> model)
        {

            return await _httpClient.PutAsJsonAsync($"{uri}{id}", model);
        }
        protected async Task<List<T>> All(string uri)
        {

            return await _httpClient.GetFromJsonAsync<List<T>>(uri, options) ?? new List<T>();
        }

        protected async Task<Y?> All<Y>(string uri) => await _httpClient.GetFromJsonAsync<Y>(uri, options);
        protected async Task<T?> GetById(string uri)
        {

            return await _httpClient.GetFromJsonAsync<T>(uri, options);
        }
        protected async Task<HttpResponseMessage> Delete(string uri)
        {

            return await _httpClient.DeleteAsync(uri);
        }
        protected async Task<T?> GetBy(string uri)
        {
            var respons = await _httpClient.GetFromJsonAsync<T?>(uri, options);
            return respons;
        }
    }
}
