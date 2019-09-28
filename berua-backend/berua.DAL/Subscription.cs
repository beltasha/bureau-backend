namespace berua.DAL
{
    public class Subscription
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
