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

    public static class InitSchoolboy 
    {
        public static void SetDefaults(Character InObj) {
            InObj.CharacterName = "Little Johnny";
            InObj.Gender = GenderType.Male;
            InObj.Age = 6;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Hi, I'm Johnny" });
            InObj.PhraseTable[0].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.Greeting] = 1.0f;
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.4f;
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Do you know where babies come from?" });
            InObj.PhraseTable[1].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Greeting] = 0.2f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.No] = 0.2f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.4f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.2f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Proposal] = 0.6f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Have you seen my mommy?" });
            InObj.PhraseTable[2].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[2].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;
            InObj.PhraseTable[2].PhraseProperties[(int)PhraseTypes.Greeting] = 0.2f;
            InObj.PhraseTable[2].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Mommy! Mommy!" });
            InObj.PhraseTable[3].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[3].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;
            InObj.PhraseTable[3].PhraseProperties[(int)PhraseTypes.Greeting] = 0.1f;
            InObj.PhraseTable[3].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Wanna buy my little sister?" });
            InObj.PhraseTable[4].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[4].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;
            InObj.PhraseTable[4].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.6f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Golly!" });
            InObj.PhraseTable[5].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[5].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.7f;
            InObj.PhraseTable[5].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.3f;
            InObj.PhraseTable[5].PhraseProperties[(int)PhraseTypes.Yes] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Really?" });
            InObj.PhraseTable[6].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[6].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.8f;
            InObj.PhraseTable[6].PhraseProperties[(int)PhraseTypes.No] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I have a spider" });
            InObj.PhraseTable[7].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.2f;
            InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.Greeting] = 0.1f;
            InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "My dad can beat up your dad" });
            InObj.PhraseTable[8].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;
            InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Threat] = 1.0f;
            InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Retreat] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Its OK" });
            InObj.PhraseTable[9].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.5f;
            InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.Yes] = 1.0f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You won't get a spanking" });
            InObj.PhraseTable[10].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[10].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.4f;
            InObj.PhraseTable[10].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Does that hurt?" });
            InObj.PhraseTable[11].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[11].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Cut it out!" });
            InObj.PhraseTable[12].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[12].PhraseProperties[(int)PhraseTypes.Threat] = 0.4f;
            InObj.PhraseTable[12].PhraseProperties[(int)PhraseTypes.No] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You're going to get it!" });
            InObj.PhraseTable[13].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[13].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[13].PhraseProperties[(int)PhraseTypes.Threat] = 1.0f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I'm telling on you!" });
            InObj.PhraseTable[14].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[14].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[14].PhraseProperties[(int)PhraseTypes.Greeting] = 1.0f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "What's that smell?" });
            InObj.PhraseTable[15].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[15].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[15].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;
            InObj.PhraseTable[15].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Johnny has been temporarily disabled by the NSA" });
            InObj.PhraseTable[16].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[16].PhraseProperties[(int)PhraseTypes.Yes] = 0.2f;
            InObj.PhraseTable[16].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "My mom said not to wipe my boogers on people, they don't like it." });
            InObj.PhraseTable[17].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[17].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[17].PhraseProperties[(int)PhraseTypes.Greeting] = 1.0f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = " Today I found a frog.  I tried really hard not to hurt him, but I smooshed his leg.  I think thats why he won't eat the flies I caught for him." });
            InObj.PhraseTable[18].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[18].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;
            InObj.PhraseTable[18].PhraseProperties[(int)PhraseTypes.Ramble] = 1.0f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Are there any mud puddles around here?" });
            InObj.PhraseTable[19].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I don't know what to say." });
            InObj.PhraseTable[20].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Greeting] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Threat] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Proposal] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Yes] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.No] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Ramble] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.1f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You're lying" });
            InObj.PhraseTable[21].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[21].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.2f;
            InObj.PhraseTable[21].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;
            InObj.PhraseTable[21].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You have bad breath." });
            InObj.PhraseTable[22].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.Greeting] = 0.2f;
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.Retreat] = 0.2f;
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.3f;
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.4f;
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.2f;
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.No] = 0.2f;
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;
 
            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You're a poo poo head" });
            InObj.PhraseTable[23].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[23].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "If you were a triangle you would be obtuse, cause your fat and confusing" });
            InObj.PhraseTable[24].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[24].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You couldn't find your ass with both hands" });
            InObj.PhraseTable[25].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[25].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Why are you so dumb?  Did your mommy drop you?" });
            InObj.PhraseTable[26].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Your mommas so fat..." });
            InObj.PhraseTable[27].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[27].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;


            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Guess what I saw my older sister doing." });
            InObj.PhraseTable[28].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[28].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Do you know what my favorite thing is?" });
            InObj.PhraseTable[29].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[29].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You know what my friend and I were doing?" });
            InObj.PhraseTable[30].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[30].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "What were you doing?" });
            InObj.PhraseTable[31].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[31].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Homework" });
            InObj.PhraseTable[32].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[32].PhraseProperties[(int)PhraseTypes.GiveActivity] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Oiling their tricycle" });
            InObj.PhraseTable[33].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[33].PhraseProperties[(int)PhraseTypes.GiveActivity] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Picking their nose" });
            InObj.PhraseTable[34].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[34].PhraseProperties[(int)PhraseTypes.GiveActivity] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Know any Jokes" });
            InObj.PhraseTable[35].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[35].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.8f;
            InObj.PhraseTable[35].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You know whats red and green and really fast?  My frog after put him in the blender." });
            InObj.PhraseTable[36].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[36].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.8f;
            InObj.PhraseTable[36].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.2f;
        }        
    }
}
       
