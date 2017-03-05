using System;
using System.Collections.Generic;


namespace DialogEngine
{
    public class Skylar : Character
    {
        public Skylar()
        {
            CharacterName = "Skylar";
            CharacterPrefix = "SC";

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Skylar has not been initialized.",
                FileName = "SkylarHasNotBeen",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Hi, I'm Skylar",
                FileName = "HiImSkylar",
                phraseWeights = new Dictionary<string, double>
                {
                   { "Greeting", 1.0 },
                   { "RequestAffirmation", 0.2 },
                   { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Meow",
                FileName = "Meow",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.3 },
                    { "Greeting", 0.2 },
                    { "YesNoQuestion", 0.4 },
                    { "RequestAdvice", 0.1 },
                    { "Retreat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "My litter box is full.",
                FileName = "MyLitterBoxIs",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.2 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "EEEwww!",
                FileName = "EEEwww",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "No", 0.2 },
                    { "ShutUp", 0.2 },
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "Could I have a scratch behind the ears?",
                FileName = "CoudIHaveA",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "RequestAffirmation", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "Who has a watch?  I need to know what time it is.",
                FileName = "WhoHasAWach",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.7 },
                    { "GiveDisbelief", 0.3 },
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "Wow",
                FileName = "wow",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "Nice to see you again",
                FileName = "NiceToSeeYou",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 },
                    { "Greeting", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.PG,
                DialogStr = "I have claws",
                FileName = "IHaveClaws",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.1 },
                    { "Threat", 1.0 },
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "You do not want to see me when I'm mad",
                FileName = "YouDoNotWantTo",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.5 },
                    { "No", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you have any rabbit?  I like rabbit.",
                FileName = "DoYouHaveRabbitI",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "Are those shoelaces I see?  They look delicious and scrumptious.",
                FileName = "AreThoesShoeLacesISee",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "The stinkier the better!",
                FileName = "TheStinkierTheBetter",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.4 },
                    { "GiveMotivation", 0.3 },
                    { "Exclamation", 0.3 }
                }
            });
                if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
                {
                    Phrases.RemoveAt(Phrases.Count - 1);
                }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't wash those socks, they smell just right!",
                FileName = "Don'tWashThoesSocksThay",
                phraseWeights = new Dictionary<string, double>
                {
                    { "GiveSurprisingStatement", 0.4 },
                    { "GiveActivity", 0.3 },
                    { "Exclamation", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do I like cat food?",
                FileName = "DoILikeCatFood",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 },
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Yes",
                FileName = "Yes",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "No",
                FileName = "No",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Remind me am I a gata or a gato?",
                FileName = "RemindMeAmIAGata",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.1 },
                    { "Greeting", 0.1 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Once I decided to tackle my sister, but my owners came over and yelled" +
                            "at me so I decided to tackle the couch monster instead.  Then I decided to take a nap." +
                            "If you look closely you can see where I took down that couch, it was quite a" +
                            "tasty victory.  I think we can eat some couch monster for dinner tonight that entre c" +
                            "comes with a side of stuffing.",
                FileName = "OneDayIDicidedTo",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "Ramble", 0.4 },
                    { "GiveRecentHistory", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You are a good person.  And have a lot interesting food.",
                FileName = "YouAreAGood",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "GiveSurprisingStatement", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Your feet smell wonderful.  Now pet me.",
                FileName = "YourFeetSmellWonderful",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 },
                    { "GiveAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Purr.",
                FileName = "Purr",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.6 },
                    { "GiveSurprisingStatement", 0.3 },
                    { "Retreat", 0.3 },
                    { "RequestCatchup", 0.3 }
                    }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Give me some lap.",
                FileName = "GiveMeSomeLap",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.02 },                    
                    { "GiveSurprisingStatement", 0.2 }                                       
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I was begging for food.  They finally set down the bowl.  Fancy feast." +
                            "It is the most delicious thing world except for fresh couch.",
                FileName = "IWasBeggingForFood",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I don't mind eating stale couch.  Its almost as good as that new couch flavor.",
                FileName = "IDon'tMindStaleCouch",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.4 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Do cats fly?",
                FileName = "CanCatsFly",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Watch our for flying cats.",
                FileName = "WachOutForFlyingCats",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 },
                    { "GiveAdvice", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "That sounds as likely as bacon falling from the sky.",
                FileName = "ThatSoundsAsLikelyAs",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 },
                    { "GiveDisbelief", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Guess what I saw my older sister doing.",
                FileName = "GessWhatISawMy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what my favorite thing is?",
                FileName = "DoYouKnowWhat",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You had to ask now.  I was about to catch my dinner.",
                FileName = "YouHadTo",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 },
                    { "YesNoQuestion", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Trying to catch some mice for dinner behind the horses trough.",
                FileName = "TryingToCach",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                FileName = "TheFoodWasTooDim",
                DialogStr = "The food was too dim.  It needed to have more suprise in it.  " +
                           "I think it would have been better with some more meat in it.",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.4 },
                    { "GiveRecentHistory", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Talk to the paw",
                FileName = "TalkToThePaw",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I have a loaded cannon and I am not afraid to use it.  You know what" +
                            "I mean by a loaded cannon.",
                FileName = "IHaveAloadedC",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I am turning away from you, the tail up means you should stop talking now.",
                FileName = "IAmTuerning",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.8 }                    
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you know any humorous or entertaining tales that could cheer someone up?",
                FileName = "DoYouKnowAny",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.5 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I would watch where you step.  It might not end well." ,
                FileName = "watch",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.6 },
                    { "RequestAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Be careful where you step.  You don't want to step in something unpleasant." +
                            "Those may look like rocks, but you know what it is...",
                FileName = "BeCarefulWhereYou",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 1.5 },
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You overthink something it is going to get hard.  If you underthink" +
                            "it could get worse.  You need to think it just right.  How do you know if" +
                            "it is just right?  I have no idea.",
                FileName = "YouUnderthink",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Your fresh scent offends me.",
                FileName = "YourFresh",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                 FileName = "YourColor",
                DialogStr = "Your color choices are unpleasant.",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you been hunting lately?",
                FileName = "HaveYou",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 },
                    { "YesNoQuestion", 0.2 },
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what kept my owners so busy they couldn't fill the food bowl",
                FileName = "DoYouKnowWhatKept",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what my sister is always doing that prevents her" +
                            "from paying attention to what she was doing?",
                FileName = "DoYouKnowWhatMy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 },
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hunting mice?",
                FileName = "HuntingMice",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I Think I Here My",
                FileName = "IThinkIHereMy",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Borrowing sand from the litter box to build a castle?",
                FileName = "BorrowingSandFromThe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I've lost a few mice.",
                FileName = "I'vLostAFew",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 },
                    { "RequestAffirmation", 0.2 },
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Always watch where you step if you want to avoid unpleasantness in this world.",
                FileName = "AlwaysWachWereYou",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 },
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Does a cat have five legs?",
                FileName = "DoseACatHave",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do dogs chase cats?",
                FileName = "DoDogsChaseCats",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do cats like water?",
                FileName = "DoCatsLikeWater",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do cats waste their time chasing dogs?",
                FileName = "DOCatsWastethere",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 },
                    { "RequestAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Naps fix everything.  Except for stomach aches.",
                FileName = "NapsFixEverythingExeprdFor",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Why would my sister eat her cat litter?",
                FileName = "WhyDidMySisterEat",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What could make my owner forget to fill my food bowl.",
                FileName = "WhatCoudMakeMyOwner",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 },
                    { "RequestAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "They were probably angry from being awakened from their nap.",
                FileName = "ThayWherProblyAngryFrom",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Maybe someone stepped on their paw.",
                FileName = "MabySomeOneStepedOnThair",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know where the last stop on the bus line is?",
                FileName = "DoYouKnowWhereThe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know where the dogs hang out around here.  I want to stay clear.",
                FileName = "DoYouKnowWhereTheDogs",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Try down where the dogs go, they burry things.",
                FileName = "TryDownWhereTheDogsGo",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.6 },
                    { "GiveAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Do you know what really scares me sprinklers ah sprinklers run" +
                            "let me in that's what happens when I'm around" +
                            "sprinkers so pleas don't turn them on please. ",
                FileName = "DoYouKnowWhat",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.5 },
                    { "Ramble", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Below that nice scrumptious couch, just watch out for the unknowns under there.",
                FileName = "BelowThatNiceScrumpishCouch",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sorry I was just looking at those scrumptious chickens over there.",
                FileName = "SorryIWasJustLooking",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "GiveRecentHistory", 0.4 } }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Sometimes it is better to forgive.",
                FileName = "",
                phraseWeights = new Dictionary<string, double>{
                    { "LM06B", 0.2 } }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "",
                FileName = "",
                phraseWeights = new Dictionary<string, double>{
                    { "LM", 0.2 } }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "",
                FileName = "",
                phraseWeights = new Dictionary<string, double>{
                    { "LM", 0.2 } }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "",
                FileName = "",
                phraseWeights = new Dictionary<string, double>{
                    { "LM", 0.2 } }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "",
                FileName = "",
                phraseWeights = new Dictionary<string, double>{
                    { "LM", 0.2 } }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "",
                FileName = "",
                phraseWeights = new Dictionary<string, double>{
                    { "LM", 0.2 } }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
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
