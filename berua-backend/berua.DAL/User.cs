using System;
using System.Collections.Generic;
using System.Text;

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
        public ICollection<Subscription> Subscriptions { get; set; }

        public User()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}
