using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DialogEngine
{
    public class Cartman : Character  
    {
        public Cartman() {


            CharacterName = "Cartman";
            CharacterPrefix = "CM"; 
            Phrases.Add(new PhraseEntry {
                PhraseRating = ParentalRating.G,
                DialogStr = "Cartman has not been initialized.",
                FileName = "CartmanHasNotBeen",/*
                testtestPhraseWeights = new Dictionary<string, double>
                {
                    { "Exclamation",0.01 }
                },*/
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.01 }
                },
            });
            
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm Eric but everyone calls me Cartman.",
                FileName = "ImEricButEveryone",/*
                testtestPhraseWeights = new Dictionary<string, double>
                {
                    { "Greeting", 0.7 }
                },*/
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.7 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You will respect my authoritah!",
                FileName = "YouWillRespectMy",/*
                testtestPhraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 1.2 },
                    { "ShutUp", 0.3 },
                    { "Threat", 1 },
                    { "GiveAdvice", 0.05 }
                },*/
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 1.2 },
                    { "ShutUp", 0.3 },
                    { "Threat", 1 },
                    { "GiveAdvice", 0.05 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "You killed Kenny!",
                FileName = "YouKilledKenny",/*
                testtestPhraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "ShutUp", 0.1 },
                    { "GiveSurprisingStatement", 0.3 }
                },*/
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "ShutUp", 0.1 },
                    { "GiveSurprisingStatement", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Eating cheesy poofs.",
                FileName = "EatingCheesyPoofs",
                /*
                testtestPhraseWeights = new Dictionary<string, double>
                {
                    { "GiveActivity", 0.7 }
                },*/
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.7 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "No!  This is my pie.",
                FileName = "NoThisIsMy",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.5 },
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Eating pie.",
                FileName = "EatingPie",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 1.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What do you want to do after school?",
                FileName = "WhatDoYouWant",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I have dry balls.",
                FileName = "IHaveDryBalls",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.5 },
                    { "RequestAffirmation", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "I have an itch in a bad place!",
                FileName = "IHaveAnItch",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.5 },
                    { "RequestAffirmation", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "I hope you get amoebic dysentery. I'm going home.",
                FileName = "IHopeYouGet",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "Retreat", 0.5 },
                    { "GiveDisbelief", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Screw you. I'm going home.",
                FileName = "ScrewYouImGoing",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "Retreat", 0.5 },
                    { "GiveDisbelief", 0.5 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Whatever.  I'll do what I want.",
                FileName = "WhateverIllDoWhat",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.3 },
                    { "Retreat", 0.1 },
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Dang",
                FileName = "Dang",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Goddammit",
                FileName = "Goddammit",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Hippies. They're everywhere. They wanna save the earth, but all they do is smoke pot and smell bad..",
                FileName = "HippiesTheyreEverywhereThey",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "I've been turning this steering wheel for three whole hours but I still don't feel like a " +
                            "race car driver.",
                FileName = "IveBeenTurningThis",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I've been licking this carpet for three whole hours and I still don't feel like a lesbian",
                FileName = "IveBeenLickingThis",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 },
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "You so much as TOUCH my ass, and I'll put a firecracker in your nutsack and blow your balls all over your pants",
                FileName = "YouSoMuchAs",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.6 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Why is it that everything today has to do with things going into our or coming out of my ass?",
                FileName = "WhyIsItThat",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.4 },
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Dolphins, Eskimos, Who cares?  Its all a bunch of tree hugging hippie crap.",
                FileName = "DolphinsEskimosWhoCares",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 },
                    { "GiveSurprisingStatement", 0.4 },
                    { "Ramble", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "You so much as TOUCH me and I will blow you up!",
                FileName = "YouSoMuchAsUp",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.6 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why is it that everything today has to do with things going into or out of my body?",
                FileName = "WhyIsItThatBody",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.4 },
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Dolphins, Eskimos, Who cares?  Its all about tree hugging hippies.",
                FileName = "DolphinsEskimosWhoCaresHippies",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.4 },
                    { "GiveSurprisingStatement", 0.4 },
                    { "Ramble", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I'm not fat.  I'm festively plump.",
                FileName = "ImNotFatIm",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "GiveSurprisingStatement", 0.4 },
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm outa here.",
                FileName = "ImOutaHere",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.8 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't you know the first law of physics? Anything that's fun costs at least eight dollars",
                FileName = "DontYouKnowThe",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hippies can't stand death metal",
                FileName = "HippiesCantStandDeath",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I'd bet a hundred dollars on it.",
                FileName = "IdBetAHundred",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "AHHHHH!!",
                FileName = "Ahhhhh",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "It hurts, goddamnit!",
                FileName = "ItHurtsGoddamnit",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "It hurts, Ow.  It hurts!",
                FileName = "ItHurtsOwIt",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Would you grind someone's parents into chili meat " +
                            "if you really hated him and wanted to make him suffer?",
                FileName = "WouldYouGrindSomeones",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "GiveSurprisingStatement", 0.3 },
                    { "RequestAdvice", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "What do you do if you really hate someone and want to make him suffer?",
                FileName = "WhatDoYouDo",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "GiveSurprisingStatement", 0.3 },
                    { "RequestAdvice", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Trying to train a pony to bite someone's wiener off.",
                FileName = "TryingToTrainA",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Trying to train a pony to bite someone.",
                FileName = "TryingToTrainASomeone",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Why the fuck not?",
                FileName = "WhyTheFuckNot",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Why not?",
                FileName = "WhyNot",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.R,
                DialogStr = "Sucking balls.",
                FileName = "SuckingBalls",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sucking on rotten eggs.",
                FileName = "SuckingOnRottenEggs",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Taking people's jobs.",
                FileName = "TakingPeoplesJobs",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sometimes they are just out to take our jobs.",
                FileName = "SometimesTheyAreJust",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.3 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.R,
                DialogStr = "You're a fucking faggot, dude.",
                FileName = "YoureAFuckingFaggot",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You're a booger head.",
                FileName = "YoureABoogerHead",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "You don't even have two brain cells to rub together.",
                FileName = "YouDontEvenHave",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I don't like you.",
                FileName = "IDontLikeYou",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 },
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "You bet your last dime",
                FileName = "YouBetYourLast",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Back off you big ape!",
                FileName = "BackOffYouBig",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 },
                    { "No", 0.4 },
                    { "Threat", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "You bet your fuckin' ass!",
                FileName = "YouBetYourFuckin",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Fuck off, you donkey-raping shit eater.",
                FileName = "FuckOffYouDonkey",
                phraseWeights = new Dictionary<string, double>{
                    { "Insult", 0.4 },
                    { "No", 0.4 },
                    { "Threat", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Would you like to have some tea poured by my friend Polly Prissy Pants?",
                FileName = "WouldYouLikeTo",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "Ramble", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "This is  weak... ",
                FileName = "ThisIsWeak",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.R,
                DialogStr = "Pretty sweet huh?",
                FileName = "PrettySweetHuh",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "Yes", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Granola makes me mad.",
                FileName = "GranolaMakesMeMad",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "This is fucking weak... ",
                FileName = "ThisIsFuckingWeak",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Pretty fucking sweet huh?",
                FileName = "PrettyFuckingSweetHuh",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.4 },
                    { "Yes", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Granola pisses me off.",
                FileName = "GranolaPissesMeOff",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.2 },
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Of course.",
                FileName = "OfCourse",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
/* ended here for G makeng */            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I dreamed I was standing out in a field, and there was this huge satellite " +
                            "dish stickin' out of my butt. And there were hundreds of cows and aliens, and then I went " +
                            "up on the ship, and Scott Baio gave me pinkeye.",
                FileName = "IDreamedIWas",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Would I let the kitty eat my pot pie?",
                FileName = "WouldILetThe",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't be such a negative Nancy.  Tell me whats going on.",
                FileName = "DontBeSuchA",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.4 },
                    { "RequestJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "They took our jobs.",
                FileName = "TheyTookOurJobs",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.4 },
                    { "RequestAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Suck my balls.",
                FileName = "SuckMyBalls",
                phraseWeights = new Dictionary<string, double>{
                    { "No", 0.4 },
                    { "GiveSurprisingStatement", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Sweet.",
                FileName = "Sweet",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.4 },
                    { "Exclamation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My mom is so poor she can't even pay attention.",
                FileName = "MyMomIsSoPoorShe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My mom is so poor that when she goes to KFC she has to lick other people's fingers.",
                FileName = "MyMomIsSoPoorThat",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My mom is so poor the ducks throw bread at her.",
                FileName = "MyMomIsSoPoorThe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "My mom is so poor she opened a gmail account just so she can eat the spam.",
                FileName = "MyMomIsSoPoorSheOpened",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveJoke", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Want some cheesy poofs?",
                FileName = "WantSomeCheesyPoofs",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 },
                    { "Greeting", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Playing death metal to hippies.",
                FileName = "PlayingDeathMetalTo",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Whats up hippie.",
                FileName = "WhatsUpHippie",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hey.",
                FileName = "Hey",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "So why are you so cool?  What have you done.",
                FileName = "SoWhyAreYou",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It's OK.  Have some pie.",
                FileName = "ItsOkHaveSome",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Don't worry.  At least you aren't as poor as my mom.",
                FileName = "DontWorryAtLeast",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Everything's gonna be okay! Life isn't so crappy after all!",
                FileName = "EverythingsGonnaBeOkay",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Everything's gonna be okay! Life isn't so bad after all!",
                FileName = "EverythingsGonnaBeSoBad",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Well, you won't believe this. But Stan's parents sent me on a quest to get a magical video back.  It was obviously evil.  So we had to return it all the way to the video rental store.  Then these sixth graders started chasing us on bicycles.  But we finally got it into the return slot, but, Butters went down with it.",
                FileName = "WellYouWontBelieve",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.4 },
                    { "Ramble", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I was just negotiating for some weapons and an escape route.",
                FileName = "IWasJustNegotiating",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "What have you been up to?",
                FileName = "WhatHaveYouBeen",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.3 },
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You will make me laugh, right now.  Or I will ensure there is a laxative in your next meal.",
                FileName = "YouWillMakeMe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.4 },
                    { "Threat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "You will make me laugh, ... right now.",
                FileName = "YouWillMakeMeLaugh",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.4 },
                    { "Threat", 0.4 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Hi.",
                FileName = "Hi",
                phraseWeights = new Dictionary<string, double>{
                    { "Greeting", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I will explode your head with my telekinetic powers!",
                FileName = "IWillExplodeYour",
                phraseWeights = new Dictionary<string, double>{
                    { "Threat", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Lets go.",
                FileName = "LetsGo",
                phraseWeights = new Dictionary<string, double>{
                    { "Retreat", 0.6 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Is it just me, or does a lot of crazy shit go on in this town?",
                FileName = "IsItJustMe",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Is it just me, or does a lot of crazy stuff go on in this town?",
                FileName = "IsItJustMeStuff",
                phraseWeights = new Dictionary<string, double>{
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Absolutely.",
                FileName = "Absolutely",
                phraseWeights = new Dictionary<string, double>{
                    { "Yes", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "No.",
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
                DialogStr = "Did you bring me some cheesy poofs?",
                FileName = "DidYouBringMe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestCatchup", 0.02 },
                    { "YesNoQuestion", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "It isn't so bad.  Kitty still loves us.",
                FileName = "ItIsntSoBad",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "How come nobody wants to be my friend.",
                FileName = "HowComeNobodyWants",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "You don't know what the hell you are talking about.",
                FileName = "YouDontKnowWhat",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "You don't know what you are talking about.",
                FileName = "YouDontKnowWhatYou",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveDisbelief", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I was playing lambs with Polly Prissypants when Peter Panda suggested we play a new game, but then my mom spoiled it all.",
                FileName = "IWasPlayingLambs",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveRecentHistory", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I know what I am getting for my birthday.  I snuck around in my mom's closet and saw the box for the Ultravibe Pleasure 2000.",
                FileName = "IKnowWhatI",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveSurprisingStatement", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "So we ordered the magical seamen from a magazine and put it in an aquarium.  We thought they would just play basketball and ride around on turtles.  But, the sea pepole started building little cities.",
                FileName = "SoWeOrderedThe",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Shut the fuck up.",
                FileName = "ShutTheFuckUp",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Shut up.",
                FileName = "ShutUp",
                phraseWeights = new Dictionary<string, double>{
                    { "ShutUp", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "You think your so talented.  Why don't you entertain us?",
                FileName = "YouThinkYourSo",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestJoke", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "I was explaining why I don't like rainbows.  Cause, well, you know. You'll just be sitting there" +
                            ", minding your own business, and they'll come marching in, and crawl up your leg, and start biting the " +
                            "inside of your ass, and you'll be all like, \"Hey. Get out of my ass you stupid rainbows.\" Then they " +
                            "explained that rainbows are those little arches of color that show up during a rainstorm.",
                FileName = "IWasExplainingWhy",
                phraseWeights = new Dictionary<string, double>{
                    { "Ramble", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Follow your dreams. You can reach your goals. I'm living proof. I'm Beefcake.",
                FileName = "FollowYourDreamsYou",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAffirmation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "I hate you guys.",
                FileName = "IHateYouGuys",
                phraseWeights = new Dictionary<string, double>{
                    { "Exclamation", 0.1 },
                    { "Retreat", 0.2 },
                    { "No", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Well, what would you rather be doing?",
                FileName = "WellWhatWouldYou",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Bulking up with some \"Weight Gain 4000\"",
                FileName = "BulkingUpWithSome",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveActivity", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "They are coming for me.  What should I do?",
                FileName = "TheyAreComingFor",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "I don't think Peter Panda and Polly Prissypants are getting along.",
                FileName = "IDontThinkPeter",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes all you have to do is stop and think \"What would Brian Boytano do?\"",
                FileName = "SometimesAllYouHave",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Sometimes swearing loudly doesn't help a situation, but usually it does.",
                FileName = "SometimesSwearingLoudlyDoesnt",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveAdvice", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Why would they say I was fat?",
                FileName = "WhyWouldTheySay",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Why would aliens be so curious about the inside of my butt?",
                FileName = "WhyWouldAliensBe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Why would aliens be so curious about my insides?",
                FileName = "WhyWouldAliensBeInsides",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Well of course, some people just want to be king of the world.",
                FileName = "WellOfCourseSome",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG,
                DialogStr = "Perhaps it is the same reason we must sometimes put on our crown of invisibility and our boots of levitation and accept our quest.",
                FileName = "PerhapsItIsThe",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveMotivation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "In your ass.",
                FileName = "InYourAss",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.G,
                DialogStr = "Have you considered somewhere very far away?",
                FileName = "HaveYouConsideredSomewhere",
                phraseWeights = new Dictionary<string, double>{
                    { "GiveLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Where is my hemoriod cream?",
                FileName = "WhereIsMyHemoroid",
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
                DialogStr = "Where is my cream?",
                FileName = "WhereIsMyCream",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.PG13,
                DialogStr = "Damnit, where did she come from?  She could spoil my plan.",
                FileName = "DamnitWhereDidShe",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.3 },
                    { "GiveDisbelief", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.PG,
                DialogStr = "Where did she come from?  She could spoil my plan.",
                FileName = "WhereDidSheCome",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.3 },
                    { "GiveDisbelief", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry{
                PhraseRating = ParentalRating.R,
                DialogStr = "Is there a whorehouse around here?  My nuts are going to explode.",
                FileName = "IsThereAWhorehouse",
                phraseWeights = new Dictionary<string, double>{
                    { "RequestLocation", 0.2 },
                    { "GiveSurprisingStatement", 0.1 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating) {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Give me my two dollars!",
                FileName = "GiveMeMyTwo",
                phraseWeights = new Dictionary<string, double>{
                    { "LM01A", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "You do too have two dollars, I see you buy lunch at the cafeteria every day!",
                FileName = "YouDoTooHave",
                phraseWeights = new Dictionary<string, double>{
                    { "LM01C", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm going to get Billy's dad, he will make you give me my two dollars.",
                FileName = "ImGoingToGet",
                phraseWeights = new Dictionary<string, double>{
                    { "LM01E", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Excuse me mister Lumbergh.  Johnny won't give me my two dollars.",
                FileName = "ExcuseMeMrLumberg",
                phraseWeights = new Dictionary<string, double>{
                    { "LM03A", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Well, he owes me two dollars, and I know he has it, but he won't give it to me.",
                FileName = "WellHeOwesMe",
                phraseWeights = new Dictionary<string, double>{
                    { "LM03C", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "He knows that a bet is a bet.  He said I couldn't prove" + 
                            " that the government lies to us, but I proved it.  Three times!",
                FileName = "HeKnowsThatABet",
                phraseWeights = new Dictionary<string, double>{
                    { "LM03E", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Nope.  Haven't seen him.  I can't find kitty either.",
                FileName = "NopeIhaventSeenHim",
                phraseWeights = new Dictionary<string, double>{
                    { "LM03G", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "It is easy to work out.  Give me my two dollars!",
                FileName = "ItIsEasyTo",
                phraseWeights = new Dictionary<string, double>{
                    { "LM04B", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "What can you do?  You can't pass my history test for me, or get Gretchen " + 
                "to sit by me at lunch.  Maybe you should just dig in the sand around the playground till " + 
                "you collect enough loose change... To give me my two dollars!",
                FileName = "WhatCanYouDo",
                phraseWeights = new Dictionary<string, double>{
                    { "LM04D", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Skylar, Johny won't pay me the two dollars he owes me, and my kitty is lost!",
                FileName = "SkylarJohnnyWontPay",
                phraseWeights = new Dictionary<string, double>{
                    { "LM06A", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I'm not sure that works for me.",
                FileName = "CM_ImNotSureThat",
                phraseWeights = new Dictionary<string, double>{
                    { "LM06C", 0.2 },
                    { "Retreat", 0.1 },
                    { "GiveDisbelief", 0.2 },
                    { "No", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Maybe Kitty is around here.  There you are Kitty.  What were " + 
                            "you doing with Billy?  Was he trying to get you to play Pokemon go?",
                FileName = "MaybeKittyIsAround",
                phraseWeights = new Dictionary<string, double>{
                    { "LM07A", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "It seems like someone convinced her I was pretty cool.",
                FileName = "ItSeemsLikeSomeone",
                phraseWeights = new Dictionary<string, double>{
                    { "LM09B", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Fortunately I have an endless supply of stories about kitty.  " +
                            "She thought that was pretty cool.",
                FileName = "CM_FortuneatelyIHaveAn",
                phraseWeights = new Dictionary<string, double>{
                    { "LM09D", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I am",
                FileName = "IAm",
                phraseWeights = new Dictionary<string, double>{
                    { "LM09F", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

           
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Don't worry.  You don't have to give me two dollars.  If you will be my friend.",
                FileName = "DontWorryYouDont",
                phraseWeights = new Dictionary<string, double>{
                    { "LM10B", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Yeah.",
                FileName = "Yeah",
                phraseWeights = new Dictionary<string, double>{
                    { "LM10D", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "No Way!  How did Kitty get there?  That is awesome news.  Thanks Johnny!",
                FileName = "NoWayHowDid",
                phraseWeights = new Dictionary<string, double>{
                    { "LM10F", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "What is that supposed to mean?",
                FileName = "WhatIsThatSupposed",
                phraseWeights = new Dictionary<string, double>{
                    { "LM11B", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }

            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Hey.  How did you get Kitty?",
                FileName = "CM_HeyHowDidYou",
                phraseWeights = new Dictionary<string, double>{
                    { "LM15A", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Kitty's not a furry little beast, she's my kitty.",
                FileName = "CM_KittysNotAFurry",
                phraseWeights = new Dictionary<string, double>{
                    { "LM15C", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "Come on kitty, were going home.  We can have some tea with polly prissy "+
                            "pants and peter panda.",
                FileName = "ComeOnKittyWere",
                phraseWeights = new Dictionary<string, double>{
                    { "LM15E", 0.2 }
                }
            });
            if (Phrases[Phrases.Count - 1].PhraseRating > SessionVars.CurrentParentalRating)
            {
                Phrases.RemoveAt(Phrases.Count - 1);
            }
            
            Phrases.Add(new PhraseEntry
            {
                PhraseRating = ParentalRating.G,
                DialogStr = "I see you aren't out telling people how cool I am, well two dollars is better than that anyway.",
                FileName = "CM_ISeeYouArent",
                phraseWeights = new Dictionary<string, double>{
                    { "LM16B", 0.2 }
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
            foreach (PhraseEntry currentPhrase in Phrases) {
                foreach (var currentPhraseType in currentPhrase.phraseWeights) {
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
