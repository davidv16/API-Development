using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JustTradeIt.Software.API.Models.Dtos;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class TradeInputModel
    {
        [Required]
        public string ReceiverIdentifier { get; set; }
        [Required]
        public IEnumerable<ItemDto> ItemsInTrade { get; set; }
    }
}