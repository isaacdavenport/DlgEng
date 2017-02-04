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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.SHSilence, 3.5 }
                }
            });

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Hey, I can hear you out there.  Help me out.  I'm locked in the bathroom!",
                FileName = "HeyICanHear",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.1 },
                    { PhraseTypes.RequestAffirmation, 0.1 }
                }
            });

            PhraseTotals.PhraseWeights = new Dictionary<PhraseTypes, double>();
            PhraseTotals.DialogStr = "PhraseTotals";
            PhraseTotals.FileName = "No Audio Filename Associated with PhraseTotals";

            foreach (PhraseTypes currentPhraseType in Enum.GetValues(typeof (PhraseTypes))) {
                PhraseTotals.PhraseWeights.Add(currentPhraseType, 0.0f);
            }

            foreach (PhraseEntry currentPhrase in Phrases) {
                foreach (var currentPhraseType in currentPhrase.PhraseWeights) {
                    // for a given phrase of a character add all the associated phrase weights for the phrase to the proper PhraseTotals
                    PhraseTotals.PhraseWeights[currentPhraseType.Key] += currentPhraseType.Value;
                }
            }

            // initialize  RecentPhrases with the 0 phrase uninitialized so initial phrases aren't blocked while 
            //que fills since there is no way to prevent que from growing and dump old phrases into oblivion we must manually trim
            for (int k = 0; k < RecentPhrasesQueueSize; k++)
                RecentPhrases.Enqueue(Phrases[0]);
        }
    }
}
