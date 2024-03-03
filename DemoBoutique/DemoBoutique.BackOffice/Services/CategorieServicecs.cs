using DemoBoutique.Domain.Categorie;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DemoBoutique.BackOffice.Services
{
    public class CategorieServicecs : HttpBaseService<Categorie>
    {
        public CategorieServicecs(HttpClient httpClient, ProtectedLocalStorage localStorage, IConfiguration configuration) : base(httpClient, localStorage, configuration)
        {
        }

        public async Task<HttpResponseMessage> Add(Categorie model)
        {
            var result = await Add($"api/Categorie/Add", model);
            return result;
        }

        public async Task<List<Categorie>> All()
        {
            var result = await All($"api/Categorie/All");
            return result;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await Delete($"api/Categorie/delete/{id}");
            return result;
        }
    }
}
