using Microsoft.EntityFrameworkCore;

namespace berua.DAL
{
    public class BeruaContext : DbContext
    {
        private static bool _created = false;
        public BeruaContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=Sample.db");
    }
}