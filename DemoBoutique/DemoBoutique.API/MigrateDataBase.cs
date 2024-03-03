using DemoBoutique.Infrastructure.Persistence;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DemoBoutique.API
{
    public static class MigrateDataBaseExtention
    {
        public static void MigrateDatabase([NotNull] this IApplicationBuilder app)
        {
            // Migrate and seed the database during startup. Must be synchronous.
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                 .CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<DbContextDemoBoutique>();
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

}
