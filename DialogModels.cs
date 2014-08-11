using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dialog_Generator
{
    /*  
        Exclamation, Greeting, Threat, Retreat, Proposal, Yes, No, RequestCatchup, GiveAffirmation, RequestAffirmation, 
        GiveDisbelief, GiveRecentHistory, GiveSurprisingStatement, Ramble, ShutUp, RequestJoke, GiveJoke, PhraseTypesSize
    */

    public static class InitModelDialogs 
    {
        public static void SetDefaults(DialogTracker InObj) {
 

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.8f, Name = "Dual Greeting" });
            InObj.ModelDialogTable[0].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            InObj.ModelDialogTable[0].PhraseTypeSequence.Add(PhraseTypes.Greeting);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.4f, Name = "Single Exclamation" });
            InObj.ModelDialogTable[1].PhraseTypeSequence.Add(PhraseTypes.Exclamation);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Greeting Threat Retreat" });
            InObj.ModelDialogTable[2].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            InObj.ModelDialogTable[2].PhraseTypeSequence.Add(PhraseTypes.Threat);
            InObj.ModelDialogTable[2].PhraseTypeSequence.Add(PhraseTypes.Retreat);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Request and Give Affirmation" });
            InObj.ModelDialogTable[3].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            InObj.ModelDialogTable[3].PhraseTypeSequence.Add(PhraseTypes.RequestAffirmation);
            InObj.ModelDialogTable[3].PhraseTypeSequence.Add(PhraseTypes.GiveAffirmation);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.1f, Name = "Request and Give Joke" });
            InObj.ModelDialogTable[4].PhraseTypeSequence.Add(PhraseTypes.RequestJoke);
            InObj.ModelDialogTable[4].PhraseTypeSequence.Add(PhraseTypes.GiveJoke);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.2f, Name = "Disbelieve surprising statement" });
            InObj.ModelDialogTable[5].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            InObj.ModelDialogTable[5].PhraseTypeSequence.Add(PhraseTypes.GiveSurprisingStatement);
            InObj.ModelDialogTable[5].PhraseTypeSequence.Add(PhraseTypes.GiveDisbelief);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Deny Proposal" });
            InObj.ModelDialogTable[6].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            InObj.ModelDialogTable[6].PhraseTypeSequence.Add(PhraseTypes.Proposal);
            InObj.ModelDialogTable[6].PhraseTypeSequence.Add(PhraseTypes.No);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.1f, Name = "Dual Exclamation" });
            InObj.ModelDialogTable[7].PhraseTypeSequence.Add(PhraseTypes.Exclamation);
            InObj.ModelDialogTable[7].PhraseTypeSequence.Add(PhraseTypes.Exclamation);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Dual Exclamation Proposal No" });
            InObj.ModelDialogTable[8].PhraseTypeSequence.Add(PhraseTypes.Exclamation);
            InObj.ModelDialogTable[8].PhraseTypeSequence.Add(PhraseTypes.Exclamation);
            InObj.ModelDialogTable[8].PhraseTypeSequence.Add(PhraseTypes.Proposal);
            InObj.ModelDialogTable[8].PhraseTypeSequence.Add(PhraseTypes.No);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Dual Exclamation Proposal Yes" });
            InObj.ModelDialogTable[9].PhraseTypeSequence.Add(PhraseTypes.Exclamation);
            InObj.ModelDialogTable[9].PhraseTypeSequence.Add(PhraseTypes.Exclamation);
            InObj.ModelDialogTable[9].PhraseTypeSequence.Add(PhraseTypes.Proposal);
            InObj.ModelDialogTable[9].PhraseTypeSequence.Add(PhraseTypes.Yes);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "That wasn't funny" });
            InObj.ModelDialogTable[10].PhraseTypeSequence.Add(PhraseTypes.GiveJoke);
            InObj.ModelDialogTable[10].PhraseTypeSequence.Add(PhraseTypes.GiveDisbelief);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Insult, Insult, Insult" });
            InObj.ModelDialogTable[11].PhraseTypeSequence.Add(PhraseTypes.Insult);
            InObj.ModelDialogTable[11].PhraseTypeSequence.Add(PhraseTypes.Insult);
            InObj.ModelDialogTable[11].PhraseTypeSequence.Add(PhraseTypes.Insult);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "RequestAffirmation, Insult, Insult" });
            InObj.ModelDialogTable[12].PhraseTypeSequence.Add(PhraseTypes.RequestAffirmation);
            InObj.ModelDialogTable[12].PhraseTypeSequence.Add(PhraseTypes.Insult);
            InObj.ModelDialogTable[12].PhraseTypeSequence.Add(PhraseTypes.Insult);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Greeting, Proposal, Insult" });
            InObj.ModelDialogTable[13].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            InObj.ModelDialogTable[13].PhraseTypeSequence.Add(PhraseTypes.Proposal);
            InObj.ModelDialogTable[13].PhraseTypeSequence.Add(PhraseTypes.Insult);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "RequestCatchup, Insult" });
            InObj.ModelDialogTable[14].PhraseTypeSequence.Add(PhraseTypes.RequestCatchup);
            InObj.ModelDialogTable[14].PhraseTypeSequence.Add(PhraseTypes.Insult);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.3f, Name = "Guess what They/I did, Guess Activity, Correct Activity " });
            InObj.ModelDialogTable[15].PhraseTypeSequence.Add(PhraseTypes.RequestActivity);
            InObj.ModelDialogTable[15].PhraseTypeSequence.Add(PhraseTypes.GiveActivity);
            InObj.ModelDialogTable[15].PhraseTypeSequence.Add(PhraseTypes.GiveActivity);

            InObj.ModelDialogTable.Add(new ModelDialog() { Popularity = 0.5f, Name = "Greeting, GiveJoke, Insult" });
            InObj.ModelDialogTable[16].PhraseTypeSequence.Add(PhraseTypes.Greeting);
            InObj.ModelDialogTable[16].PhraseTypeSequence.Add(PhraseTypes.GiveJoke);
            InObj.ModelDialogTable[16].PhraseTypeSequence.Add(PhraseTypes.Insult);
        }        
    }
}
       
