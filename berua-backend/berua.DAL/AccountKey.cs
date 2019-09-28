using System;
using System.Collections.Generic;
using System.Text;

namespace berua.DAL
{
    public class AccountKey
    {
        public string Id { get; set; }
        public byte SocialNetworkType { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public List<Subscription> Subscriptions { get; set; }
    }
}
