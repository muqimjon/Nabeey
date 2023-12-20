using Recore.Service.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class CheckPhoneAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string phoneNumber = value.ToString() 
            ?? throw new CustomException(403, "Invalid phone number");

        string pattern = @"^\+998(90|91|93|94|97|88|20|33|70|99)[0-9]{7}$";

        if (!Regex.IsMatch(phoneNumber, pattern))
            throw new CustomException(403, "Invalid phone number");

        return true;
    }
}
