using System;
using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }

        //Navigation Properties
        public User Receiver { get; set; }
        public User Sender { get; set; }
        public ICollection<TradeItem> TradeItems { get; set; }

    }
}