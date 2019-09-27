using System;
using System.Collections.Generic;

namespace berua.DAL
{
    public class User
    {
        public int Id { get; set; }
        public DateTime DateRegistration { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
