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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.01 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.4 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.4 },
                    { PhraseTypes.GiveDisbelief, 0.4 },
                    { PhraseTypes.Retreat, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Again?",
                FileName = "Again",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.4 },
                    { PhraseTypes.GiveDisbelief, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "What the Fuck!",
                FileName = "WhatTheFuck",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Wassup",
                FileName = "Wassup",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "How about those Broncos?",
                FileName = "HowBoutThoseBroncos",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Yo",
                FileName = "Yo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "We will discuss things later.",
                FileName = "WeWillDiscussThings",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.8 },
                    { PhraseTypes.No, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "That is not for this discussion.",
                FileName = "ThatsNotForThis",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 },
                    { PhraseTypes.No, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you want to keep your position?",
                FileName = "DoYouWannaKeep",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 1.0 },
                    { PhraseTypes.YesNoQuestion, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lets do lunch some time.",
                FileName = "LetsDoLunchSometime",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.3 },
                    { PhraseTypes.YesNoQuestion, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Lets go for a beer",
                FileName = "LetsGoForA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.4 },
                    { PhraseTypes.RequestCatchup, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lets do it.",
                FileName = "LetsDoIt",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = " I’ll get back to you on that.",
                FileName = "IllGetBackTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.2 },
                    { PhraseTypes.No, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Could you pull together more detail on that for me to review later?",
                FileName = "CouldYouPullTogether",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = " I just thought I would stop by….are you busy?",
                FileName = "IJustThoughtId",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 },
                    { PhraseTypes.Greeting, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You nailed it.",
                FileName = "YouNailedIt",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.1 },
                    { PhraseTypes.Yes, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "High Five!",
                FileName = "HighFive",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 },
                    { PhraseTypes.Yes, 0.2 },
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I am concerned about how I am perceived around the office.",
                FileName = "ImConcernedAboutHow",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Would you be on my personal board of directors?",
                FileName = "WouldYouBeOn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.3 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I thought you were already fired.",
                FileName = "IThoughtYouWere",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
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
                DialogStr = "Look it isn't that you are a bad person...",
                FileName = "LookItIsntThat",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you know what I heard she does with her weekends?",
                FileName = "DoYouWannaKnow",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Present a 55 slide power point deck",
                FileName = "PresentA55Slide",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Astonishing.  Is that in writing somewhere? I would like to take it to legal.",
                FileName = "AstonishingIsThatIn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I was ordering some more TPS cover sheets and before that I was checking out the ass on Maggie from accounting. Have you seen it?",
                FileName = "IWasOrderingSome",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.4 },
                    { PhraseTypes.YesNoQuestion, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I gave everyone in the deparment a sixty percent raise this morning.",
                FileName = "IGaveEveryoneIn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We weren't getting the synergy we needed from our existing vendor so we decided to pivot.  Coorporate got involved, which is why I am here.  Now we are rolling out a new strategy based on best practices from some adjacent industries.  There will be downsizing, rightsizing and outsourcing involved, but we will respect the mission and diversity of the teams involved.",
                FileName = "WeWerentGettingThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.4 },
                    { PhraseTypes.GiveRecentHistory, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Guess what I saw Maggie in accounting doing.",
                FileName = "GuessWhatISaw",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you know what my favorite thing to do before a round of layoffs?",
                FileName = "YouKnowWhatMy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We are doing a coorporate strengths analysis. Your biggest strength?",
                FileName = "WereDoingACoorporate",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "We need to take this offline.",
                FileName = "WeNeedToTakeThis",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Downsizing",
                FileName = "Downsizing",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Prepping for a meeting",
                FileName = "PreppingForAMeeting",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Writing new job descriptions",
                FileName = "WritingNewJobDescriptions",
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
                DialogStr = "Do you know what TLA stands for?",
                FileName = "DoYouKnowWhatTla",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.5 },
                    { PhraseTypes.YesNoQuestion, 0.5 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.5 },
                    { PhraseTypes.Ramble, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "I think you may have confused me with someone who gives a fuck.",
                FileName = "IThinkYouMay",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.4 },
                    { PhraseTypes.Insult, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I know it is Friday afternoon, but, could you have these ready for my 7AM meeting Monday?",
                FileName = "IKnowItsFriday",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes, I think of all the families that will be affected by this layoff and I can't sleep.",
                FileName = "SometimesIThinkOf",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Can you give me a status update?",
                FileName = "CanYouGiveMe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.4 },
                    { PhraseTypes.RequestCatchup, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I was in the mens room.  Seems someone didn't replace the paper.",
                FileName = "IWasInThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.4 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.4 },
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I heard the VP thinks I am too stiff.  I need something to lighten up this next presentation.",
                FileName = "IHeardTheVPThinks",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.4 },
                    { PhraseTypes.RequestAffirmation, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Wait till coorporate hears about this.",
                FileName = "WaitTillCorporateHears",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Welcome to todays super high intensity training.",
                FileName = "WelcomeToTodaysSuper",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Sounds like someone wants to get donkey-punched.",
                FileName = "SoundsLikeSomeoneWants",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I think I will ponder this quietly for a bit.",
                FileName = "IThinkIWill",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you think an offer to buy dinner for Maggie in accounting would be an acceptable consolation to go with her pink slip?",
                FileName = "DoYouThinkAn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Where is my \"approved\" stamp?",
                FileName = "WhereIsMyApproved",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I am afraid not.",
                FileName = "IAmAfraidNot",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It seems like so long since we have talked.  What have you been up to?",
                FileName = "ItSeemsLikeSo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't worry.  We all have to allow ourselves some mistakes.",
                FileName = "DontWorryWeAll",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I get this fleeting sense that people might not be nice to me if they didn't report to me.",
                FileName = "IGetThisFleeting",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Oh my.",
                FileName = "OhMy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveDisbelief, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "It deeply saddens me, every time I have to re-learn just how much of my life can be drained away by a single personnel issue.",
                FileName = "ItDeeplySaddensMe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Someone call housekeeping, the men's room is in dire need of some attention.",
                FileName = "SomeoneCallHousekeepingThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I am losing patience with this client.  They had the audacity to ask for idemnification, from us.  Who do they think they are?  I understand that people will try to get whatever they can in contract negotiations, but really it is getting ridiculous in there.",
                FileName = "IAmLosingPatience",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Next time, think BEFORE.. you speak.",
                FileName = "NextTimeThinkBefore",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "That funny smell from the ladies room is still there.  Speaking of funny...",
                FileName = "ThatFunnySmellFrom",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
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
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.2 },
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "When I told HR to try recruiting from Wal Mart, I didn't think they would be talking to stockers.",
                FileName = "WhenIToldHR",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I will mop the floor with you.  And don't you worry, I will be able to get into the corners just fine.",
                FileName = "IWillMopThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What was the first action item again?",
                FileName = "WhatWasTheFirst",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Globally leveraging next generation niche markets to creat a client centered fistful of core competencies.",
                FileName = "GloballyLeveragingNextGeneration",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "After your boss decides you are a homosexual and that your safety process presentation was a thinly guised sexual proposition, is there anything that can be done to save your career?",
                FileName = "AfterYourBossDecides",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Any insights into the Finkelman-Stapler-Data Dump-Harassment situation?",
                FileName = "AnyInsightsIntoThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'm going to head out for the weekend, send me a text if you need any info to finish those reports.",
                FileName = "ImGonnaHeadOut",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes you have to go back to the drawing board for a more creative solution.",
                FileName = "SometimesYouHaveTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'll bet you could look in petty cash and find something.",
                FileName = "IllBetYouCould",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes you can't take everything in a critique literaly.",
                FileName = "SometimesYouCantTake",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Have you seen their weak attempt at covering up the petty cash expenditures?  They used white out for gods sake.  Who uses white out anymore.  What would drive someone to attempt to cover their inappropriate seafood purchases with whiteout?",
                FileName = "HaveYouSeenTheir",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Have you tried the conference room?",
                FileName = "HaveYouTriedThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Try next door on the second story above the adult toy store.",
                FileName = "TryNextDoorOn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Where all the other backstabbers hang out perhaps?",
                FileName = "WhereAllTheOther",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.05 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Where is everyone?  It is only five fifteen.",
                FileName = "WhereIsEveryone",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you seen Maggie?  ",
                FileName = "HaveYouSeenMaggie",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "This conversation is no longer productive.",
                FileName = "ThisConversationIsNo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.7 },
                    { PhraseTypes.ShutUp, 0.7 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why would anyone accept an assignment to Des Moine?",
                FileName = "WhyWouldAnyoneAccept",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes it is hard to divine someone's motivations.  I mean we can't necessarily fathom "
                + "just how far a fear of public speaking or love of Jusin Bieber will take a person.",
                FileName = "SometimesItsHardTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes people are just responding to the incentives they are presented with.",
                FileName = "SometimesPeopleAreJust",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Here I am at the schoolhouse.",
                FileName = "HereIAmAt",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.AtSchoolhouse, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "How did Johnny get your two dollars?  You probably have twenty five pounds on the kid.",
                FileName = "HowDidJohnnyGet",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM03B, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Does Johnny agree he owes you two dollars?",
                FileName = "DoesJohnnyAgreeHe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM03D, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Yes, thats not much of a challenge.  But I am busy looking for Billy, have you seen him?",
                FileName = "YesThatsNotMuch",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM03F, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Well you are welcome to go with me to the school house and help me find Billy.  Have you " + 
                  "looked for kitty there yet?    Or I suppose you could go ask Skylar the cat about your problem with Johnny.",
                FileName = "WellYouAreWelcome",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM03H, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Whats up Johnny?",
                FileName = "WhatsUpJohnny",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM05B, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "It looked like you guys were playing a bit rough over there.",
                FileName = "ItLookedLikeYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM05D, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Right.  Well I don't know what you two were up to, so I can't verify anything.  Have you tried " +
                    "working things out with Eric ? Otherwise you could talk to Skylar the cat.He sometimes has good insights " +
                    "on such matters.Or you could give me a hand trying to find Billy, he was supposed to be here 20 minutes ago.",
                FileName = "RightWellIDont",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM05F, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Where is that kid?",
                FileName = "WhereIsThatKid",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM14B, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Billy is to young to be working out.  He won't be needing abs of steel for at least seven or eight years.",
                FileName = "BillyIsTooYoung",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM14D, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "What are you talking about?",
                FileName = "WhatAreYouTalking",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM14F, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Billy what are you doing back at school.  It doesn't look like you are studying.  " +
                    "Put down that phone when I am talking to you young man.  " +
                    "Your phone has nothing to do with evolution, devolution is more like it.",
                FileName = "BillyWhatAreYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.LM18A, 0.2 }
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
