using System;
using System.Collections.Generic;


namespace DialogEngine
{
    public class Witch : Character
    {
        public Witch() {
            CharacterName = "Hazel";
            CharacterPrefix = "HZ";

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Cackle",
                FileName = "Cackle",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.05 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Come talk for a spell",
                FileName = "Howdy",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1); 
                
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Abracadabra!",
                FileName = "TheseSpursAintAfraid",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.Threat, 0.5 },
                    { PhraseTypes.GiveSurprisingStatement, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                DialogStr = "Have your spells been working out lately?",
                PhraseRating = ParentalRating.G,
                FileName = "LetsUsHunkerDown",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 },
                    { PhraseTypes.RequestCatchup, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Screechin' spiders!",
                FileName = "AllBe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.6 },
                    { PhraseTypes.SmCb_01B, 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What's got your wand in a twist?",
                FileName = "ThisAintCowPoke",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.3 },
                    { PhraseTypes.RequestActivity, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }


            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "",
                FileName = "WhatDidYouThink",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Calm down there big feller.",
                FileName = "CalmDownThereBig",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.4 },
                    { PhraseTypes.Retreat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Well sir you might want to take that up with the local chaplain",
                FileName = "WellSirYouMight",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 },
                    { PhraseTypes.GiveDisbelief, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.03 },
                    { PhraseTypes.Retreat, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What in tarnation?",
                FileName = "WhatInTarnation",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.GiveDisbelief, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Galloping Goblins!",
                FileName = "WrapMeInCalf",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.GiveDisbelief, 0.4 },
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Could you help me figure out who stole my witch's hat?",
                FileName = "IsThatYouGeorge",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }


            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "That's just witchful thinking",
                FileName = "KeepYourPowderDry",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.Retreat, 0.1 },
                    { PhraseTypes.GiveAdvice, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Those two gonna get hitched?",
                FileName = "ThoseTwoGonnaGet",
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
                DialogStr = "The full moon tonight will cause some very...interesting...things to happen.",
                FileName = "ItsQuietOutHere",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.RequestAffirmation, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Yessss",
                FileName = "Yessss",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 1.0 },
                    { PhraseTypes.GiveAffirmation, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Cackling cats!",
                FileName = "DontGetYourKnickers",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.4 },
                    { PhraseTypes.No, 0.3 },
                    { PhraseTypes.GiveSurprisingStatement, 0.3 },
                    { PhraseTypes.GiveAdvice, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }


            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Would you like to try my magic potion? I made it with boiled frogs and the tears of naughty children",
                FileName = "IThinkMyNose",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.4 },
                    { PhraseTypes.GiveRecentHistory, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Nothing left to do, nothing left to say, come on broomstick, let's fly away!",
                FileName = "LetsVamoos",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.6 },
                    { PhraseTypes.YesNoQuestion, 0.4 },
                    { PhraseTypes.RequestCatchup, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Boiled frog warts never look as good as they taste",
                FileName = "ThatMakesAsMuch",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.3 },
                    { PhraseTypes.GiveDisbelief, 0.3 },
                    { PhraseTypes.No, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "How should I know? I'm a witch, not a psychic",
                FileName = "CantRightlySay",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.01 },
                    { PhraseTypes.Yes, 0.01 },
                    { PhraseTypes.No, 0.1 },
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
                DialogStr = "Yesterday, I had to fix my jack-o-lantern with a pumpkin patch",
                FileName = "AintThatPurdyAs",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.1 },
                    { PhraseTypes.Retreat, 0.1 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.GiveJoke, 0.05 },
                    { PhraseTypes.GiveDisbelief, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Curiosity killed the cat!",
                FileName = "ThatIsSoHot",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.3 },
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Why don't I fly my broom when I'm angry? I'm afrain of flying off the handle!",
                FileName = "TheTsaWouldntLet",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.5 },
                    { PhraseTypes.GiveRecentHistory, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Don't count your dragons before the eggs have hatched",
                FileName = "ThatsAboutAsSignificant",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm so hungry I could eat a small dragon",
                FileName = "TheDaysMovinSlower",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.GiveRecentHistory, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lord, I promise I'll be better.",
                FileName = "LordIpromiseIll",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.RequestAffirmation, 0.2 },
                    { PhraseTypes.GiveDisbelief, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I really don't get along with those wizards. Always using some new wand technology to cast some unnatural, unoriginal spell. Give me some birch bark, birds claws, and an old fashioned cauldron and I can whip up any old spell for you.",
                FileName = "RemindsMeOfTheTimeThemInjuns",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Halloween just isn't the same anymore. Used to be all the witches swooping around, scaring the daylights out of every living creature, vampires running around and werewolves howling. Now it's just boring candy and rotting jack-o-lanters on every porch. ",
                FileName = "SomeDaysWhenThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Is your broomstick ready to fly?",
                FileName = "ReadyToSaddleUp",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I wouldn't touch you with a ten foot broomstick",
                FileName = "YouLilyLiveredCoward",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 },
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Someone as stupid as you should be burned at the stake!",
                FileName = "YouGotShitFor",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Could you help me cast this spell?",
                FileName = "GuessWhatThemDoggies",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Have you seen my black cat Matilda?",
                FileName = "YouKnowWhatMY",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What are you going to be for Halloween? ",
                FileName = "YouKnowWhatIve",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Could you help me skin these snakes?",
                FileName = "YouWillNeverGuess",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Making a love potion",
                FileName = "BirthinCattle",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Shaving some cats.Their fur is very good in levetation potions",
                FileName = "Fornicating",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Reading your palm",
                FileName = "BrandinEmWithA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I crashed my broomstick and accidentally turned my cat into a hedgehog. So not really having the best day.",
                FileName = "ICouldUseSome",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.8 },
                    { PhraseTypes.RequestAffirmation, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Would you like me to brew a potion for you?",
                FileName = "MaybeANightUnder",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Want to take a ride on my broomstick?",
                FileName = "WhenIFeelBlue",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Today I accidentally gave someone a potion that turns your teeth green, instead of a love potion. O well, they probably won't want to date them now anyways. ",
                FileName = "ILeanedBackOn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.6 },
                    { PhraseTypes.GiveJoke, 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "",
                FileName = "RemindsMeOfTheDayIMet",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 },
                    { PhraseTypes.GiveJoke, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Can I borrow some of your clothes for Halloween? This year, I'm dressing up as an idiot.",
                FileName = "YouCouldntFindYour",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Greetings, muggle",
                FileName = "MorningSunshine",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Don't make me use my paralysis potion on you",
                FileName = "YouMightNotOught",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm off. It's time to go terrify some small children.",
                FileName = "ThinkItsAboutTime",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you like the smell of toasted crow's feet?",
                FileName = "DoYouEnjoyTheFragrant",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.4 },
                    { PhraseTypes.YesNoQuestion, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Just learn a few new spells and things will be good as new!",
                FileName = "ThemCloudsAreGonna",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.4 },
                    { PhraseTypes.Exclamation, 0.1 },
                    { PhraseTypes.Retreat, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "From your chin to your toes, complete silence grows! Hush!",
                FileName = "HushNowYou",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.4 },
                    { PhraseTypes.Threat, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't make me use my silencing spell!",
                FileName = "IThinkIHave",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.4 },
                    { PhraseTypes.Threat, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Have you heard any new potion chants?",
                FileName = "HeardAnyNewHumdingers",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.4 },
                    { PhraseTypes.YesNoQuestion, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Whistling wizards!",
                FileName = "ItsHotterThanA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Exclamation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Goodday, fellow wiccin",
                FileName = "ShalomAmigo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Greeting, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'd be careful. My new wand could fry your eyelashes to a crisp.",
                FileName = "AnotherInchAndIll",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Threat, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Goodbye goodbye, broomstick fly",
                FileName = "GetUmUpHead",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Retreat, 1.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You look a little green. Have you been drinking from the wrong caulron? ",
                FileName = "HaveYouBeenDrinking",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.YesNoQuestion, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Is my cat black?",
                FileName = "SureIsTheQuickest",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Yes, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do vampires drink Coke-cola?",
                FileName = "WhenTroubleComesTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Do you dig for water under the outhouse?",
                FileName = "DoYouDigFor",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.No, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sit down for a spell and spill!",
                FileName = "TakeALoadOff",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestCatchup, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Keep your lights on at night..you never know what creatures are lurking around in the dark",
                FileName = "sometimesYouGetThrown",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAffirmation, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.GiveAdvice, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Most cats are smarter than humans. And cleaner. ",
                FileName = "SometimesIBelieveI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAffirmation, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Curses!",
                FileName = "IFeelLikeI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.1 },
                    { PhraseTypes.Exclamation, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Just skinned a few snakes and boiled some lizard eyes.  ",
                FileName = "ISpentTheAfternoon",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveRecentHistory, 0.2 },
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "",
                FileName = "HemroidsAndSaddlesDont",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveSurprisingStatement, 0.2 },
                    { PhraseTypes.GiveRecentHistory, 0.2 },
                    { PhraseTypes.RequestAffirmation, 0.2 },
                    { PhraseTypes.GiveJoke, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Can't wait for Halloween, when the sky gets dark and the moon glows bright, scary creatures will appear in the night. Goblins will growl and werewolves will howl, and Halloween will send shivers up your spine!",
                FileName = "DespairDoesntEndYour",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Ramble, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hush!",
                FileName = "HoldYourHorses",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.ShutUp, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Trick or treat? How about a joke instead?",
                FileName = "IllBetYouA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestJoke, 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What did the witch do when her broomstick broke? She witch-hiked, of course.",
                FileName = "MollyToldMeI",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveJoke, 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Stop being such a scaredy cat!",
                FileName = "OnionsCanMakePeople",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I've met goblins more attractive than you!",
                FileName = "GivenYourPhysiqueThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.Insult, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why'd you miss the full moon ceremonial sacrifice last night?",
                FileName = "WhatCouldTheyHave",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestActivity, 0.2 },
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Getting my wand ready",
                FileName = "PuttingTheirSpursOn",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveActivity, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Halloween is just getting so mainstream, half the time costumes aren't even scary anymore. Maybe I should skip out this year.",
                FileName = "GotALetterFrom",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 },
                    { PhraseTypes.RequestAffirmation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What do you usually do when your eyebrows get burnt off from a malfunctioning spell?",
                FileName = "ImDueToDeliver",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Just act a little more like my cat, and hang in there",
                FileName = "IfYoureRidingThe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Anyone can speak cat, all you have to do is say meow while acting completely bored.",
                FileName = "WellDontSquatWith",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Nature gave us all something to fall back on, and sooner or later we all land flat on it.",
                FileName = "NatureGaveUsAll",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.1 },
                    { PhraseTypes.RequestAdvice, 0.1 },
                    { PhraseTypes.GiveMotivation, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Don't put all your eggs in one basket ",
                FileName = "IfYouFindYourself",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveAdvice, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Did you let the cat out of the bag?",
                FileName = "DoYouSupposeA",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Are you trying to end up burnt at the stake?",
                FileName = "WhatCouldPossiblyBe",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "The crystal ball told me to",
                FileName = "IReckonItWas",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "What happens in Salem stays in Salem.",
                FileName = "CouldHaveJustBeen",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My first spell didn't work out, what else would you expect from me?",
                FileName = "ISupposeItIs",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveMotivation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Am I going to have to go ask the town oracle?",
                FileName = "SoAreYouGoing",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 },
                    { PhraseTypes.YesNoQuestion, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Last I saw my broomstick, some stupid human was dragging it all over the floor for some reason, have you seen it?",
                FileName = "ISeemToHave",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.RequestLocation, 0.2 },
                    { PhraseTypes.RequestAffirmation, 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "In yer dreams sweetheart.",
                FileName = "InYerDreamsSweetheart",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 },
                    { PhraseTypes.No, 0.1 },
                    { PhraseTypes.SmCb_01D, 1.0 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "",
                FileName = "YouMightHaveTo",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Somewhere under that smilin umbrella of stars.",
                FileName = "SomewhereUnderThatSmilin",
                PhraseWeights = new Dictionary<PhraseTypes, double>{
                    { PhraseTypes.GiveLocation, 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
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
                    // for a given phrase of a character add all the associated phrase weights for the phrase
                    //to the proper PhraseTotals
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
