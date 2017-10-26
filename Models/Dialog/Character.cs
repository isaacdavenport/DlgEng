using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DialogEngine.Models.Dialog
{
    public class Character
    {
        // A character's Phrases list holds all the phrases they might say along with 
        // heuristic phraseWeights on what parts of a model dialog they might use them in.

        public const int RecentPhrasesQueueSize = 8;

        [JsonProperty("Phrases")]
        public List<PhraseEntry> Phrases = new List<PhraseEntry>(); //entry now has string phraseweight tags.

        [JsonProperty("PhraseTotals")] public PhraseEntry PhraseTotals = new PhraseEntry();

        public Queue<PhraseEntry> RecentPhrases = new Queue<PhraseEntry>()
            ; //TODO make this a method that runs over the history

        [JsonProperty("CharacterName")]
        public string CharacterName { get; protected set; }

        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix { get; protected set; }
    }
}
