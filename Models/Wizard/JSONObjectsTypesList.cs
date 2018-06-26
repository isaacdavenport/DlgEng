

using DialogEngine.Models.Dialog;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DialogEngine.Models
{
    public class JSONObjectsTypesList
    {
        [JsonProperty("Wizards")]
        public ObservableCollection<Wizard> Wizards { get; set; }

        [JsonProperty("Characters")]
        public ObservableCollection<Character> Characters { get; set; }

        [JsonProperty("DialogModels")]
        public ObservableCollection<ModelDialogInfo> DialogModels {get; set;}
    }
}
