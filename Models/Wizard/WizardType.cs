
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DialogEngine.Models.Wizard
{
    public class WizardType
    {
        [JsonProperty("WizardName")]
        public string WizardName { get; set; }

        [JsonProperty("Commands")]
        public string Commands { get; set; }

        [JsonProperty("TutorialSteps")]
        public List<TutorialStep> TutorialSteps { get; set; }
    }
}
