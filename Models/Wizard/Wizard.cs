using Newtonsoft.Json;
using System.Collections.Generic;

namespace DialogEngine.Models.Dialog
{
    public class Wizard
    {
        [JsonProperty("WizardName")]
        public string WizardName { get; set; }

        [JsonProperty("Commands")]
        public string Commands { get; set; }

        [JsonProperty("TutorialSteps")]
        public List<TutorialStep> TutorialSteps { get; set; }

        [JsonIgnore]
        public string FileName { get; set; }

        [JsonIgnore]
        public int JsonArrayIndex { get; set; }

    }
}
