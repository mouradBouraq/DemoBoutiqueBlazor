using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Domain.Commande
{
    public class Commande : BaseEntitie
    {
        public decimal TotalPrixTTC { get; set; }
        public int  ClientID { get; set; }
        List<CommandeLigne> Lignes { get; set; }
        public DemoBoutique.Domain.Client.Client Client { get; set; }

    }
}
