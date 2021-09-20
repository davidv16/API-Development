using System;
using System.Collections.Generic;

namespace TechnicalRadiation.Models.Entities
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgSource { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime PublishDate { get; set; }

        // code generated
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation properties
        public ICollection<AuthorNewsItem> AuthorNewsItemLink { get; set; }
        // Navigation properties
        public ICollection<CategoryNewsItem> CategoryNewsItemLink { get; set; }


    }
}