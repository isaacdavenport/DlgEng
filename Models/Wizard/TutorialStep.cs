

using System.Collections.Generic;

namespace DialogEngine.Models.Wizard
{
    public class TutorialStep
    {
        public string VideoFileName { get; set; }
        public string InstructionalText { get; set; }
        public bool CollectUserInput { get; set; }
        public string Commands { get; set; }
        public Dictionary<string, double> PhraseWeights { get; set; }
        public string PhraseRating { get; set; }
        public double Popularity { get; set; }
        public List<List<string>> PlayUserRecordedAudioInContext {get; set; }
    }
}
