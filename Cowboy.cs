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

    public static class InitCowboy 
    {
        public static void SetDefaults(Character InObj) 
        {
            InObj.CharacterName = "Cowboy Bill";
            InObj.Gender = GenderType.Male;
            InObj.Age = 52;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Howdy" });
            InObj.PhraseTable[0].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.1f;
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.Greeting] = 1.0f;
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.4f;
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;
            InObj.PhraseTable[0].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "These spurs ain't afraid to kick up some dust." });
            InObj.PhraseTable[1].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.3f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Threat] = 0.5f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.Yes] = 0.2f;
            InObj.PhraseTable[1].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Lets us hunker down here for a minute." });
            InObj.PhraseTable[2].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[2].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[2].PhraseProperties[(int)PhraseTypes.Yes] = 0.1f;
            InObj.PhraseTable[2].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I'll be" });
            InObj.PhraseTable[3].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[3].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[3].PhraseProperties[(int)PhraseTypes.Yes] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = " That ain't cowpoke work your talkin bout there." });
            InObj.PhraseTable[4].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[4].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[4].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "What did you think I meant when I said posse?" });
            InObj.PhraseTable[5].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[5].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;
            InObj.PhraseTable[5].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Calm down there big feller." });
            InObj.PhraseTable[6].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[6].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.2f;
            InObj.PhraseTable[6].PhraseProperties[(int)PhraseTypes.Retreat] = 0.7f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Well sir you might want to take that up with the local chaplain" });
            InObj.PhraseTable[7].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.No] = 0.2f;
            InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.2f;
            InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.Proposal] = 0.3f;
            InObj.PhraseTable[7].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "What in tarnation?" });
            InObj.PhraseTable[8].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[8].PhraseProperties[(int)PhraseTypes.No] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Wrap me in calf-leather and call me an ankle biter." });
            InObj.PhraseTable[9].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.4f;
            InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.No] = 0.2f;
            InObj.PhraseTable[9].PhraseProperties[(int)PhraseTypes.Retreat] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Is that you George?" });
            InObj.PhraseTable[10].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[10].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;
            InObj.PhraseTable[10].PhraseProperties[(int)PhraseTypes.Greeting] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Keep your poweder dry boys." });
            InObj.PhraseTable[11].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[11].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[11].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Those two gonna get hitched?" });
            InObj.PhraseTable[12].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[12].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;
            InObj.PhraseTable[12].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Its quiet out here.  Too quiet. " });
            InObj.PhraseTable[13].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[13].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[13].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 1.0f;
            InObj.PhraseTable[13].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Yup" });
            InObj.PhraseTable[14].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[14].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.2f;
            InObj.PhraseTable[14].PhraseProperties[(int)PhraseTypes.Yes] = 1.0f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Don't get your knickers in a wad" });
            InObj.PhraseTable[15].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[15].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[15].PhraseProperties[(int)PhraseTypes.Threat] = 1.0f;
            InObj.PhraseTable[15].PhraseProperties[(int)PhraseTypes.Insult] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I think my nose was assaulted by a skunk that ate a case of rotten cabbage" });
            InObj.PhraseTable[16].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[16].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[16].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.4f;
            InObj.PhraseTable[16].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.4f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Lets vamoos" });
            InObj.PhraseTable[17].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[17].PhraseProperties[(int)PhraseTypes.Retreat] = 0.2f;
            InObj.PhraseTable[17].PhraseProperties[(int)PhraseTypes.Proposal] = 0.4f;
            InObj.PhraseTable[17].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "That makes about as much sense as whiskey on pancakes" });
            InObj.PhraseTable[18].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[18].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.1f;
            InObj.PhraseTable[18].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;
            InObj.PhraseTable[18].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I don't know what to say." });
            InObj.PhraseTable[19].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Greeting] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Threat] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Proposal] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Yes] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.No] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Ramble] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.1f;
            InObj.PhraseTable[19].PhraseProperties[(int)PhraseTypes.Insult] = 0.1f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Ain't that purdy as a pig under a Christmas Tree" });
            InObj.PhraseTable[20].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.Yes] = 0.2f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.2f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.2f;
            InObj.PhraseTable[20].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.1f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "That is so hot it would catch fire faster than a meth lab" });
            InObj.PhraseTable[21].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[21].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.3f;
            InObj.PhraseTable[21].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
    
            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "The TSA Wouldn't let me wear my spurs through the metal detector.  What am I going to do ride the captain?" });
            InObj.PhraseTable[22].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.4f;
            InObj.PhraseTable[22].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.2f;
    
            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Thats about as significant as a fart in the wind" });
            InObj.PhraseTable[23].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[23].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[23].PhraseProperties[(int)PhraseTypes.No] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "The day's movin slower than molasses in January" });
            InObj.PhraseTable[24].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[24].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[24].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.3f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Lord, I promise I'll be better." });
            InObj.PhraseTable[25].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[25].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;
            InObj.PhraseTable[25].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.2f;
            InObj.PhraseTable[25].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Can't rightly say" });
            InObj.PhraseTable[26].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Greeting] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Threat] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Retreat] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Proposal] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Yes] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.No] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.GiveAffirmation] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.GiveDisbelief] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.GiveRecentHistory] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.GiveSurprisingStatement] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.Ramble] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.ShutUp] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.1f;
            InObj.PhraseTable[26].PhraseProperties[(int)PhraseTypes.GiveJoke] = 0.1f;

            
            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Some days, when the light is just right comin over the hill, \r\nI think I can see ole Bob up the ridge. Bob never was much one for words, \r\nbut he had a knack. Take that time right before the flood on the big Thompson.  \r\nMust have been something he sniffed out in the loweing of the herd, \r\nbut Bob got them doggies up the side of the hill before we knew what was happening.  \r\nWe didn't even manage to break camp and lost two weeks worth of flour and cooking oil, not to mention my favorite knife.  Got that knife of an Injun up in Gold Hill..." 
            });
            InObj.PhraseTable[27].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[27].PhraseProperties[(int)PhraseTypes.Ramble] = 0.9f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Ready to saddle up?" });
            InObj.PhraseTable[28].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[28].PhraseProperties[(int)PhraseTypes.Proposal] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You lily livered coward-" });
            InObj.PhraseTable[29].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[29].PhraseProperties[(int)PhraseTypes.Insult] = 0.7f;
            InObj.PhraseTable[29].PhraseProperties[(int)PhraseTypes.Exclamation] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You got shit form brains and your heart pumps pee pee" });
            InObj.PhraseTable[30].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[30].PhraseProperties[(int)PhraseTypes.Insult] = 0.5f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Guess what them dogies was up to this afternoon" });
            InObj.PhraseTable[31].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[31].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.5f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You know what my Isabelle been up to?" });
            InObj.PhraseTable[32].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[32].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.5f;
            InObj.PhraseTable[32].PhraseProperties[(int)PhraseTypes.RequestCatchup] = 0.2f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You know what I been doin with my new spurs" });
            InObj.PhraseTable[33].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[33].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.5f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "You will never guess what I did with my hat" });
            InObj.PhraseTable[34].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[34].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.5f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Birthing cattle" });
            InObj.PhraseTable[35].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[35].PhraseProperties[(int)PhraseTypes.GiveActivity] = 0.5f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Fornicating" });
            InObj.PhraseTable[36].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[36].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.5f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "Branding em with the big L and lazy P" });
            InObj.PhraseTable[37].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[37].PhraseProperties[(int)PhraseTypes.RequestActivity] = 0.5f;

            InObj.PhraseTable.Add(new PhraseTableEntry() { DialogStr = "I could use some cheering up pardner" });
            InObj.PhraseTable[38].PhraseProperties = new float[(int)PhraseTypes.PhraseTypesSize];
            InObj.PhraseTable[38].PhraseProperties[(int)PhraseTypes.RequestJoke] = 0.8f;
            InObj.PhraseTable[38].PhraseProperties[(int)PhraseTypes.RequestAffirmation] = 0.4f;
        }        
    }
}
       
