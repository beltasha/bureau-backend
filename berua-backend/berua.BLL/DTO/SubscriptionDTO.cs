namespace berua.BLL.DTO
{
    public class SubscriptionDTO
    {
        public int UserId { get; set; }
        public string AccountKey { get; set; }
        public SocialNetworkType Type { get; set; }
    }
}
