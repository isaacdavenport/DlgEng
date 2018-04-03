

using DialogEngine.Models.Dialog;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DialogEngine.Models.Wizard
{
    public class WizardsList
    {
        public List<WizardType> Wizards { get; set; }
        public List<Character> Characters { get; set; }
        public List<ModelDialogInfo> DialogModels {get; set;}
    }
}
