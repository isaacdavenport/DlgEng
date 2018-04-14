

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DialogEngine.Dialogs.ValidationRules
{
    public class RegexValidationRule : ValidationRule
    {
        public RegexValidtionRuleWrapper Wrapper { get; set;}

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = ValidationResult.ValidResult;

            // If there is no regular expression to evaluate,
            // then the data is considered to be valid.
            if (!String.IsNullOrEmpty(Wrapper.RegexPattern))
            {
                // Cast the input value to a string (null becomes empty string).
                string text = value as string ?? String.Empty;

                // If the string does not match the regex, return a value
                // which indicates failure and provide an error mesasge.
                if (!Regex.IsMatch(text, Wrapper.RegexPattern))
                    result = new ValidationResult(false, Wrapper.Message);
            }

            return result;
        }
    }
}
