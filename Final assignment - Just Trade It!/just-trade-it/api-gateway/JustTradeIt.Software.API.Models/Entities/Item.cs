using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int ItemConditionId { get; set; }
        public int OwnerId { get; set; }

        //Navigation Properties
        public ItemCondition ItemCondition { get; set; }
        public User Owner { get; set; }
        public ICollection<ItemImage> ItemImages { get; set; }
        public ICollection<TradeItem> TradeItems { get; set; }
    }
}