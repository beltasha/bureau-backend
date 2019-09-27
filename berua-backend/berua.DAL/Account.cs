using System.Collections.Generic;

namespace berua.DAL
{
    public class Account
    {
        public int Id { get; set; }
        public string LoginVK { get; set; }
        public string LoginInstagram { get; set; }
        public string LoginFacebook { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        public Account()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}
