using DemoBoutique.FrontOffice;
using DemoBoutique.FrontOffice.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DemoBoutique.FrontOffice
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //Http client
            string apiUrl = builder.Configuration["ApiUrl"];

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
            builder.Services.AddScoped<ProduitServices>();

            await builder.Build().RunAsync();
        }
    }
}
