using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string HashedPassword { get; set; }

        //Navigation Properties
        public ICollection<Item> Items { get; set; }
        public ICollection<TradeItem> TradeItems { get; set; }
        public ICollection<Trade> Receivers { get; set; }
        public ICollection<Trade> Senders { get; set; }

    }
}