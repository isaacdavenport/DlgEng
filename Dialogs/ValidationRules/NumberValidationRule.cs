

using System;
using System.Globalization;
using System.Windows.Controls;

namespace DialogEngine.Dialogs.ValidationRules
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string _enteredValue = (value == null ? "": value.ToString());

            try
            {
                int.Parse(_enteredValue);
                return ValidationResult.ValidResult;
            }
            catch (Exception)
            {
               return new ValidationResult(false, "Field requres number.");
            }
        }
    }
}
