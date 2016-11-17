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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Good day",
                FileName = "GoodDay",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.3 },
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "It brings a flush to the cheeks, and one's face may get red too.",
                FileName = "ItBringsAFlush",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.4 },
                    { PhraseTypes.Exclamation, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Mark my words, I will send a toddler with a perment marker to your home!",
                FileName = "MarkMyWordsI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "This too shall pass.",
                FileName = "ThisTooShallPass",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 },
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Your behavior is intolerable!",
                FileName = "YourBehaviorIsIntolerable",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.1 },
                    { PhraseTypes.Retreat, 0.2 },
                    { PhraseTypes.No, 0.2 },
                    { PhraseTypes.ShutUp, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I saw Tracy in the garden yesterday.  She exhibited her normal state of pique but her tomatos were distressingly picturesque.  At any rate an unhappy woman with access to weed killer must be watched closely.  So I follwed her to shoe store.  She came out with a pair of saddle shoes and red stilleto heels.  I fear her disquite has blossomed into full multiple personality disorder.",
                FileName = "ISawTracyInThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you completed your assignments?",
                FileName = "HaveYouCompletedYour",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Boys will be boys.",
                FileName = "BoysWillBeBoys",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "And what, pray tell, have you been up to?",
                FileName = "AndWhatPrayTell",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I fear my Cassenova will never arrive.",
                FileName = "IFearMyCassenova",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.4 },
                    { PhraseTypes.GiveRecentHistory, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You are in need of some discipline young man.",
                FileName = "YouAreInNeed",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.Threat, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Well sir, we shall be on our way.",
                FileName = "WellSirWeShall",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.Retreat, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I am at a loss for words.",
                FileName = "IAmAtA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.01 },
                    { PhraseTypes.Retreat, 0.01 },
                    { PhraseTypes.Yes, 0.01 },
                    { PhraseTypes.No, 0.01 },
                    { PhraseTypes.GiveAffirmation, 0.01 },
                    { PhraseTypes.RequestAffirmation, 0.01 },
                    { PhraseTypes.GiveDisbelief, 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Today I read from the unabridged works of deSade.",
                FileName = "TodayIReadFrom",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.1 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.GiveJoke, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You are looking handsome today.",
                FileName = "YouAreLookingHandsome",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You get an A+ today.",
                FileName = "YouGetAnAPlus",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.1 },
                    { PhraseTypes.Retreat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Literacy has taken a real plunge in this country.",
                FileName = "LiteracyHasTakenA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.6 },
                    { PhraseTypes.GiveSurprisingStatement, 0.1 },
                    { PhraseTypes.ShutUp, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Your participle is dangling",
                FileName = "YourParticipleIsDangling",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.4 },
                    { PhraseTypes.SmCb_01A, 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "In a battle of wits you would be unarmed",
                FileName = "InABattleOfWits",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You have my permission to fellatiate that goat over there.",
                FileName = "YouHaveMyPermission",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You are a bigger whore than Hester Prynne",
                FileName = "YoureABiggerWhore",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Even lady Chatterly wouldn't take you as a lover",
                FileName = "EvenLadyChatterlyWouldnt",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Guess what I caught Lisa and Jimmy doing in the bathroom.",
                FileName = "GuessWhatICaught",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what I spent the summer doing?",
                FileName = "DoYouKnowWhatI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "What was Dicken's favorite activity?  It wasn't writing.",
                FileName = "WhatWasDickensFavorite",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What should I spend my evening on?",
                FileName = "WhatShouldISpend",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 },
                    { PhraseTypes.RequestAffirmation, 0.3 },
                    { PhraseTypes.SmCb_01C, 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Homework",
                FileName = "Homework",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.4 },
                    { PhraseTypes.GiveActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Playing tiddly winks with foreign coins",
                FileName = "PlayingTiddlyWinksWith",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.4 },
                    { PhraseTypes.GiveRecentHistory, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hoping a prince will come",
                FileName = "HopingAPrinceWill",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.4 },
                    { PhraseTypes.GiveRecentHistory, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I could use some comic relief",
                FileName = "ICouldUseSome",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.6 },
                    { PhraseTypes.RequestAffirmation, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "The reason the bicycle fell over was it was two tired",
                FileName = "TheReasonTheBicycle",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I really must take leave of you now.",
                FileName = "IReallyMustTake",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "May I be so bold as to join you.",
                FileName = "MayIBeAs",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It wasn't the panda that eats, shoots, and leaves, it was the bank robber",
                FileName = "ItWasntThePanda",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It isn't really so bad deary.",
                FileName = "ItIsntReallySo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.4 },
                    { PhraseTypes.SmCb_01E, 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "When I'm sad, I stick pins in an effigy of Carlos and shout at the top of my lungs.  It always makes me feel better.",
                FileName = "WhenImSadI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.1 },
                    { PhraseTypes.Ramble, 0.3 },
                    { PhraseTypes.GiveSurprisingStatement, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Let's take a walk, shall we.",
                FileName = "LetsTakeAWalk",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Let's not make a bigger deal of this than we have to OK?",
                FileName = "LetsNotMakeA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Would you care to try some of my famous nipples of venus?",
                FileName = "WouldYouCareTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.4 },
                    { PhraseTypes.GiveSurprisingStatement, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Of course",
                FileName = "OfCourse",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Does a childs lip quiver when he sees me reach for my ruler?",
                FileName = "DoesAChildsLip",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do the Lilliputians win the limbo game at every luau?",
                FileName = "DoTheLilliputiansWin",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do I look agreeable?",
                FileName = "DoILookAgreeable",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I assure you I am not that kind of girl.",
                FileName = "IAssureYouI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "If you look deeply into my eyes perhaps you can see a glimpse of the purgatory that surely awaits you.",
                FileName = "IfYouLookDeeply",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.1 },
                    { PhraseTypes.GiveSurprisingStatement, 0.1 },
                    { PhraseTypes.YesNoQuestion, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Indeed.",
                FileName = "Indeed",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.4 },
                    { PhraseTypes.No, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what happens to those who tell fibs?",
                FileName = "DoYouKnowWhatHappens",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.4 },
                    { PhraseTypes.Threat, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Who is talking here?",
                FileName = "WhoIsTalkingHere",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Where have you been?",
                FileName = "WhereHaveYouBeen",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Unconscionable.",
                FileName = "Unconscionable",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "The principal is all in a tizzy these days.  Seems the Texas school board has pushed the textbook publishers to remove all references to the state of Alaska ever having existed over some phallic size argument by the respective governors.  ",
                FileName = "ThePrincipalIsAll",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.4 },
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I beg your pardon.",
                FileName = "IBegYourPardon",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.2 },
                    { PhraseTypes.ShutUp, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What, pray tell, are they giggling about?",
                FileName = "WhatPrayTellAre",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.3 },
                    { PhraseTypes.RequestAffirmation, 0.3 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "How do you do?",
                FileName = "HowDoYouDo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.4 },
                    { PhraseTypes.RequestCatchup, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Any mentionable recent exploits?",
                FileName = "AnyMentionableRecentExploits",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Why not tell your friend Eunice what's going on?",
                FileName = "WhyNotTellYour",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Haberdash!",
                FileName = "Haberdash",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Well hello there.",
                FileName = "WellHelloThere",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I hope you step on a Lego.",
                FileName = "IHopeYouStep",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'll not listen to reason.  Reason is always something someone else has to say.",
                FileName = "IllNotListenTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you suppose they will allow me to add \"doing well in the shallow end of the gene pool\" as a report card mark?",
                FileName = "DoYouSupposeThey",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do angels have wings?",
                FileName = "DoAngelsHaveWings",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "About as likely as a sucessful 4th grade class free of ritalin, prozac, and valium.",
                FileName = "AboutAsLikelyAs",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "And where have you been off to?",
                FileName = "AndWhereHaveYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I know it is tough today, but it really will be better tomorrow.",
                FileName = "IKnowItIsTough",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I do well most times, but there are these moments where I just really miss having a shoulder to lean on.",
                FileName = "IDoWellMost",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "That is absurd.",
                FileName = "ThatIsAbsurd",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I sometimes wonder how a child in my class could have ever become the way they are.  Then we have parent teach conference weeks, and after about 20 hours, I wish I could go back to wondering as opposed to knowing how a kid could get to be a certain way.  And every time I hear a parent say \"I have a great idea\", I have this sinking sensation that am about to lose 2-20 hours of my life to something asenine.",
                FileName = "ISometimesWonderHow",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Seeing back to school ads in July, for a teacher, is like a vammpire seeing a cross.  It burns, dear god, it burns.",
                FileName = "SeeingBackToSchool",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Perhaps I should start dating the janitor, he must be able to sweep women off their feet.",
                FileName = "PerhapsIShouldStart",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Silence!",
                FileName = "Silence",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What does one say to the parents of the anti-christ when they believe his behavior is generated from the excess gluten in the cafeteria lunches?",
                FileName = "WhatDoesOneSay",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "An open, casket funeral for Mr. Smyth?  Why?  I mean could they not close the casket and close the book on that old letch?  Perhaps he died of a viagra overdose and they couldn't close the casket.",
                FileName = "AnOpenCasketFuneral",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You are an artist.  Why I sense you could entertain a room with nothing to play but a rusty trombone.",
                FileName = "YouAreAnArtist",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I would appreciate a full report on your recent activities young man.",
                FileName = "IWouldAppreciateA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "conjugating the words lie and lay for a class of 8th graders.",
                FileName = "ConjugatingTheWordsLie",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm just not certain.  If I ask outright I am risking everything.",
                FileName = "ImJustNotCertain",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Can a teacher, in good concience, allow the class bully an extra minute or two of harassment after he is entrapped by the entire special needs track team?",
                FileName = "CanATeacherIn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't know about that, but I do know you never forget to check your pockets for crayons before doing the laundry.",
                FileName = "IDontKnowAbout",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We all have bits of crazy in us, some more than others.  Much, more with some...",
                FileName = "WeAllHaveBits",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Do you know why the third grade boys have started inserting tampons into their bums?",
                FileName = "DoYouKnowWhy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "The nerve of her.  What inspires a seventy two year old middle school principal to run the kissing booth while the kids are on break?",
                FileName = "TheNerveOfHer",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Could be the old green eyed Jealousy.  Or it could be the glassy eyed reefer madness.  Hard to say.",
                FileName = "CouldBeTheOld",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I expect it was similar to the reason one doesn't go lingerie shopping with one's mother.",
                FileName = "IExpectItWas",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "It was spite.  Apparently some people live purely on spite and Marblor Reds.",
                FileName = "ItWasSpiteApparently",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "The same place you left your dignity.",
                FileName = "TheSamePlaceYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Try across town.",
                FileName = "TryAcrossTown",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I am certain I would not be familiar with such a location.",
                FileName = "IAmCertainI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Things edge farther towards intolerability.  Perhaps I will have to relocate.",
                FileName = "ThingsEdgeFartherTowards",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 },
                    { PhraseTypes.RequestAffirmation, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 },
                    { PhraseTypes.RequestAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "If you were my favorite whalebone girdle, where would you be hiding?",
                FileName = "IfYouWereMy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.1 },
                    { PhraseTypes.Threat, 0.1 },
                    { PhraseTypes.ShutUp, 0.1 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.3 },
                    { PhraseTypes.RequestAffirmation, 0.1 },
                    { PhraseTypes.RequestAdvice, 0.2 },
                    { PhraseTypes.GiveDisbelief, 0.1 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02A, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02C, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02E, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02G, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02I, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM15B, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM15D, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM17B, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            PhraseTotals.DialogStr = "PhraseTotals";
            PhraseTotals.FileName = "No Audio Filename Associated with PhraseTotals";
            PhraseTotals.PhraseWeights = new Dictionary<PhraseTypes, double>();

            foreach (PhraseTypes item in Enum.GetValues(typeof (PhraseTypes))) {
                PhraseTotals.PhraseWeights.Add(item, 0.0f);
            }

            foreach (PhraseEntry pentry in Phrases) {
                foreach (var entry in pentry.PhraseWeights) {
                    PhraseTotals.PhraseWeights[entry.Key] += entry.Value;
                }
            }

            // initialize  RecentPhrases with the 0 phrase uninitialized so initial phrases aren't blocked while 
            //que fills since there is no way to prevent que from growing and dump old phrases into oblivion we must manually trim
            for (int k = 0; k < RecentPhrasesQueueSize; k++)
                RecentPhrases.Enqueue(Phrases[0]);
        }
    }
}
