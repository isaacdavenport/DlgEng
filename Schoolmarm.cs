using System;
using System.Collections.Generic;


namespace DialogEngine
{
    public class SchoolMarm : Character
    {
        public SchoolMarm() {
            CharacterName = "Eunice";
            CharacterPrefix = "SM";

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "UnInitSchoolmarm",
                FileName = "UnInitSchoolMarm",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Good day",
                FileName = "GoodDay",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.3 },
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "It brings a flush to the cheeks, and one's face may get red too.",
                FileName = "ItBringsAFlush",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Mark my words, I will send a toddler with a perment marker to your home!",
                FileName = "MarkMyWordsI",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "This too shall pass.",
                FileName = "ThisTooShallPass",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 },
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Your behavior is intolerable!",
                FileName = "YourBehaviorIsIntolerable",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.1 },
                    { "Retreat", 0.2 },
                    { "No", 0.2 },
                    { "ShutUp", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I saw Tracy in the garden yesterday.  She exhibited her normal state of pique but her tomatos were distressingly picturesque.  At any rate an unhappy woman with access to weed killer must be watched closely.  So I follwed her to shoe store.  She came out with a pair of saddle shoes and red stilleto heels.  I fear her disquite has blossomed into full multiple personality disorder.",
                FileName = "ISawTracyInThe",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you completed your assignments?",
                FileName = "HaveYouCompletedYour",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Boys will be boys.",
                FileName = "BoysWillBeBoys",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "And what, pray tell, have you been up to?",
                FileName = "AndWhatPrayTell",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I fear my Cassenova will never arrive.",
                FileName = "IFearMyCassenova",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.4 },
                    { "GiveRecentHistory", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You are in need of some discipline young man.",
                FileName = "YouAreInNeed",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "Threat", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Well sir, we shall be on our way.",
                FileName = "WellSirWeShall",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "Retreat", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I am at a loss for words.",
                FileName = "IAmAtA",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 },
                    { "Retreat", 0.01 },
                    { "Yes", 0.01 },
                    { "No", 0.01 },
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
                DialogStr = "Today I read from the unabridged works of deSade.",
                FileName = "TodayIReadFrom",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.1 },
                    { "GiveSurprisingStatement", 0.2 },
                    { "GiveJoke", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You are looking handsome today.",
                FileName = "YouAreLookingHandsome",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You get an A+ today.",
                FileName = "YouGetAnAPlus",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.1 },
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Literacy has taken a real plunge in this country.",
                FileName = "LiteracyHasTakenA",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.6 },
                    { "GiveSurprisingStatement", 0.1 },
                    { "ShutUp", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Your participle is dangling",
                FileName = "YourParticipleIsDangling",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.4 },
                    { "SmCb_01A", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "In a battle of wits you would be unarmed",
                FileName = "InABattleOfWits",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You have my permission to fellatiate that goat over there.",
                FileName = "YouHaveMyPermission",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You are a bigger whore than Hester Prynne",
                FileName = "YoureABiggerWhore",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Even lady Chatterly wouldn't take you as a lover",
                FileName = "EvenLadyChatterlyWouldnt",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Guess what I caught Lisa and Jimmy doing in the bathroom.",
                FileName = "GuessWhatICaught",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what I spent the summer doing?",
                FileName = "DoYouKnowWhatI",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "What was Dicken's favorite activity?  It wasn't writing.",
                FileName = "WhatWasDickensFavorite",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What should I spend my evening on?",
                FileName = "WhatShouldISpend",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 },
                    { "RequestAffirmation", 0.3 },
                    { "SmCb_01C", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Homework",
                FileName = "Homework",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.4 },
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Playing tiddly winks with foreign coins",
                FileName = "PlayingTiddlyWinksWith",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 },
                    { "GiveRecentHistory", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hoping a prince will come",
                FileName = "HopingAPrinceWill",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 },
                    { "GiveRecentHistory", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I could use some comic relief",
                FileName = "ICouldUseSome",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.6 },
                    { "RequestAffirmation", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "The reason the bicycle fell over was it was two tired",
                FileName = "TheReasonTheBicycle",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I really must take leave of you now.",
                FileName = "IReallyMustTake",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "May I be so bold as to join you.",
                FileName = "MayIBeAs",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It wasn't the panda that eats, shoots, and leaves, it was the bank robber",
                FileName = "ItWasntThePanda",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It isn't really so bad deary.",
                FileName = "ItIsntReallySo",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 },
                    { "SmCb_01E", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "When I'm sad, I stick pins in an effigy of Carlos and shout at the top of my lungs.  It always makes me feel better.",
                FileName = "WhenImSadI",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.1 },
                    { "Ramble", 0.3 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Let's take a walk, shall we.",
                FileName = "LetsTakeAWalk",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Let's not make a bigger deal of this than we have to OK?",
                FileName = "LetsNotMakeA",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Would you care to try some of my famous nipples of venus?",
                FileName = "WouldYouCareTo",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Of course",
                FileName = "OfCourse",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Does a childs lip quiver when he sees me reach for my ruler?",
                FileName = "DoesAChildsLip",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do the Lilliputians win the limbo game at every luau?",
                FileName = "DoTheLilliputiansWin",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do I look agreeable?",
                FileName = "DoILookAgreeable",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I assure you I am not that kind of girl.",
                FileName = "IAssureYouI",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "If you look deeply into my eyes perhaps you can see a glimpse of the purgatory that surely awaits you.",
                FileName = "IfYouLookDeeply",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.1 },
                    { "GiveSurprisingStatement", 0.1 },
                    { "YesNoQuestion", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Indeed.",
                FileName = "Indeed",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 },
                    { "No", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what happens to those who tell fibs?",
                FileName = "DoYouKnowWhatHappens",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 },
                    { "Threat", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Who is talking here?",
                FileName = "WhoIsTalkingHere",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Where have you been?",
                FileName = "WhereHaveYouBeen",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Unconscionable.",
                FileName = "Unconscionable",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "The principal is all in a tizzy these days.  Seems the Texas school board has pushed the textbook publishers to remove all references to the state of Alaska ever having existed over some phallic size argument by the respective governors.  ",
                FileName = "ThePrincipalIsAll",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 },
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I beg your pardon.",
                FileName = "IBegYourPardon",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.2 },
                    { "ShutUp", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What, pray tell, are they giggling about?",
                FileName = "WhatPrayTellAre",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.3 },
                    { "RequestAffirmation", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You would think that people talking to one " +
                            "another with zero sense of context would be impossible.  But in reality, people do it all the time.",
                FileName = "YouWouldThinkThat",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "How do you do?",
                FileName = "HowDoYouDo",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 },
                    { "RequestCatchup", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Any mentionable recent exploits?",
                FileName = "AnyMentionableRecentExploits",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Why not tell your friend Eunice what's going on?",
                FileName = "WhyNotTellYour",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Haberdash!",
                FileName = "Haberdash",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Well hello there.",
                FileName = "WellHelloThere",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I hope you step on a Lego.",
                FileName = "IHopeYouStep",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'll not listen to reason.  Reason is always something someone else has to say.",
                FileName = "IllNotListenTo",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you suppose they will allow me to add \"doing well in the shallow end of the gene pool\" as a report card mark?",
                FileName = "DoYouSupposeThey",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do angels have wings?",
                FileName = "DoAngelsHaveWings",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "About as likely as a sucessful 4th grade class free of ritalin, prozac, and valium.",
                FileName = "AboutAsLikelyAs",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "And where have you been off to?",
                FileName = "AndWhereHaveYou",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I know it is tough today, but it really will be better tomorrow.",
                FileName = "IKnowItIsTough",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I do well most times, but there are these moments where I just really miss having a shoulder to lean on.",
                FileName = "IDoWellMost",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "That is absurd.",
                FileName = "ThatIsAbsurd",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I sometimes wonder how a child in my class could have ever become the way they are.  Then we have parent teach conference weeks, and after about 20 hours, I wish I could go back to wondering as opposed to knowing how a kid could get to be a certain way.  And every time I hear a parent say \"I have a great idea\", I have this sinking sensation that am about to lose 2-20 hours of my life to something asenine.",
                FileName = "ISometimesWonderHow",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Seeing back to school ads in July, for a teacher, is like a vammpire seeing a cross.  It burns, dear god, it burns.",
                FileName = "SeeingBackToSchool",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Perhaps I should start dating the janitor, he must be able to sweep women off their feet.",
                FileName = "PerhapsIShouldStart",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Silence!",
                FileName = "Silence",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What does one say to the parents of the anti-christ when they believe his behavior is generated from the excess gluten in the cafeteria lunches?",
                FileName = "WhatDoesOneSay",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "An open, casket funeral for Mr. Smyth?  Why?  I mean could they not close the casket and close the book on that old letch?  Perhaps he died of a viagra overdose and they couldn't close the casket.",
                FileName = "AnOpenCasketFuneral",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You are an artist.  Why I sense you could entertain a room with nothing to play but a rusty trombone.",
                FileName = "YouAreAnArtist",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I would appreciate a full report on your recent activities young man.",
                FileName = "IWouldAppreciateA",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "conjugating the words lie and lay for a class of 8th graders.",
                FileName = "ConjugatingTheWordsLie",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm just not certain.  If I ask outright I am risking everything.",
                FileName = "ImJustNotCertain",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Can a teacher, in good concience, allow the class bully an extra minute or two of harassment after he is entrapped by the entire special needs track team?",
                FileName = "CanATeacherIn",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't know about that, but I do know you never forget to check your pockets for crayons before doing the laundry.",
                FileName = "IDontKnowAbout",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We all have bits of crazy in us, some more than others.  Much, more with some...",
                FileName = "WeAllHaveBits",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Do you know why the third grade boys have started inserting tampons into their bums?",
                FileName = "DoYouKnowWhy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "The nerve of her.  What inspires a seventy two year old middle school principal to run the kissing booth while the kids are on break?",
                FileName = "TheNerveOfHer",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Could be the old green eyed Jealousy.  Or it could be the glassy eyed reefer madness.  Hard to say.",
                FileName = "CouldBeTheOld",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I expect it was similar to the reason one doesn't go lingerie shopping with one's mother.",
                FileName = "IExpectItWas",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "It was spite.  Apparently some people live purely on spite and Marblor Reds.",
                FileName = "ItWasSpiteApparently",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "The same place you left your dignity.",
                FileName = "TheSamePlaceYou",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Try across town.",
                FileName = "TryAcrossTown",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I am certain I would not be familiar with such a location.",
                FileName = "IAmCertainI",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Things edge farther towards intolerability.  Perhaps I will have to relocate.",
                FileName = "ThingsEdgeFartherTowards",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 },
                    { "RequestAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Is there a nail salon around here?  I feel I will just crumble if" +
                            " I have to keep wandering around looking like I just clawed my way out of my own grave.",
                FileName = "IsThereANail",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 },
                    { "RequestAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "If you were my favorite whalebone girdle, where would you be hiding?",
                FileName = "IfYouWereMy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Don't be pigeon livered, get on with it.",
                FileName = "DontBePigeonLivered",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.1 },
                    { "Threat", 0.1 },
                    { "ShutUp", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "I've been hornswoggled!",
                FileName = "IveBeenHornswoggled",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.3 },
                    { "RequestAffirmation", 0.1 },
                    { "RequestAdvice", 0.2 },
                    { "GiveDisbelief", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "What is the matter Johny?",
                FileName = "WhatIsTheMatter",
                phraseWeights = new Dictionary<string, double>{
                    { "LM02A", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "And why would he do that?",
                FileName = "AndWhyWouldHe",
                phraseWeights = new Dictionary<string, double>{
                    { "LM02C", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you owe him two dollars?",
                FileName = "DoYouOweHim",
                phraseWeights = new Dictionary<string, double>{
                    { "LM02E", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Did anyone witness this alleged crime?",
                FileName = "DidAnyoneWitnessThis",
                phraseWeights = new Dictionary<string, double>{
                    { "LM02G", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Well go ask Billy's father if he can corroborate your story.  "+
                            "Better yet, work it out directly with Eric.",
                FileName = "WellGoAskBillys",
                phraseWeights = new Dictionary<string, double>{
                    { "LM02I", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "This furry little monster is yours I take it.  "+
                            "I was initially put off by the smell of this little beast.",
                FileName = "ThisFurryLittleMonster",
                phraseWeights = new Dictionary<string, double>{
                    { "LM15B", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Perhaps so, despite the odor, the warmth and purring when stroked "+
                            "attributes are somewhat endearing, but you must take better care "+
                            "that she doesn't wander off.",
                FileName = "PerhapsSoDespiteThe",
                phraseWeights = new Dictionary<string, double>{
                    { "LM15D", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Johnny, that is very honest of you, I will return this to her this "+
                            "afternoon.  In fact, you deserve a reward.  You can keep one dollar.",
                FileName = "JohnnyThatIsVery",
                phraseWeights = new Dictionary<string, double>{
                    { "LM17B", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
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
