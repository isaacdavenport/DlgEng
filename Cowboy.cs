using System;
using System.Collections.Generic;


namespace DialogEngine
{
    public class Cowboy : Character
    {
        public Cowboy() {
            CharacterName = "Cowboy Bill";
            CharacterPrefix = "CB";

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "The Cowboy has not been initialized.",
                FileName = "TheCowboyHasNot",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Howdy",
                FileName = "Howdy",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1); 
                
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "These Spurs ain't afraid to kick up some dust",
                FileName = "TheseSpursAintAfraid",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "Threat", 0.5 },
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                DialogStr = "Lets us hunker down here a minute",
                PhraseRating = ParentalRating.G,
                FileName = "LetsUsHunkerDown",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'll be",
                FileName = "AllBe",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.6 },
                    { "SmCb_01B", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "That ain't cowpoke work your talkin bout there.",
                FileName = "ThisAintCowPoke",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.3 },
                    { "RequestActivity", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }


            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What did you think I meant when I said posse?",
                FileName = "WhatDidYouThink",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Calm down there big feller.",
                FileName = "CalmDownThereBig",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 },
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Well sir you might want to take that up with the local chaplain",
                FileName = "WellSirYouMight",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 },
                    { "GiveDisbelief", 0.2 },
                    { "YesNoQuestion", 0.03 },
                    { "Retreat", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What in tarnation?",
                FileName = "WhatInTarnation",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveDisbelief", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Wrap me in calf-leather and call me an ankle biter.",
                FileName = "WrapMeInCalf",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveDisbelief", 0.4 },
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Is that you George?  I thought you was kilt in that shootout.",
                FileName = "IsThatYouGeorge",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }


            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Keep your powder dry boys.",
                FileName = "KeepYourPowderDry",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "Retreat", 0.1 },
                    { "GiveAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Those two gonna get hitched?",
                FileName = "ThoseTwoGonnaGet",
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
                DialogStr = "Its quiet out here.  Too quiet. ",
                FileName = "ItsQuietOutHere",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Yup",
                FileName = "Yep",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 1.0 },
                    { "GiveAffirmation", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Don't get your knickers in a wad",
                FileName = "DontGetYourKnickers",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "No", 0.3 },
                    { "GiveSurprisingStatement", 0.3 },
                    { "GiveAdvice", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }


            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I think my nose was assaulted by a polecat that ate a case of rotten cabbage",
                FileName = "IThinkMyNose",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.4 },
                    { "GiveRecentHistory", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lets vamoos",
                FileName = "LetsVamoos",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.6 },
                    { "YesNoQuestion", 0.4 },
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "That makes about as much sense as whiskey on pancakes",
                FileName = "ThatMakesAsMuch",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.3 },
                    { "GiveDisbelief", 0.3 },
                    { "No", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Can't rightly say.",
                FileName = "CantRightlySay",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.01 },
                    { "Yes", 0.01 },
                    { "No", 0.1 },
                    { "GiveAffirmation", 0.01 },
                    { "RequestAffirmation", 0.01 },
                    { "GiveDisbelief", 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Ain't that purdy as a pig under a Christmas Tree",
                FileName = "AintThatPurdyAs",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.1 },
                    { "Retreat", 0.1 },
                    { "GiveSurprisingStatement", 0.2 },
                    { "GiveJoke", 0.05 },
                    { "GiveDisbelief", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "That is so hot it would catch fire faster than a meth lab",
                FileName = "ThatIsSoHot",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.3 },
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "The TSA Wouldn't let me wear my spurs through the metal detector.  What am I going to do ride the captain?",
                FileName = "TheTsaWouldntLet",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.5 },
                    { "GiveRecentHistory", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Thats about as significant as a fart in the wind",
                FileName = "ThatsAboutAsSignificant",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "The day's movin slower than molasses in January",
                FileName = "TheDaysMovinSlower",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveRecentHistory", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lord, I promise I'll be better.",
                FileName = "LordIpromiseIll",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "RequestAffirmation", 0.2 },
                    { "GiveDisbelief", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Reminds me of the time them injuns came over the mesa.  Wasn't but me an " +
                            "George and Carl out with 87 head.  Cattle don't make good fighters I tell you.",
                FileName = "RemindsMeOfTheTimeThemInjuns",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Some days, when the light is just right comin over the hill, \r\nI think I can see ole Bob up the ridge. Bob never was much one for words, \r\nbut he had a knack. Take that time right before the flood on the big Thompson.  \r\nMust have been something he sniffed out in the loweing of the herd, \r\nbut Bob got them doggies up the side of the hill before we knew what was happening.  \r\nWe didn't even manage to break camp and lost two weeks worth of flour and cooking oil, not to mention my favorite knife.  Got that knife off an Injun up in Gold Hill...",
                FileName = "SomeDaysWhenThe",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Ready to saddle up?",
                FileName = "ReadyToSaddleUp",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You lily livered coward-",
                FileName = "YouLilyLiveredCoward",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 },
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "You got shit form brains and your heart pumps pee pee",
                FileName = "YouGotShitFor",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Guess what them dogies was up to this afternoon",
                FileName = "GuessWhatThemDoggies",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You know what my Isabelles been up to?",
                FileName = "YouKnowWhatMY",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You know what I've been doin with my new spurs",
                FileName = "YouKnowWhatIve",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You will never guess what I did with my hat",
                FileName = "YouWillNeverGuess",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Birthing cattle",
                FileName = "BirthinCattle",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Fornicating",
                FileName = "Fornicating",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Branding em with the big L and lazy P",
                FileName = "BrandinEmWithA",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I could use some cheering up pardner",
                FileName = "ICouldUseSome",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.8 },
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe a night out under the stars would make you feel better",
                FileName = "MaybeANightUnder",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "When I feel blue, I like a bit of Hank Williams Sr. and some cocaine",
                FileName = "WhenIFeelBlue",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I leaned back on my spurs today and caught more air than Tony Hawk.",
                FileName = "ILeanedBackOn",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.6 },
                    { "GiveJoke", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Reminds me of the day I met Jesus. He cuts the grass for old man Christianson.",
                FileName = "RemindsMeOfTheDayIMet",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You couldn't find your backside with both hands.",
                FileName = "YouCouldntFindYour",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Mornin' sunshine.",
                FileName = "MorningSunshine",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You might not ought to provoke a man with a loaded six shooter.",
                FileName = "YouMightNotOught",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Think its about time to ride off into the sunset.",
                FileName = "ThinkItsAboutTime",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you enjoy the fragrant aroma of polecat?",
                FileName = "DoYouEnjoyTheFragrant",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 },
                    { "YesNoQuestion", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Them clouds are gonna break fore too long pardner.",
                FileName = "ThemCloudsAreGonna",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 },
                    { "Exclamation", 0.1 },
                    { "Retreat", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hush now, you!",
                FileName = "HushNowYou",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.4 },
                    { "Threat", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I think I have heard about enough.",
                FileName = "IThinkIHave",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.4 },
                    { "Threat", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Heard any new humdingers back in town?",
                FileName = "HeardAnyNewHumdingers",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.4 },
                    { "YesNoQuestion", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "It's hotter than a hemroid.",
                FileName = "ItsHotterThanA",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Shalom Amigo.",
                FileName = "ShalomAmigo",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Another inch and I'll skin you alive with a rusty potato peeler.",
                FileName = "AnotherInchAndIll",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Git em up.  Head em out.",
                FileName = "GetUmUpHead",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 1.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Have you been drinking downsream from the herd?",
                FileName = "HaveYouBeenDrinking",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sure as the quickest way to double your money is to fold it over and put it back in yer pocket.",
                FileName = "SureIsTheQuickest",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "When toruble comes to visit do you let it in and offer it a place to sit down?",
                FileName = "WhenTroubleComesTo",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you dig for water under the outhouse?",
                FileName = "DoYouDigFor",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Take a load off, sit for a spell, and tell me what in tarnation is going on.",
                FileName = "TakeALoadOff",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes you get thrown from your horse, you have to get up and get back on.  " +
                            "And sometimes you land on a cactus; and have to roll around and scream in pain for a while first.",
                FileName = "sometimesYouGetThrown",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.2 },
                    { "GiveAdvice", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sometimes I believe I am better off just letting my horse do all the thinking.",
                FileName = "SometimesIBelieveI",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I feel like I just asked the barber if I need a haircut.",
                FileName = "IFeelLikeI",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.1 },
                    { "Exclamation", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I spent the afternoon helping the sherriff to round up a litte posse, Old man hanson is a bit hard of hearing and brought the Madam and three of her girls.  We was sure they wouldn't be much help bringing them russlers to justice, but we was surprised.",
                FileName = "ISpentTheAfternoon",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.2 },
                    { "GiveSurprisingStatement", 0.2 },
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Hemroids and saddles don't mix so well.  I feel like I've been sittin on a bees nest.",
                FileName = "HemroidsAndSaddlesDont",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 },
                    { "GiveRecentHistory", 0.2 },
                    { "RequestAffirmation", 0.2 },
                    { "GiveJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Despair doesn't end your world, neither does pain or getting your butt whooped.  Your world ends when you are dead, and there is a hell of a fight to pitch till that moment.  My dad always tole me you won't get hurt if you stay on top, but you can't be on top every minute of every day.  So you live with the pain.  In a way it reminds you that you are alive.",
                FileName = "DespairDoesntEndYour",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hold your horses.",
                FileName = "HoldYourHorses",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'll bet ya a rusty spur and a 45 shell you can't make me laugh.",
                FileName = "IllBetYouA",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Molly told me I had her biting off more than she could chew caring " +
                            "for that new calf.  I told her it was no problem because her mouth was bigger than she was aware.  She didn't take to kindly to that and proceded" +
                            "to flap them outsized gums with considerable belligerence for near a half hour proving my initial point.",
                FileName = "MollyToldMeI",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Onions can make people cry.  But there hasn't been a vegetable that could make people laugh, till you started opening your mouth.",
                FileName = "OnionsCanMakePeople",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Given your physique, the only reason you should be riding a bull, is if you want to meet a nurse.",
                FileName = "GivenYourPhysiqueThe",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What could they have been doing that was so embarassing they needed to skip town?",
                FileName = "WhatCouldTheyHave",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 },
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Putting their spurs on.",
                FileName = "PuttingTheirSpursOn",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Got a letter from my Isabelle.  It was hard to read.",
                FileName = "GotALetterFrom",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 },
                    { "RequestAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'm due to deliver 46 head to Stillson's and I'm short 7.  " +
                            "Pretty sure it was that good fer nothin brother in law of mine.",
                FileName = "ImDueToDeliver",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "If you're ridin' ahead of the herd, take a look back " +
                            "every now and then to make sure it's still there with ya.",
                FileName = "IfYoureRidingThe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Well, don't squat with yer spurs on.",
                FileName = "WellDontSquatWith",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Nature gave us all something to fall back on, and sooner or later we all land flat on it.",
                FileName = "NatureGaveUsAll",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.1 },
                    { "RequestAdvice", 0.1 },
                    { "GiveMotivation", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "If you find yourself in a hole, the first thing to do is stop digging.",
                FileName = "IfYouFindYourself",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you suppose a man would really light himself a'fire on his parent's doorstep out of sheer pride?",
                FileName = "DoYouSupposeA",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What could possible be going through a bunch of holstien's heads to make em crowd the entry door " +
                            "at the slaughterhouse?",
                FileName = "WhatCouldPossiblyBe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I reckon it was a woman that caused it.",
                FileName = "IReckonItWas",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Could have just been the luck of the chamber pot.",
                FileName = "CouldHaveJustBeen",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I suppose it is the same thing that prevents you from making the horse drink after you lead him to water.",
                FileName = "ISupposeItIs",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "So are you going to tell me where it is?  Or will I have to fill you full of lead?",
                FileName = "SoAreYouGoing",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 },
                    { "YesNoQuestion", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I seem to have misplaced my locket with my sweet Isabelle's picture.",
                FileName = "ISeemToHave",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 },
                    { "RequestAffirmation", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "In yer dreams sweetheart.",
                FileName = "InYerDreamsSweetheart",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 },
                    { "No", 0.1 },
                    { "SmCb_01D", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You might have to head into the city for that.",
                FileName = "YouMightHaveTo",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Somewhere under that smilin umbrella of stars.",
                FileName = "SomewhereUnderThatSmilin",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
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
