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

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var result = await _serviceProduit.ListAsync();
            return Ok(result);
        }
    }
}
