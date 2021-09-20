namespace TechnicalRadiation.Models.Entities
{
    public class AuthorNewsItem
    {
        public int AuthorsId { get; set; }
        public int NewsItemsId { get; set; }

        // Navigation Properties
        public Author Author { get; set; }
        public NewsItem NewsItem { get; set; }
    }
}