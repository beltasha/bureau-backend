using Microsoft.EntityFrameworkCore;
using System.IO;

namespace berua.DAL
{
    public class BeruaContext : DbContext
    {
        private readonly string _pathdb = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountKey> AccountKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .HasKey(pt => new { pt.AccountKeyId, pt.UserId });

            modelBuilder.Entity<Subscription>()
                .HasOne(pt => pt.AccountKey)
                .WithMany(p => p.Subscriptions)
                .HasForeignKey(pt => pt.AccountKeyId);

            modelBuilder.Entity<Subscription>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.Subscriptions)
                .HasForeignKey(pt => pt.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_pathdb}/beruaDB.db");
    }
}