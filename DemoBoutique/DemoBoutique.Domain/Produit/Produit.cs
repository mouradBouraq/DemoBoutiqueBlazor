using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoBoutique.Domain.Categorie;

namespace DemoBoutique.Domain.Produit
{
    public class Produit : BaseEntitie
    {
        public string Libelle { get; set; }
        public string Image { get; set; }
        public decimal Prix { get; set; }

        public int CategorieId { get; set; }

        public DemoBoutique.Domain.Categorie.Categorie Categorie { get; set; }
    }
}
