using System.Collections.Generic;

namespace berua.DAL
{
    public class Account
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string AvatarUrl { get; set; }
        public string KeyVK { get; set; }
        public string KeyInstagram { get; set; }
        public string KeyFacebook { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
