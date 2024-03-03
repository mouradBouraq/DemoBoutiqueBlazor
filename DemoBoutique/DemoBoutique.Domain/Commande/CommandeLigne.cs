using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Domain.Commande
{
    public class CommandeLigne : BaseEntitie
    {
        public string Libele { get; set; }
        public int ProduitId { get; set; }
        public int CommandeId { get; set; }
        public int Quantite { get; set; }
        public double PrixUnitaireTTC { get; set; }
        public Commande Commande { get; set; }
        public DemoBoutique.Domain.Produit.Produit Produit { get; set; }
    }
}
