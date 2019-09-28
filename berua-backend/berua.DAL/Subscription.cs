namespace berua.DAL
{
    public class Subscription
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public string AccountKeyId { get; set; }
        public AccountKey AccountKey { get; set; }
    }
}
