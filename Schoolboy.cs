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
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hi, I'm Johnny",
                FileName = "HiImJohnny",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 1.0 },
                    { "RequestAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Do you know where babies come from?",
                FileName = "DoYouKnowWhereBabies",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.3 },
                    { "GiveJoke", 0.2 },
                    { "YesNoQuestion", 0.4 },
                    { "RequestAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you seen my mommy?",
                FileName = "HaveYouSeenMy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Mommy! Mommy!",
                FileName = "MommyMommy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "No", 0.2 },
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Wanna buy my little sister?",
                FileName = "WannaBuyMyLittle",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "GiveSurprisingStatement", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Golly!",
                FileName = "Golly",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.7 },
                    { "GiveDisbelief", 0.3 },
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Really?",
                FileName = "Really",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.8 },
                    { "No", 0.4 },
                    { "LM10C", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I have a spider",
                FileName = "IHaveASpider",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 },
                    { "Greeting", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My dad can beat up your dad",
                FileName = "MyDadCanBeat",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.1 },
                    { "Threat", 1.0 },
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Its OK",
                FileName = "ItsOk",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.5 },
                    { "Yes", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You won't get a spanking",
                FileName = "YouWontGetA",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 },
                    { "GiveSurprisingStatement", 0.2 },
                    { "GiveAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Does that hurt?",
                FileName = "DoesThatHurt",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Cut it out!",
                FileName = "CutItOut",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.4 },
                    { "No", 0.3 },
                    { "GiveDisbelief", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You're going to get it!",
                FileName = "YoureGonnaGetIt",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm telling on you!",
                FileName = "ImTellingOnYou",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 },
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What's that smell?",
                FileName = "WhatsThatSmell",
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
                DialogStr = "Johnny has been temporarily disabled by the NSA",
                FileName = "JohnnyHasBeenTemporarily",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 },
                    { "GiveSurprisingStatement", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "My mom said not to wipe my boogers on people, they don't like it.",
                FileName = "MyMomSaidNot",
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
                DialogStr = " Today I found a frog.  I tried really hard not to hurt him, but I smooshed his leg.  " +
                            "I think thats why he won't eat the flies I caught for him.",
                FileName = "TodayIFoundA",
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
                DialogStr = "Are there any mud puddles around here?",
                FileName = "AreThereAnyMud",
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
                DialogStr = "I don't know what to say.",
                FileName = "IDontKnowWhat",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 },
                    { "Retreat", 0.01 },
                    { "Yes", 0.01 },
                    { "No", 0.01 },
                    { "GiveAffirmation", 0.01 },
                    { "GiveDisbelief", 0.01 },
                    { "GiveSurprisingStatement", 0.01 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You're lying.",
                FileName = "YoureLying",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.6 },
                    { "GiveSurprisingStatement", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You have bad breath.",
                FileName = "YouHaveBadBreath",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.02 },
                    { "Retreat", 0.2 },
                    { "GiveSurprisingStatement", 0.4 },
                    { "ShutUp", 0.2 },
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You're a poo poo head",
                FileName = "YoureAPooPoo",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "If you were a triangle you would be obtuse, cause your fat and confusing",
                FileName = "IfYouWereA",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You couldn't find your ass with both hands",
                FileName = "YouCouldntFindYour",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why are you so dumb?  Did your mommy drop you?",
                FileName = "WhyAreYouSo",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Your mommas so fat...",
                FileName = "YourMommasSoFat",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Guess what I saw my older sister doing.",
                FileName = "GuessWhatISaw",
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
                DialogStr = "You know what my friend and I were doing?",
                FileName = "YouKnowWhatMy",
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
                DialogStr = "What were you doing?",
                FileName = "WhatWereYouDoing",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
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
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Oiling a tricycle",
                FileName = "OilingATricycle",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Nose picking",
                FileName = "NosePicking",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Know any Jokes",
                FileName = "KnowAnyJokes",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.8 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You know whats red and green and really fast?  My frog after I put him in the blender.",
                FileName = "YouKnowWhatsRed",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.5 },
                    { "GiveSurprisingStatement", 0.2 }
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
                DialogStr = "Sure.",
                FileName = "Sure",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Making babies.",
                FileName = "MakingBabies",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Awww man.",
                FileName = "AwwwMan",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I know you.",
                FileName = "IKnowYou",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You better stand back.",
                FileName = "YouBetterStandBack",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I hope your next game fails to save.",
                FileName = "IHopeYourNext",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "OK.  Bye.",
                FileName = "OKBye",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I gotta go home now.",
                FileName = "IGottaGoHome",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Does it hurt to have such a big nose?",
                FileName = "DoesItHurtTo",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I can't say no to that.",
                FileName = "ICantSayNo",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Does it look like it to you, you big jerk?",
                FileName = "DoesItLookLike",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Does a dog have five legs?",
                FileName = "DoesADogHave",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What's going on?",
                FileName = "WhatsGoingOn",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe your mom can make it better?",
                FileName = "MaybeYourMom",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't feel so good.",
                FileName = "IDontFeelSo",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "RequestAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't think thats right.",
                FileName = "IDontThinkThats",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Mostly I've been been trying to see up Cindy's skirt, but it ain't easy.",
                FileName = "MostlyIveBeenTrying",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know what color my boogers have been this week?",
                FileName = "DoYouKnowWhatColor",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 },
                    { "YesNoQuestion", 0.2 },
                    { "RequestAdvice", 0.1 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "This is the part where if your lips keep movin, you're gonna get in trouble.",
                FileName = "ThisIsThePart",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I like Billy, he knows lots of funny stories.  Do you?",
                FileName = "ILikeBillyHe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I made my dad's handkerchief dance today, I put some boogey in it.",
                FileName = "IMadeMyDads",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My little brother knows more than you, and he's three.",
                FileName = "MyLittleBrotherKnows",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Know what my dad told me to do after I went outside?",
                FileName = "KnowWhatMyDad",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What should I do?",
                FileName = "WhatShouldIDo",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "My mom is mad at me.",
                FileName = "MyMomIsMad",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sometimes people get mad.  And they just yell.  Then they are quiet for a while.  Then they say they are sorry.",
                FileName = "SometimesPeopleGetMad",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Always tell the truth.",
                FileName = "AlwaysTellTheTruth",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What would make Billy give all his ninja throwing stars to his little sister?",
                FileName = "WhatWouldMakeBilly",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why does mommy and dady's bed sound like a pogo stick sometimes?",
                FileName = "WhyDoesMommyAnd",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe they just wanted to.",
                FileName = "MaybeTheyJustWanted",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Maybe it was painful gas pressure.",
                FileName = "MaybeItWasPainful",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe its the smell.",
                FileName = "MaybeItsTheSmell",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I bet they was afraid of gettin a spankin.",
                FileName = "IBetTheyWas",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I thought I had to fart, but it was more than that.  Now I need to go home.",
                FileName = "IThoughtIHad",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 },
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                DialogStr = "Try under the bed with the monsters.",
                PhraseRating = ParentalRating.G,
                FileName = "TryUnderTheBed",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I dug a hole in front of my sisters playhouse and covered it with newspapers.",
                FileName = "IDugAHole",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 },
                    { "GiveRecentHistory", 0.2 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Do you know where my tricycle is?",
                FileName = "DoYouKnowWhereMy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            } //TODO replace all of these if-removeAt checks in characters with a foreach at the end 
                Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You can find lots of things under the ocean.",
                FileName = "YouCanFindLots",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 },
                    { "GiveAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I wanted to play with Billy and Ashley in the tree house but the door was locked" +
                            " and when I said I wanted to play too, Billy threw some underwear at me and said I should get a " +
                            "clue.  I want to play too.  Do you know where I can get a clue?",
                FileName = "IWantedToPlay",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 },
                    { "YesNoQuestion", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM01B", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM01D", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM01F", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM02B", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM02D", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM02F", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM02H", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM04A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM04C", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM04E", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM05A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM05C", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM05E", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM08A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM09A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM09C", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM09E", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM10A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM10E", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM10G", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM13A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM13C", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM13E", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM13G", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM14A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM14C", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM14E", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM14G", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM16A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM17A", 0.2 }
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
                phraseWeights = new Dictionary<string, double>{
                    { "LM17C", 0.2 }
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
