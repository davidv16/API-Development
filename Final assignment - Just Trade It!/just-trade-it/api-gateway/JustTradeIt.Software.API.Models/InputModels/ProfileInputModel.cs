using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ProfileInputModel
    {
        [MinLength(3)]
        public string FullName { get; set; }

        // TODO: finish this
        //public IFormFile ProfileImage { get; set; }

    }
}