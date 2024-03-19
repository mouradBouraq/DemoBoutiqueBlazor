using DemoBoutique.Domain.Commande;
using DemoBoutique.Domain.Models;

namespace DemoBoutique.FrontOffice.Services
{
    public class CommandeServices : HttpBaseService<CommandeModel>
    {
        public CommandeServices(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public async Task<HttpResponseMessage> Add(CommandeModel model)
        {
            var result = await Add($"api/Commande/Add", model);
            return result;
        }
    }
}
