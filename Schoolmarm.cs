using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dialog_Generator
{
    /*  
        Exclamation, Greeting, Threat, Retreat, Proposal, Yes, No, RequestCatchup, GiveAffirmation, RequestAffirmation, 
        GiveDisbelief, GiveRecentHistory, GiveSurprisingStatement, Ramble, ShutUp, RequestJoke, GiveJoke
    */

    public static class InitSchoolmarm 
    {
        public static void SetDefaults(Character InObj) {
             InObj.CharacterName = "Eunice";
             InObj.Gender = GenderType.Female;
             InObj.Age = 38;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Good day" });
             InObj.PhraseTable[0].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
             InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.Greeting] = 1.0f;
             InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.4f;
             InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Your behavior is intolerable!" });
             InObj.PhraseTable[1].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.1f;
             InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;
             InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.No] = 0.2f;
             InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Threat] = 0.2f;
             InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Retreat] = 0.2f;
             InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.3f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Have you completed your assignments?" });
             InObj.PhraseTable[2].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[2].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "And what, pray tell, have you been up to?" });
             InObj.PhraseTable[3].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[3].PhraseProperties[(int)PhraseTypes.Greeting] = 0.2f;
             InObj.PhraseTable[3].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.5f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I fear my Cassenova will never arrive." });
             InObj.PhraseTable[4].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[4].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.6f;
             InObj.PhraseTable[4].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.1f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You are in need of some discipline young lady." });
             InObj.PhraseTable[5].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[5].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
             InObj.PhraseTable[5].PhraseProperties[(int)PhraseTypes.Threat] = 0.5f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You are in need of some discipline young man." });
             InObj.PhraseTable[6].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[6].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
             InObj.PhraseTable[6].PhraseProperties[(int)PhraseTypes.Threat] = 0.5f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Well sir, we shall be on our way." });
             InObj.PhraseTable[7].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
             InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.Threat] = 0.5f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I don't know what to say." });
             InObj.PhraseTable[8].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Greeting] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Threat] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Proposal] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Yes] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.No] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Ramble] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.1f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.1f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Today I read from the unabridged works of deSade." });
             InObj.PhraseTable[9].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.2f;
             InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.4f;
             InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.2f;
             InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.2f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You are looking handsome today." });
             InObj.PhraseTable[10].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[10].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.2f;
             InObj.PhraseTable[10].PhraseProperties[(int)PhraseTypes.Greeting] = 0.5f;
             InObj.PhraseTable[10].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You get an A+ today." });
             InObj.PhraseTable[11].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[11].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.2f;
             InObj.PhraseTable[11].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;
        
        
        }        
    }
}
       
