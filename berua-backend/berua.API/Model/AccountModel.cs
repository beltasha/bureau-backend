using berua.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berau_backend.Model
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string AccountUrl { get; set; }
        public SocialNetworkType Type { get; set; }
    }
}
