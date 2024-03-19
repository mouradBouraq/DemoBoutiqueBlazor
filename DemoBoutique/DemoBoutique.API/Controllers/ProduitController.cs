using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Models;
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


        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceProduit.ListAsync();

            List<ProduitGetDtoscs> _list = new List<ProduitGetDtoscs>();
            ProduitGetDtoscs produitGetDtoscs;
            foreach (var item in result)
            {
                produitGetDtoscs = new ProduitGetDtoscs();
                produitGetDtoscs.Id = item.Id;
                produitGetDtoscs.Libelle = item.Libelle;
                produitGetDtoscs.Image = item.Image;
                produitGetDtoscs.Prix = item.Prix;
               
                produitGetDtoscs.CategorieId = item.CategorieId;
                _list.Add(produitGetDtoscs);
            }
            return Ok(_list);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Produit produit)
        {
            //produit.CreatedAt = DateTime.Now;
            //produit.CreatedBy = "1";
            //produit.UpdatedAt = DateTime.Now;
            //produit.UpdatedBy = "1";
            var result = await _serviceProduit.AddAsync(produit);
            return Ok(result);
        }

        [HttpPost("Add2")]
        public async Task<IActionResult> Add(ProduitPostDtoscs produitPostDto)
        {
            Produit produit = new Produit();
            produit.Libelle = produitPostDto.Libelle;
            produit.Image = produitPostDto.Image;
            produit.Prix = produitPostDto.Prix;

            produit.CategorieId = produitPostDto.CategorieId;
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

        [HttpPost("Update")]
        public async Task<IActionResult> Update(ProduitPutDtoscs produitPutDto)
        {
            Produit produit = new Produit();
            produit.Id = produitPutDto.Id;
            produit.Libelle = produitPutDto.Libelle;
            produit.Image = produitPutDto.Image;
            produit.Prix = produitPutDto.Prix;

            produit.CategorieId = produitPutDto.CategorieId;
             await _serviceProduit.UpdateAsync(produit);
            return Ok();
        }
    }
}
