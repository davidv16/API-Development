using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.CustomAttributes
{
    public class UrlList : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var urlList = value as IEnumerable<string>;

            foreach (var url in urlList)
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
                {
                    return new ValidationResult($"Invalid url: {url}");
                }
            }
            return ValidationResult.Success;
        }

    }
}