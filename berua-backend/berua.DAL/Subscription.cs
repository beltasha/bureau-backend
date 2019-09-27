using System;
using System.Collections.Generic;
using System.Text;

namespace berua.DAL
{
    public class Subscription
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public User Student { get; set; }

        public int AccountId { get; set; }
        public Account Course { get; set; }
    }
}
