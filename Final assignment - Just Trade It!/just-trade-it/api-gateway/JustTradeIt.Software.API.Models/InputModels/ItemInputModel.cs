using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ItemInputModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }

        //TODO: validate condition: MINT, GOOD, USED, BAD, DAMAGED
        [Required]
        //[AcceptVerbs("MINT", "GOOD", "USED", "BAD", "DAMAGED")]
        public string ConditionCode { get; set; }
        //TODO: is this good enough to validate url?
        [Url]
        public IEnumerable<string> ItemImages { get; set; }
    }
}