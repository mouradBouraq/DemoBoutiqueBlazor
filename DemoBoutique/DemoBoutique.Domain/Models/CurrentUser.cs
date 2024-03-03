using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Domain.Models
{
    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
    }
}
