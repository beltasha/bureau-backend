namespace berua.DAL
{
    public class Subscription
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string AccountKeyId { get; set; }
        public AccountKey AccountKey { get; set; }
    }
}
