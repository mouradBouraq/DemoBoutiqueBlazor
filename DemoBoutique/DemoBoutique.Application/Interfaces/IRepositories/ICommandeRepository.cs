using DemoBoutique.Application.Interfaces.Repositories.Base;
using DemoBoutique.Domain.Commande;
using DemoBoutique.Domain.Produit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Interfaces.Repositories
{
    public interface ICommandeRepository : IRepositoryBase<Commande> 
    {
    }
}
