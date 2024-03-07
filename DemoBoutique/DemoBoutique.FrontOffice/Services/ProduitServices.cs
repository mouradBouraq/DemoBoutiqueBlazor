using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Produit;
using DemoBoutique.FrontOffice.Services;
using System.Net.Http.Json;
using System;

namespace DemoBoutique.FrontOffice.Services
{
    public class ProduitServices : HttpBaseService<Produit>
    {
        public ProduitServices(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public async Task<HttpResponseMessage> Add(Produit model)
        {
            var result = await Add($"api/produit/Add", model);
            return result;
        }

        public async Task<List<Produit>> All()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Produit>>($"api/produit/All", options) ?? new List<Produit>();
            //var result = await All($"api/produit/All");
            return result;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await Delete($"api/produit/delete/{id}");
            return result;
        }
    }
}
