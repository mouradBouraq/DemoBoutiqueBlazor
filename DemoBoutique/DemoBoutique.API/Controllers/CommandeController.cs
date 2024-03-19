using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Commande;
using DemoBoutique.Domain.Models;
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

        [HttpPost]
        public async Task<IActionResult> Add(CommandeModel commande)
        {
            Commande _newCommande = new Commande();
            _newCommande.ClientID = 1;

            _newCommande.Lignes = commande.CommandeLigneModels.Select(x => new CommandeLigne
            {
                ProduitId = x.IdProduit,
                PrixUnitaireTTC = x.PrixUnitaire,
                Quantite=x.Quantite,
              }).ToList();
            var result = await _serviceCommande.AddAsync(_newCommande);
            return Ok(result);
        }
    }
}
