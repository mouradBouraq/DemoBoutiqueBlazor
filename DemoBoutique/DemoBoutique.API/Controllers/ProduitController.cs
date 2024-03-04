using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Produit;
using Microsoft.AspNetCore.Mvc;

namespace DemoBoutique.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        IServiceBase<Produit> _serviceProduit;
        public ProduitController(IServiceBase<Produit> service)
        {
            _serviceProduit = service;
        }

        [HttpGet("All")]

        public async Task< IActionResult> Index()
        {
            var result = await _serviceProduit.ListAsync();
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Produit produit)
        {
            var result = await _serviceProduit.AddAsync(produit);
            return Ok(result);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceProduit.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
