

using System.Globalization;
using System.Windows.Controls;

namespace DialogEngine.ValidationRules
{
    public class CharacterPrefixValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Field requires 3 characters.")
                : ValidationResult.ValidResult;
        }
    }
}
