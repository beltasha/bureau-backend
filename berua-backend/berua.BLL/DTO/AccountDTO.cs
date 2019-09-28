using System.Collections.Generic;

namespace berua.BLL.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string PhotoUrl { get; set; }
        public Dictionary<SocialNetworkType, string> AccountIds {get; set;}

        public AccountDTO()
        {
            AccountIds = new Dictionary<SocialNetworkType, string>();
        }
    }
}
