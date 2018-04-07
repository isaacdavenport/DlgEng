

using DialogEngine.Models.Dialog;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DialogEngine.Models.Wizard
{
    public class WizardsList
    {
        public ObservableCollection<WizardType> Wizards { get; set; }
        public ObservableCollection<Character> Characters { get; set; }
        public ObservableCollection<ModelDialogInfo> DialogModels {get; set;}
    }
}
