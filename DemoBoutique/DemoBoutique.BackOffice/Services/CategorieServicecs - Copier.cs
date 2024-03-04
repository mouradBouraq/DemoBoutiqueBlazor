using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Produit;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DemoBoutique.BackOffice.Services
{
    public class ProduitServicecs : HttpBaseService<Produit>
    {
        public ProduitServicecs(HttpClient httpClient, ProtectedLocalStorage localStorage, IConfiguration configuration) : base(httpClient, localStorage, configuration)
        {
        }

        public async Task<HttpResponseMessage> Add(Produit model)
        {
            var result = await Add($"api/produit/Add", model);
            return result;
        }

        public async Task<List<Produit>> All()
        {
            var result = await All($"api/produit/All");
            return result;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await Delete($"api/produit/delete/{id}");
            return result;
        }
    }
}
