using System;
using System.Collections.Generic;

namespace TechnicalRadiation.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        // code generated
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation properties
        public ICollection<CategoryNewsItem> NewsItemLink { get; set; }

    }
}