using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berua.API.Model
{
    public class TokenModel
    {
        public string Code { get; set; }
        public string ClientId { get; set; }
        public string RedirectId { get; set; }
    }
}
