

using System.Globalization;
using System.Windows.Controls;

namespace DialogEngine.Dialogs.ValidationRules
{
    public class CharacterPrefixValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string _enteredValue = (string)value ?? "";

            return (_enteredValue.Length != 3) && (_enteredValue.Length !=2) 
                ? new ValidationResult(false, "Field requires 3 characters.")
                : ValidationResult.ValidResult;
        }
    }
}
