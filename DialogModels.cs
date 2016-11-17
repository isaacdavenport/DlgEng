using System;

namespace DialogEngine
{
    public static class InitModelDialogs  
    {  //TODO this could be moved to a collection as well like the Character Objects and inheret popularity for statistical selection of elements
        // TODO add a date that a dialog model or phrase is added so we can probabalistically select more recent phrases / dialogs
        public static void SetDefaults(DialogTracker inObj) {

            DateTime firstRound = new DateTime(2016, 5, 18);

            int i = 0;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Greeting, Greeting",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Greeting);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Single Greeting",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Greeting);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Exclamation, Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Exclamation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Exclamation);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Single Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Exclamation);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Threat Retreat",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Threat);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Retreat);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "RequestAffirmation, Give Affirmation",
                AddedOnDateTime = firstRound, Popularity = 0.4 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestAffirmation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveAffirmation);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Request Affirmation Give Joke",
                AddedOnDateTime = firstRound, Popularity = 0.3 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestAffirmation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveJoke);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Request Joke, Give Joke",
                AddedOnDateTime = firstRound, Popularity = 0.9 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestJoke);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveJoke);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "GiveSurprisingSt, GiveDisbelief",
                AddedOnDateTime = firstRound, Popularity = 0.3 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveSurprisingStatement);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveDisbelief);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "YesNoQuestion, No",
                AddedOnDateTime = firstRound, Popularity = 0.7 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.YesNoQuestion);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.No);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "YesNoQuestion, Yes",
                AddedOnDateTime = firstRound, Popularity = 0.5 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.YesNoQuestion);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Yes);
            
            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "GiveSurprisingStatement Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.4 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveSurprisingStatement);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Exclamation);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "GiveJoke, Disbelief -That wasn't funny",
                AddedOnDateTime = firstRound, Popularity = 0.3 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveJoke);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveDisbelief);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Insult, Insult, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "RequestAffirmation, Insult, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestAffirmation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "YesNoQuestion, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.YesNoQuestion);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "RequestCatchup, Insult, Retreat", AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestCatchup);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Retreat);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = 
                "Guess what They/I did, Guess Activity, Correct Activity ",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestActivity);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveActivity);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveActivity);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "GiveJoke, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveJoke);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "RequestAff GiveAff ReqCat Ramble retreat",
                AddedOnDateTime = firstRound, Popularity = 0.3 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestAffirmation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveAffirmation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestCatchup);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Ramble);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Retreat);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "RequestCatchup GiveRecentHistory",
                AddedOnDateTime = firstRound, Popularity = 0.6 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestCatchup);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveRecentHistory);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "RequestCatchup, Ramble, Shutup",
                AddedOnDateTime = firstRound, Popularity = 0.4 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestCatchup);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Ramble);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.ShutUp);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Insult, Shutup",
                AddedOnDateTime = firstRound, Popularity = 0.15 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.ShutUp);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Insult, Insult, Insult, Exc",
                AddedOnDateTime = firstRound, Popularity = 0.1 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Exclamation);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Insult, Threat, Retreat",
                AddedOnDateTime = firstRound, Popularity = 0.1 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Insult);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Threat);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.Retreat);
            
            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "ReqAct, GuessAct, CorrectAct ",
                AddedOnDateTime = firstRound, Popularity = 0.2 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestActivity);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveActivity);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveActivity);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "ReqMotiv, Givemotiv",
                AddedOnDateTime = firstRound, Popularity = 0.6 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestMotivation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveMotivation);
 
            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "ReqAdv,  GiveAdv",
                AddedOnDateTime = firstRound, Popularity = 1.3 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestAdvice);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveAdvice);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "Request location Give location",
                AddedOnDateTime = firstRound, Popularity = 2.1 });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.RequestLocation);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.GiveLocation);

            i++;
            inObj.ModelDialogs.Add(new ModelDialog() { Name = "CB SM Script 1 inuendo",
                AddedOnDateTime = new DateTime(2016, 6, 18), Popularity = 5.1
            });
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.SmCb_01A);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.SmCb_01B);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.SmCb_01C);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.SmCb_01D);
            inObj.ModelDialogs[i].PhraseTypeSequence.Add(PhraseTypes.SmCb_01E);

            inObj.popularitySum = 0;
            foreach (var currentDialog in inObj.ModelDialogs) {
                inObj.popularitySum += currentDialog.Popularity;
            }

            /*
             * bodily noises
             * char1 burp,fart,sneeze,etc
             * char1 excuse,pardon,etc
             * chr2 excuse,pardon,insult,etc
             * let me introduce myself 
             * add a situational (all too close or too close to house) "Its getting crowded over here" line set tag
             * add dialogs with voice of the narrator
             */ 
        }        
    }
}
       
