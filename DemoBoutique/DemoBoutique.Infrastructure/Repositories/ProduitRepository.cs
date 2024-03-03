using DemoBoutique.Application.Interfaces.Repositories;
using DemoBoutique.Domain.Produit;
using DemoBoutique.Infrastructure.Persistence;
using DemoBoutique.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Infrastructure.Repositories
{
    public class ProduitRepository : RepositoryBase<Produit>, IProduitRepository
    {
        public ProduitRepository(DbContextDemoBoutique repositoryContext) : base(repositoryContext)
        {
        }
    }
}
