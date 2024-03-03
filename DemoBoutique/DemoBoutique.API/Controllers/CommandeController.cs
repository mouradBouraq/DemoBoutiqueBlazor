using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Commande;
using DemoBoutique.Domain.Produit;
using Microsoft.AspNetCore.Mvc;

namespace DemoBoutique.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        IServiceBase<Commande> _serviceCommande;
        public CommandeController(IServiceBase<Commande> service)
        {
            _serviceCommande = service;
        }

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var result = await _serviceCommande.ListAsync();
            return Ok(result);
        }
    }
}
