using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Client;
using Microsoft.AspNetCore.Mvc;

namespace DemoBoutique.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        IServiceBase<Client> _serviceClient;
        public ClientController(IServiceBase<Client> serviceClient)
        {
            _serviceClient = serviceClient;
        }

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var result = await _serviceClient.ListAsync();
            return Ok(result);
        }
    }
}
