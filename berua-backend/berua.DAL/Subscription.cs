namespace berua.DAL
{
    public class Subscription
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
