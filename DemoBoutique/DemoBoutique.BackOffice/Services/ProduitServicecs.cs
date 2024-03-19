
using DemoBoutique.Domain.Models;
using DemoBoutique.Domain.Produit;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;

namespace DemoBoutique.BackOffice.Services
{
    public class ProduitServicecs : HttpBaseService<ProduitGetDtoscs>
    {
        public ProduitServicecs(HttpClient httpClient, ProtectedLocalStorage localStorage, IConfiguration configuration) : base(httpClient, localStorage, configuration)
        {
        }

        public async Task<HttpResponseMessage> Add(ProduitPostDtoscs model)
        {
            var result = await Add($"api/produit/Add2", model);
            return result;
        }

        public async Task<HttpResponseMessage> Update(ProduitPutDtoscs model)
        {
            //var result = await Update($"api/produit/Update", model);
            //return result;
            var response = await _httpClient.PostAsJsonAsync($"api/produit/Update", model);
            return response;
        }

        public async Task<List<ProduitGetDtoscs>> All()
        {
            var result = await All($"api/produit/All");
            return result;
        }

        public async Task<List<ProduitGetDtoscs>> GetAll()
        {
            var result = await All($"api/produit/GetAll");
            return result;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await Delete($"api/produit/delete/{id}");
            return result;
        }
    }
}
