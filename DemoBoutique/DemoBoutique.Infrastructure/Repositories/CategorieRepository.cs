using DemoBoutique.Application.Interfaces.Repositories;
using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Commande;
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
    public class CategorieRepository : RepositoryBase<Categorie>, ICategorieRepository
    {
        public CategorieRepository(DbContextDemoBoutique repositoryContext) : base(repositoryContext)
        {
        }
    }
}
