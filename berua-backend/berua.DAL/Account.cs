using System.Collections.Generic;

namespace berua.DAL
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AvatarUrl { get; set; }
        public List<AccountKey> AccountKeys { get; set; }
    }
}
