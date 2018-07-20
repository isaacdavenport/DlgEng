﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DialogEngine.Models.Dialog
{
    public class Wizard:IEquatable<Wizard>
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

        public bool Equals(Wizard other)
        {
            return this.WizardName.Equals(other.WizardName);
        }
    }
}
