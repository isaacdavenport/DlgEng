

using System.Globalization;
using System.Windows.Controls;

namespace DialogEngine.Dialogs.ValidationRules
{
    public class NoEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string _enteredValue = (string)value ?? "";

            return _enteredValue.Length == 0
                ? new ValidationResult(false, "Character name can't be empty.")
                : ValidationResult.ValidResult;
        }    
    }
}
