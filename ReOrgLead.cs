using System;
using System.Collections.Generic;


namespace DialogEngine
{

    public class ReOrgLead : Character
    {
        public ReOrgLead() {
            CharacterName = "ReOrgLead";
            CharacterPrefix = "RL";

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "The ReOrgLead has not been initialized.",
                FileName = "TheReOrgLeadHasNot",
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
                DialogStr = "I'm John, here from coorporate.",
                FileName = "ImJohnHereFrom",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Jeeez.",
                FileName = "Jeeez",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "GiveDisbelief", 0.4 },
                    { "Retreat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Again?",
                FileName = "Again",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "GiveDisbelief", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "What the Fuck!",
                FileName = "WhatTheFuck",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Wassup",
                FileName = "Wassup",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "How about those Broncos?",
                FileName = "HowBoutThoseBroncos",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Yo",
                FileName = "Yo",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "We will discuss things later.",
                FileName = "WeWillDiscussThings",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.8 },
                    { "No", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "That is not for this discussion.",
                FileName = "ThatsNotForThis",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 },
                    { "No", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you want to keep your position?",
                FileName = "DoYouWannaKeep",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 1.0 },
                    { "YesNoQuestion", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lets do lunch some time.",
                FileName = "LetsDoLunchSometime",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.3 },
                    { "YesNoQuestion", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Lets go for a beer",
                FileName = "LetsGoForA",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lets do it.",
                FileName = "LetsDoIt",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = " I’ll get back to you on that.",
                FileName = "IllGetBackTo",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.2 },
                    { "No", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Could you pull together more detail on that for me to review later?",
                FileName = "CouldYouPullTogether",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = " I just thought I would stop by….are you busy?",
                FileName = "IJustThoughtId",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "Greeting", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You nailed it.",
                FileName = "YouNailedIt",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.1 },
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "High Five!",
                FileName = "HighFive",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 },
                    { "Yes", 0.2 },
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I am concerned about how I am perceived around the office.",
                FileName = "ImConcernedAboutHow",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Would you be on my personal board of directors?",
                FileName = "WouldYouBeOn",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.3 },
                    { "GiveSurprisingStatement", 0.2 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I thought you were already fired.",
                FileName = "IThoughtYouWere",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 },
                    { "GiveSurprisingStatement", 0.2 }
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
                DialogStr = "Look it isn't that you are a bad person...",
                FileName = "LookItIsntThat",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you know what I heard she does with her weekends?",
                FileName = "DoYouWannaKnow",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Present a 55 slide power point deck",
                FileName = "PresentA55Slide",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Astonishing.  Is that in writing somewhere? I would like to take it to legal.",
                FileName = "AstonishingIsThatIn",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I was ordering some more TPS cover sheets and before that I was checking out the ass on Maggie from accounting. Have you seen it?",
                FileName = "IWasOrderingSome",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.4 },
                    { "YesNoQuestion", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I gave everyone in the deparment a sixty percent raise this morning.",
                FileName = "IGaveEveryoneIn",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We weren't getting the synergy we needed from our existing vendor so we decided to pivot.  Coorporate got involved, which is why I am here.  Now we are rolling out a new strategy based on best practices from some adjacent industries.  There will be downsizing, rightsizing and outsourcing involved, but we will respect the mission and diversity of the teams involved.",
                FileName = "WeWerentGettingThe",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.4 },
                    { "GiveRecentHistory", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Guess what I saw Maggie in accounting doing.",
                FileName = "GuessWhatISaw",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you know what my favorite thing to do before a round of layoffs?",
                FileName = "YouKnowWhatMy",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We are doing a coorporate strengths analysis. Your biggest strength?",
                FileName = "WereDoingACoorporate",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We need to take this offline.",
                FileName = "WeNeedToTakeThis",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Downsizing",
                FileName = "Downsizing",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Prepping for a meeting",
                FileName = "PreppingForAMeeting",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Writing new job descriptions",
                FileName = "WritingNewJobDescriptions",
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
                DialogStr = "Do you know what TLA stands for?",
                FileName = "DoYouKnowWhatTla",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.5 },
                    { "YesNoQuestion", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What I have learned is that we basically have three types " +
                            "of people at the office.  Those who understand math, and those who don't.",
                FileName = "WhatIHaveLearned",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.5 },
                    { "Ramble", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "I think you may have confused me with someone who gives a fuck.",
                FileName = "IThinkYouMay",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 },
                    { "Insult", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I know it is Friday afternoon, but, could you have these ready for my 7AM meeting Monday?",
                FileName = "IKnowItsFriday",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes, I think of all the families that will be affected by this layoff and I can't sleep.",
                FileName = "SometimesIThinkOf",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Can you give me a status update?",
                FileName = "CanYouGiveMe",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "RequestCatchup", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I was in the mens room.  Seems someone didn't replace the paper.",
                FileName = "IWasInThe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "In the most polite and ethnically and culturally " +
                            "sensitive way possible, I would like to suggest that you shut the fuck up.",
                FileName = "InTheMostPolite",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.4 },
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I heard the VP thinks I am too stiff.  I need something to lighten up this next presentation.",
                FileName = "IHeardTheVPThinks",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.4 },
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Wait till coorporate hears about this.",
                FileName = "WaitTillCorporateHears",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Welcome to todays super high intensity training.",
                FileName = "WelcomeToTodaysSuper",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sounds like someone wants to get donkey-punched.",
                FileName = "SoundsLikeSomeoneWants",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I think I will ponder this quietly for a bit.",
                FileName = "IThinkIWill",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you think an offer to buy dinner for Maggie in accounting would be an acceptable consolation to go with her pink slip?",
                FileName = "DoYouThinkAn",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Where is my \"approved\" stamp?",
                FileName = "WhereIsMyApproved",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I am afraid not.",
                FileName = "IAmAfraidNot",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It seems like so long since we have talked.  What have you been up to?",
                FileName = "ItSeemsLikeSo",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't worry.  We all have to allow ourselves some mistakes.",
                FileName = "DontWorryWeAll",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I get this fleeting sense that people might not be nice to me if they didn't report to me.",
                FileName = "IGetThisFleeting",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Oh my.",
                FileName = "OhMy",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "It deeply saddens me, every time I have to re-learn just how much of my life can be drained away by a single personnel issue.",
                FileName = "ItDeeplySaddensMe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Someone call housekeeping, the men's room is in dire need of some attention.",
                FileName = "SomeoneCallHousekeepingThe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I am losing patience with this client.  They had the audacity to ask for idemnification, from us.  Who do they think they are?  I understand that people will try to get whatever they can in contract negotiations, but really it is getting ridiculous in there.",
                FileName = "IAmLosingPatience",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Next time, think BEFORE.. you speak.",
                FileName = "NextTimeThinkBefore",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "That funny smell from the ladies room is still there.  Speaking of funny...",
                FileName = "ThatFunnySmellFrom",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I know people wonder at some of these policies that come from coorporate.  But, " +
                            "what people don't understand is that there " +
                            "has been a lot of outsourcing at coorporate as well.  In fact their soul was outsourced to India last year." +
                            "Which only became a problem when it became reincarnated as Kolkata prostitute.",
                FileName = "IKnowPeopleWonder",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.2 },
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "When I told HR to try recruiting from Wal Mart, I didn't think they would be talking to stockers.",
                FileName = "WhenIToldHR",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I will mop the floor with you.  And don't you worry, I will be able to get into the corners just fine.",
                FileName = "IWillMopThe",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What was the first action item again?",
                FileName = "WhatWasTheFirst",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Globally leveraging next generation niche markets to creat a client centered fistful of core competencies.",
                FileName = "GloballyLeveragingNextGeneration",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "After your boss decides you are a homosexual and that your safety process presentation was a thinly guised sexual proposition, is there anything that can be done to save your career?",
                FileName = "AfterYourBossDecides",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Any insights into the Finkelman-Stapler-Data Dump-Harassment situation?",
                FileName = "AnyInsightsIntoThe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'm going to head out for the weekend, send me a text if you need any info to finish those reports.",
                FileName = "ImGonnaHeadOut",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes you have to go back to the drawing board for a more creative solution.",
                FileName = "SometimesYouHaveTo",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'll bet you could look in petty cash and find something.",
                FileName = "IllBetYouCould",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes you can't take everything in a critique literaly.",
                FileName = "SometimesYouCantTake",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Have you seen their weak attempt at covering up the petty cash expenditures?  They used white out for gods sake.  Who uses white out anymore.  What would drive someone to attempt to cover their inappropriate seafood purchases with whiteout?",
                FileName = "HaveYouSeenTheir",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Have you tried the conference room?",
                FileName = "HaveYouTriedThe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Try next door on the second story above the adult toy store.",
                FileName = "TryNextDoorOn",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Where all the other backstabbers hang out perhaps?",
                FileName = "WhereAllTheOther",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 },
                    { "YesNoQuestion", 0.05 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Where is everyone?  It is only five fifteen.",
                FileName = "WhereIsEveryone",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you seen Maggie?  ",
                FileName = "HaveYouSeenMaggie",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "This conversation is no longer productive.",
                FileName = "ThisConversationIsNo",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.7 },
                    { "ShutUp", 0.7 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why would anyone accept an assignment to Des Moine?",
                FileName = "WhyWouldAnyoneAccept",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes it is hard to divine someone's motivations.  I mean we can't necessarily fathom just how far a fear of public speaking or love of Jusin Bieber will take a person.",
                FileName = "SometimesItsHardTo",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes people are just responding to the incentives they are presented with.",
                FileName = "SometimesPeopleAreJust",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
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
