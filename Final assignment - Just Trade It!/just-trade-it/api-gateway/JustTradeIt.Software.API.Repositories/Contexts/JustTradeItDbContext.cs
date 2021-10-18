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
            

            
            modelBuilder.Entity<Trade>( act => {
                act.HasOne(field => field.Receiver)
                    .WithMany(fk => fk.Receivers)
                    .HasForeignKey(fk => fk.ReceiverId);
                    //.HasConstraintName("Receiver_FK");

                act.HasOne(field => field.Sender)
                    .WithMany(fk => fk.Senders)
                    .HasForeignKey(fk => fk.SenderId);
                    //.HasConstraintName("Sender_FK");

            });
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

