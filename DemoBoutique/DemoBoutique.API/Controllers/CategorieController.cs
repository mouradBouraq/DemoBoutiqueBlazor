using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Application.Services;
using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Client;
using Microsoft.AspNetCore.Mvc;

namespace DemoBoutique.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        IServiceBase<Categorie> _serviceCategorie;
        public CategorieController(IServiceBase<Categorie> serviceCategorie)
        {
            _serviceCategorie = serviceCategorie;
        }

        [HttpGet("All")]
        public async Task<IActionResult> Index()
        {
            var result = await _serviceCategorie.ListAsync();
            return Ok(result);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add(Categorie categorie)
        {
            var result = await _serviceCategorie.AddAsync(categorie);
            return Ok(result);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceCategorie.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
