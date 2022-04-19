using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaCEP.Models
{
    public class AccessToken
    {
        public bool Authenticated { get; set; }
        public string Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
