using Newtonsoft.Json;
using System.Collections.Generic;

namespace DialogEngine.Models.Wizard
{
    public class WizardType
    {
        public string WizardName { get; set; }
        public string Commands { get; set; }
        public List<TutorialStep> TutorialSteps { get; set; }
    }
}
