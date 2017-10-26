using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Dialog
{
    public class PhraseEntry
    {
        public string DialogStr;

        public string FileName;

        public string PhraseRating;

        public Dictionary<string, double> PhraseWeights; //to replace PhraseWeights, uses string tags.
    }
}
