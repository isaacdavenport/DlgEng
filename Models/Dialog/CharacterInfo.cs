using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DialogEngine.Models.Dialog
{
    public class CharacterInfo
    {
        [JsonProperty("CharacterName")]
        public string CharacterName { get;  set; }

        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix { get;  set; }

        public  string FileName { get; set; }

    }
}
