using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

public class Expertize : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    var input = value as string;
    List<String> values = new List<String>();

    values.Add("Ghost catcher");
    values.Add("Ghoul strangler");
    values.Add("Monster encager");
    values.Add("Zombie exploder");


    if (values.Any(x => x == input))
    {
      return ValidationResult.Success;
    }

    return new ValidationResult("Invalid expertize");
  }
}