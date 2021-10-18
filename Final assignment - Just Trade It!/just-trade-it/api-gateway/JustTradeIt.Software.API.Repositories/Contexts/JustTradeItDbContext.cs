using Microsoft.EntityFrameworkCore;
using JustTradeIt.Software.API.Models.Entities;
using System.Security.Cryptography.X509Certificates;

namespace JustTradeIt.Software.API.Repositories.Contexts
{
    public class JustTradeItDbContext : DbContext
    {
        public JustTradeItDbContext(DbContextOptions<JustTradeItDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Manual configuration of relations (many to many)
            modelBuilder.Entity<TradeItem>()
                .HasKey(x => new { x.TradeId, x.UserId, x.ItemId });
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}