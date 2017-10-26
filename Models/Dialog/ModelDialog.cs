using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DialogEngine.Models.Dialog
{
    public class ModelDialog
    {
        [JsonProperty("AddedOnDateTime")] public DateTime AddedOnDateTime = new DateTime(2016, 1, 2, 3, 4, 5);

        [JsonProperty("Adventure")] public string Adventure = "";

        // a ModelDialog is a sequence of phrase types that represent an exchange between characters 
        // the model dialog will be filled with randomly selected character phrases of the appropriate phrase type
        [JsonProperty("DialogName")] public string Name;

        [JsonProperty("PhraseTypeSequence")] public List<string> PhraseTypeSequence = new List<string>();

        [JsonProperty("Popularity")] public double Popularity = 1.0;

        [JsonProperty("Provides")] public List<string> Provides = new List<string>();

        [JsonProperty("Requires")] public List<string> Requires = new List<string>();

        public bool AreDialogsRequirementsMet()
        {
            return true;
        }
    }
}
