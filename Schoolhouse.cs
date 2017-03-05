using System;
using System.Collections.Generic;


namespace DialogEngine
{
    public class SchoolHouse : Character
    {
        public SchoolHouse() {
            CharacterName = "SchoolHouse";
            CharacterPrefix = "SH";

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Silence",
                FileName = "silence",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.1 },
                    { "SHSilence", 0.1 }
                }
            });

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Hey, I can hear you out there.  Help me out.  I'm locked in the bathroom!",
                FileName = "HeyICanHear",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.1 },
                    { "RequestAffirmation", 0.1 }
                }
            });
            
            PhraseTotals.phraseWeights = new Dictionary<string, double>();
            PhraseTotals.DialogStr = "PhraseTotals";
            PhraseTotals.FileName = "No Audio Filename Associated with PhraseTotals";

            //Collects string tags from file and adds to global list
            foreach (PhraseEntry currentPhrase in Phrases)
            {
                foreach (string type in currentPhrase.phraseWeights.Keys)
                {
                    if (!GlobalPhraseTypes.TestPhraseTypes.Contains(type))
                    {
                        GlobalPhraseTypes.TestPhraseTypes.Add(type);
                    }
                }
            }

            //add weights to phraseType weight totals for each dialog.
            foreach (PhraseEntry currentPhrase in Phrases)
            {
                foreach (var currentPhraseType in currentPhrase.phraseWeights)
                {
                    // for a given phrase of a character add all the associated phrase weights for the phrase to the proper PhraseTotals
                    PhraseTotals.phraseWeights[currentPhraseType.Key] += currentPhraseType.Value;
                }
            }

            // initialize  RecentPhrases with the 0 phrase uninitialized so initial phrases aren't blocked while 
            //que fills since there is no way to prevent que from growing and dump old phrases into oblivion we must manually trim
            for (int k = 0; k < RecentPhrasesQueueSize; k++)
                RecentPhrases.Enqueue(Phrases[0]);
        }
    }
}
