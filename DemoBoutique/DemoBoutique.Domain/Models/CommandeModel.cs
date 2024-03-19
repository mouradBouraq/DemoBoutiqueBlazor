using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Domain.Models
{
    public class CommandeModel
    {
        public double Total
        {
            get
            {
                var total = CommandeLigneModels.Sum(cl => cl.Quantite * cl.PrixUnitaire);
                return total;

            }
        }
      public  List<CommandeLigneModel> CommandeLigneModels { get; set; }
    }

    public class CommandeLigneModel
    {
        public int IdProduit { get; set; }
        public string Libele { get; set; }
        public int Quantite { get; set; }
        public double PrixUnitaire { get; set; }
    }
}
