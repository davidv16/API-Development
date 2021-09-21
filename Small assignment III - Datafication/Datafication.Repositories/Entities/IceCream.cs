using System;
using System.Collections.Generic;

namespace Datafication.Repositories.Entities
{
    public class IceCream
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManufacturerId { get; set; }

        //code generated
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        //navigation properties
        public ICollection<Image> Images { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}