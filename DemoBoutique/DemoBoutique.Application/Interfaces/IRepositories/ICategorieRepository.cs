using DemoBoutique.Application.Interfaces.Repositories.Base;
using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Interfaces.Repositories
{
    public interface ICategorieRepository : IRepositoryBase<Categorie> 
    {
    }
}
