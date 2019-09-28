using System;
using System.Collections.Generic;

namespace berua.DAL
{
    public class User
    {
        public long Id { get; set; }
        public DateTime DateRegistration { get; set; }
        public string Domain { get; set; }
        public string Phone { get; set; }
        public long ChatId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
