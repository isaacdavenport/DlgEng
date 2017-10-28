using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DialogEngine.Models.Dialog
{
    /// <summary>
    /// Used for deserialization of json files with created characters.
    /// </summary>
    public class Character
    {


        public const int RecentPhrasesQueueSize = 8;

        /// <summary>
        /// A character's Phrases list holds all the phrases they might say along with 
        /// heuristic phraseWeights on what parts of a model dialog they might use them in.
        /// </summary>
        [JsonProperty("Phrases")]
        public List<PhraseEntry> Phrases = new List<PhraseEntry>(); //entry now has string phraseweight tags.

        [JsonProperty("PhraseTotals")]
        public PhraseEntry PhraseTotals = new PhraseEntry();

        public Queue<PhraseEntry> RecentPhrases = new Queue<PhraseEntry>()
            ; //TODO make this a method that runs over the history

        [JsonProperty("CharacterName")]
        public string CharacterName { get; protected set; }

        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix { get; protected set; }
    }
}
