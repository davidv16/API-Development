using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Entities;


namespace TechnicalRadiation.Repositories.Contexts
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Manual configuration of relations (many to many)
            modelBuilder.Entity<AuthorNewsItem>()
                .HasKey(x => new { x.AuthorsId, x.NewsItemsId });

            modelBuilder.Entity<CategoryNewsItem>()
                .HasKey(x => new { x.CategoriesId, x.NewsItemsId });
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<AuthorNewsItem> AuthorNewsItem { get; set; }
        public DbSet<CategoryNewsItem> CategoryNewsItem { get; set; }

    }
}