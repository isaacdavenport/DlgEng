using System;
using System.Collections.Generic;


namespace DialogEngine
{
    public class SchoolBoy : Character
    {
        public SchoolBoy() {
            CharacterName = "SchoolBoy";
            CharacterPrefix = "SB";

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "The SchoolBoy has not been initialized.",
                FileName = "TheSchoolboyHasNot",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hi, I'm Johnny",
                FileName = "HiImJohnny",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 1.0 },
                    { PhraseTypes.RequestAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Do you know where babies come from?",
                FileName = "DoYouKnowWhereBabies",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.3 },
                    { PhraseTypes.GiveJoke, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.4 },
                    { PhraseTypes.RequestAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you seen my mommy?",
                FileName = "HaveYouSeenMy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Mommy! Mommy!",
                FileName = "MommyMommy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.2 },
                    { PhraseTypes.No, 0.2 },
                    { PhraseTypes.ShutUp, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Wanna buy my little sister?",
                FileName = "WannaBuyMyLittle",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Golly!",
                FileName = "Golly",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.7 },
                    { PhraseTypes.GiveDisbelief, 0.3 },
                    { PhraseTypes.Yes, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Really?",
                FileName = "Really",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.8 },
                    { PhraseTypes.No, 0.4 },
                    { PhraseTypes.LM10C, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I have a spider",
                FileName = "IHaveASpider",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.Greeting, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My dad can beat up your dad",
                FileName = "MyDadCanBeat",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.1 },
                    { PhraseTypes.Threat, 1.0 },
                    { PhraseTypes.Retreat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Its OK",
                FileName = "ItsOk",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.5 },
                    { PhraseTypes.Yes, 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You won't get a spanking",
                FileName = "YouWontGetA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.4 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.GiveAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Does that hurt?",
                FileName = "DoesThatHurt",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Cut it out!",
                FileName = "CutItOut",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.4 },
                    { PhraseTypes.No, 0.3 },
                    { PhraseTypes.GiveDisbelief, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You're going to get it!",
                FileName = "YoureGonnaGetIt",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm telling on you!",
                FileName = "ImTellingOnYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 },
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What's that smell?",
                FileName = "WhatsThatSmell",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Johnny has been temporarily disabled by the NSA",
                FileName = "JohnnyHasBeenTemporarily",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "My mom said not to wipe my boogers on people, they don't like it.",
                FileName = "MyMomSaidNot",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.1 },
                    { PhraseTypes.Greeting, 0.1 },
                    { PhraseTypes.GiveSurprisingStatement, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = " Today I found a frog.  I tried really hard not to hurt him, but I smooshed his leg.  " +
                            "I think thats why he won't eat the flies I caught for him.",
                FileName = "TodayIFoundA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 },
                    { PhraseTypes.Ramble, 0.4 },
                    { PhraseTypes.GiveRecentHistory, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Are there any mud puddles around here?",
                FileName = "AreThereAnyMud",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't know what to say.",
                FileName = "IDontKnowWhat",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.01 },
                    { PhraseTypes.Retreat, 0.01 },
                    { PhraseTypes.Yes, 0.01 },
                    { PhraseTypes.No, 0.01 },
                    { PhraseTypes.GiveAffirmation, 0.01 },
                    { PhraseTypes.GiveDisbelief, 0.01 },
                    { PhraseTypes.GiveSurprisingStatement, 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You're lying.",
                FileName = "YoureLying",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.6 },
                    { PhraseTypes.GiveSurprisingStatement, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You have bad breath.",
                FileName = "YouHaveBadBreath",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.02 },
                    { PhraseTypes.Retreat, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.4 },
                    { PhraseTypes.ShutUp, 0.2 },
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You're a poo poo head",
                FileName = "YoureAPooPoo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "If you were a triangle you would be obtuse, cause your fat and confusing",
                FileName = "IfYouWereA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You couldn't find your ass with both hands",
                FileName = "YouCouldntFindYour",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why are you so dumb?  Did your mommy drop you?",
                FileName = "WhyAreYouSo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Your mommas so fat...",
                FileName = "YourMommasSoFat",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Guess what I saw my older sister doing.",
                FileName = "GuessWhatISaw",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what my favorite thing is?",
                FileName = "DoYouKnowWhat",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You know what my friend and I were doing?",
                FileName = "YouKnowWhatMy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 },
                    { PhraseTypes.YesNoQuestion, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What were you doing?",
                FileName = "WhatWereYouDoing",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
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
                    { PhraseTypes.GiveActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Oiling a tricycle",
                FileName = "OilingATricycle",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Nose picking",
                FileName = "NosePicking",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Know any Jokes",
                FileName = "KnowAnyJokes",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.8 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You know whats red and green and really fast?  My frog after I put him in the blender.",
                FileName = "YouKnowWhatsRed",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.5 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "When I experience friction in my life, I " +
                            "oil my tricycle wheels.",
                FileName = "WhenIExperienceFriction",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.6 },
                    { PhraseTypes.RequestAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sure.",
                FileName = "Sure",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Making babies.",
                FileName = "MakingBabies",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Awww man.",
                FileName = "AwwwMan",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I know you.",
                FileName = "IKnowYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You better stand back.",
                FileName = "YouBetterStandBack",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I hope your next game fails to save.",
                FileName = "IHopeYourNext",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "OK.  Bye.",
                FileName = "OKBye",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I gotta go home now.",
                FileName = "IGottaGoHome",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Does it hurt to have such a big nose?",
                FileName = "DoesItHurtTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I can't say no to that.",
                FileName = "ICantSayNo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Does it look like it to you, you big jerk?",
                FileName = "DoesItLookLike",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Does a dog have five legs?",
                FileName = "DoesADogHave",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What's going on?",
                FileName = "WhatsGoingOn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe your mom can make it better?",
                FileName = "MaybeYourMom",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't feel so good.",
                FileName = "IDontFeelSo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.2 },
                    { PhraseTypes.RequestAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't think thats right.",
                FileName = "IDontThinkThats",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Mostly I've been been trying to see up Cindy's skirt, but it ain't easy.",
                FileName = "MostlyIveBeenTrying",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what color my boogers have been this week?",
                FileName = "DoYouKnowWhatColor",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 },
                    { PhraseTypes.RequestAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "He had a big gun, and it was in his holster, but he couldn't " +
                            "get it out, and the bad guys was comin, and then the robbers jumped off the roof,but his leg got " +
                            "hurt, and then they all ran in the street and the one guy was draggin his leg and gettin behind, and then...",
                FileName = "HeHadABig",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "This is the part where if your lips keep movin, you're gonna get in trouble.",
                FileName = "ThisIsThePart",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I like Billy, he knows lots of funny stories.  Do you?",
                FileName = "ILikeBillyHe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I made my dad's handkerchief dance today, I put some boogey in it.",
                FileName = "IMadeMyDads",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My little brother knows more than you, and he's three.",
                FileName = "MyLittleBrotherKnows",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Know what my dad told me to do after I went outside?",
                FileName = "KnowWhatMyDad",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What should I do?",
                FileName = "WhatShouldIDo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "My mom is mad at me.",
                FileName = "MyMomIsMad",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sometimes people get mad.  And they just yell.  Then they are quiet for a while.  Then they say they are sorry.",
                FileName = "SometimesPeopleGetMad",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Always tell the truth.",
                FileName = "AlwaysTellTheTruth",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What would make Billy give all his ninja throwing stars to his little sister?",
                FileName = "WhatWouldMakeBilly",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why does mommy and dady's bed sound like a pogo stick sometimes?",
                FileName = "WhyDoesMommyAnd",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe they just wanted to.",
                FileName = "MaybeTheyJustWanted",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Maybe it was painful gas pressure.",
                FileName = "MaybeItWasPainful",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe its the smell.",
                FileName = "MaybeItsTheSmell",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I bet they was afraid of gettin a spankin.",
                FileName = "IBetTheyWas",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I thought I had to fart, but it was more than that.  Now I need to go home.",
                FileName = "IThoughtIHad",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.Retreat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                DialogStr = "Try under the bed with the monsters.",
                PhraseRating = ParentalRating.G,
                FileName = "TryUnderTheBed",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I dug a hole in front of my sisters playhouse and covered it with newspapers.",
                FileName = "IDugAHole",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 },
                    { PhraseTypes.GiveRecentHistory, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know where my tricycle is?",
                FileName = "DoYouKnowWhereMy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            } //TODO replace all of these if-removeAt checks in characters with a foreach at the end 
                Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You can find lots of things under the ocean.",
                FileName = "YouCanFindLots",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 },
                    { PhraseTypes.GiveAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I wanted to play with Billy and Ashley in the tree house but the door was locked" +
                            " and when I said I wanted to play too, Billy threw some underwear at me and said " +
                            "I should get a clue.  I want to play too.  Do you know where I can get a clue?",
                FileName = "IWantedToPlay",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't have two dollars.",
                FileName = "IDontHaveTwo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM01B, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            { 
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't push me you big bully.",
                FileName = "DontPushMeYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM01D, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm telling miss Eunice on you, you hit me!",
                FileName = "ImTellingMissEunice",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM01F, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Eric tried to steal my lunch money.",
                FileName = "EricTriedToSteal",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02B, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "He says I owe him two dollars.",
                FileName = "HeSaysIOwe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02D, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Kind of.  I bet him that he couldn't prove the government lied to us, he said it was easy.",
                FileName = "KindOfIBet",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02F, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I think Billy's dad saw part of it.",
                FileName = "IThinkBillysDad",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM02H, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm supposed to try to work things out with you Eric.",
                FileName = "ImSupposedToTry",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM04A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I can't give you my lunch money, isn't there something else?",
                FileName = "ICantGiveYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM04C, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe I could tell Gretchen how cool you are and she would sit with you.",
                FileName = "MaybeICouldTell",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM04E, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Hi mister Lumbergh.",
                FileName = "HiMrLumbergh",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM05A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Did you see Eric push me in the dirt and try to take my money?",
                FileName = "DidYouSeeEric",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM05C, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Eric tried to take my lunch money and Miss Eunice said I should ask you to corloberlate him.",
                FileName = "EricTriedToTake",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM05E, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Nothing here.  Digging in the sand for money doesn't seem like a very good plan.  " +
                            "I have found five cents so far.  I'm never going to find enough change to pay Eric "+
                            "his two dollars.  Hey!  It looks like the lunch lady just dropped ten dollars!  "+
                            "I could pay Eric and get new wheels for my tricycle!  Or maybe I should tell miss Eunice "+
                            "the lunch lady lost her money.",
                FileName = "NothingHereDiggingIn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM08A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I saw Gretchen sitting next to you at lunch.",
                FileName = "ISawGretchenSitting",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM09A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "It wasn't easy.  I may have stretched the truth about how you rescue animals.",
                FileName = "ItWasntEasyI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM09C, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Are you going to take her to meet kitty?",
                FileName = "AreYouGoingTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM09E, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Oh no.  Its Eric.  Don't hit me.  I don't have two dollars.",
                FileName = "OhNoItsEric",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM10A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I'll be your friend.  As a friend, the first thing you probably want "+
                            "to know is that Miss Eunice has your kitty at the schoolhouse.",
                FileName = "IllBeYourFriend",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM10E, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "That is what friends are for.",
                FileName = "ThatIsWhatFriends",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM10G, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Skylar, Eric says I owe him two dollars, but I don't have two dollars.",
                FileName = "SkylarEricSaysI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM13A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Most of the things that make Eric happy are scary.",
                FileName = "MostOfTheThings",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM13C, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "He likes Gretchen.",
                FileName = "HeLikesGretchen",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM13E, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "He likes his kitty.  He REALLY likes his kitty.",
                FileName = "SB_HeLikesHisKitty",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM13G, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Billy.",
                FileName = "Billy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM14A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Did you check for Billy at the gym?",
                FileName = "DidYouCheckFor",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM14C, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "No, the pokemon gym.",
                FileName = "NoThePokemonGym",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM14E, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "At the schoolhouse there is a gym and depending on his level Billy might want to go there.",
                FileName = "AtTheSchoolhouseThere",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM14G, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Here is your stupid two dollars, you big jerk.",
                FileName = "HereIsYourStupid",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM16A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Miss Eunice, I saw the lunch lady drop this ten dollar bill.",
                FileName = "MissEuniceISaw",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM17A, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Well, I am over halfway there.",
                FileName = "WellIAmOver",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM17C, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

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
