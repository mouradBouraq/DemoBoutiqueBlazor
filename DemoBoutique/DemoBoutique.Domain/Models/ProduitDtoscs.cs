using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Domain.Models
{
    public class ProduitPostDtoscs
    {

        public string Libelle { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Prix { get; set; }

        public int CategorieId { get; set; }
    }

    public class ProduitPutDtoscs
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Prix { get; set; }

        public int CategorieId { get; set; }
    }

    public class ProduitGetDtoscs
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Prix { get; set; }

        public int CategorieId { get; set; }
       
    }
}
