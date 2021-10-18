using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class ItemCondition
    {
        public int Id { get; set; }
        public string ConditionCode { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public IEnumerable<Item> Items { get; set; }
    }
}