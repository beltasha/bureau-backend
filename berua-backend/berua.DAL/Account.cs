using System.Collections.Generic;

namespace berua.DAL
{
    public class Account
    {
        public int Id { get; set; }
        public string LoginVK { get; set; }
        public string LoginInstagram { get; set; }
        public string LoginFacebook { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
