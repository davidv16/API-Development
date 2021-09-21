using System;
using System.Collections.Generic;

namespace Datafication.Repositories.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<IceCream> IceCreams { get; set; }

        
    }
}