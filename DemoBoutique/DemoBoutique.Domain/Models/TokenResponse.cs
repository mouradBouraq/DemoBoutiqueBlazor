using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Domain.Models
{
    public class TokenResponse
    {
        public string? Token { get; set; } = string.Empty;
        public DateTimeOffset Expiration { get; set; }
    }
}
