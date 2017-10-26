using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Dialog
{
    internal static class ParentalRatings
    {
        private static readonly Dictionary<string, int> _dict = new Dictionary<string, int>
        {
            {"G", 1},
            {"PG", 2},
            {"PG13", 3},
            {"R", 4}
        };

        public static int GetNumeric(string ratingString)
        {
            // Try to get the result in the static Dictionary
            int result;
            if (_dict.TryGetValue(ratingString, out result))
                return result;
            return -1;
        }
    }
}
