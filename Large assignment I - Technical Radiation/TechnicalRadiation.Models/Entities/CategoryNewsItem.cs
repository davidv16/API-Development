namespace TechnicalRadiation.Models.Entities
{
    public class CategoryNewsItem
    {
        public int CategoriesId { get; set; }
        public int NewsItemsId { get; set; }

        // Navigation Properties
        public Category Category { get; set; }
        public NewsItem NewsItem { get; set; }

    }
}