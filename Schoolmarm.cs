using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dialog_Generator
{
    /*  
        Exclamation, Greeting, Threat, Retreat, Proposal, Yes, No, RequestCatchup, GiveAffirmation, RequestAffirmation, 
        GiveDisbelief, GiveRecentHistory, GiveSurprisingStatement,  Ramble, ShutUp, RequestJoke, GiveJoke, Insult, RequestActivity, GiveActivity, PhraseTypesSize
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

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Literacy has taken a real plunge in this country" });
             InObj.PhraseTable[12].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[12].PhraseProperties[(int)PhraseTypes.Insult] = 0.6f;
             InObj.PhraseTable[12].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;
             InObj.PhraseTable[12].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.1f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Your participle is danlging" });
             InObj.PhraseTable[13].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[13].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.4f;
             InObj.PhraseTable[13].PhraseProperties[(int)PhraseTypes.No] = 0.2f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "In a battle of wits you would be unarmed" });
             InObj.PhraseTable[14].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[14].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Eat a dick good sir" });
             InObj.PhraseTable[15].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[15].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You are a bigger whore than lady Bronte" });
             InObj.PhraseTable[16].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[16].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Even lady Chatterly wouldn't take you as a lover" });
             InObj.PhraseTable[17].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[17].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Guess what I caught Lisa and Jimmy doing in the bathroom." });
             InObj.PhraseTable[18].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[18].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Do you know what I spent the summer doing?" });
             InObj.PhraseTable[19].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "What was Dicken's favorite activity?  It wasn't writing." });
             InObj.PhraseTable[20].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "What should I spend my evening on?" });
             InObj.PhraseTable[21].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[21].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;
             InObj.PhraseTable[21].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.3f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Homework" });
             InObj.PhraseTable[22].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.GiveActivity] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Playing tiddly winks with foreign coins" });
             InObj.PhraseTable[23].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[23].PhraseProperties[(int)PhraseTypes.GiveActivity] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Hoping a prince will come" });
             InObj.PhraseTable[24].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[24].PhraseProperties[(int)PhraseTypes.GiveActivity] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I could use some comic relief" });
             InObj.PhraseTable[25].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[25].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.8f;
             InObj.PhraseTable[25].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.3f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "The reason the bicycle fell over was it was two tired" });
             InObj.PhraseTable[26].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.4f;

             InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "It wasn't the panda that eats shoots and leaves, it was the bank robber" });
             InObj.PhraseTable[27].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
             InObj.PhraseTable[27].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.4f;
        }        
    }
}
       
