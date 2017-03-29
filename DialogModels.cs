using System;
using System.Collections.Generic;


namespace DialogEngine
{
    public static class InitModelDialogs
    {
        //TODO this could be moved to a collection as well like the Character Objects and inheret popularity for statistical selection of elements
        public static void SetDefaults(DialogTracker inObj) {
            DateTime firstRound = new DateTime(2016, 5, 18);
            DateTime adventureRound = new DateTime(2016, 9, 17);

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "Greeting,Greeting,RequestCatchup,GiveRecentHistory,GiveSurprisingStatementGiveRecentHistory,GiveDisbelief,RequestAdvice,GiveJoke,Threat",
                AddedOnDateTime = firstRound,
                Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "Greeting",
                    "Greeting",
                    "RequestCatchup",
                    "GiveRecentHistory",
                    "GiveSurprisingStatement",
                    "GiveRecentHistory",
                    "GiveDisbelief",
                    "RequestAdvice",
                    "GiveJoke",
                    "Threat"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Greeting, Greeting" ,
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "Greeting",
                    "Greeting"
                }
                });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Greeting",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "Greeting"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "Exclamation, Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "Exclamation",
                    "Exclamation"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "Single Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "Exclamation"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "Threat Retreat",
                AddedOnDateTime = firstRound,
                Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "Threat",
                    "Retreat"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "RequestAffirmation, GiveAffirmation",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "RequestAffirmation",
                    "GiveAffirmation"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "RequestAffirmation, GiveJoke",
                AddedOnDateTime = firstRound, Popularity = 0.9,
                PhraseTypeSequence = new List<string> {
                    "RequestJoke",
                    "GiveJoke"
                }
            });
        
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveSurprisingSt, GiveDisbelief",
                AddedOnDateTime = firstRound, Popularity = 0.3,
                PhraseTypeSequence = new List<string> {
                    "GiveSurprisingStatement",
                    "GiveDisbelief"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "YesNoQuestion, No",
                AddedOnDateTime = firstRound, Popularity = 0.7,
                PhraseTypeSequence = new List<string> {
                    "YesNoQuestion",
                    "No"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "YesNoQuestion, Yes",
                AddedOnDateTime = firstRound, Popularity = 0.5,
                PhraseTypeSequence = new List<string> {
                    "YesNoQuestion",
                    "Yes"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveSurprisingStatement Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.4,
                PhraseTypeSequence = new List<string> {
                    "GiveSurprisingStatement",
                    "Exclamation"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveJoke, Disbelief -That wasn't funny",
                AddedOnDateTime = firstRound, Popularity = 0.3,
                PhraseTypeSequence = new List<string> {
                    "GiveJoke",
                    "GiveDisbelief"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Insult, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "Insult",
                    "Insult",
                    "Insult"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestAffirmation, Insult, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "RequestAffirmation",
                    "Insult",
                    "Insult"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "YesNoQuestion, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "YesNoQuestion",
                    "Insult"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestCatchup, Insult, Retreat",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "RequestCatchup",
                    "Insult",
                    "Retreat"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name =
                    "Guess what They/I did, Guess Activity, Correct Activity ",
                AddedOnDateTime = firstRound, Popularity = 0.8,
                PhraseTypeSequence = new List<string> {
                    "RequestActivity",
                    "GiveActivity",
                    "GiveActivity"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveJoke, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "GiveJoke",
                    "Insult"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestAff GiveAff ReqCat Ramble retreat",
                AddedOnDateTime = firstRound, Popularity = 0.3,
                PhraseTypeSequence = new List<string> {
                    "RequestAffirmation",
                    "GiveAffirmation",
                    "RequestCatchup",
                    "Ramble",
                    "Retreat"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestCatchup GiveRecentHistory",
                AddedOnDateTime = firstRound, Popularity = 1.6,
                PhraseTypeSequence = new List<string> {
                    "RequestCatchup",
                    "GiveRecentHistory"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestCatchup, Ramble, Shutup",
                AddedOnDateTime = firstRound, Popularity = 0.7,
                PhraseTypeSequence = new List<string> {
                    "RequestActivity",
                    "Ramble",
                    "ShutUp"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Shutup",
                AddedOnDateTime = firstRound, Popularity = 0.15,
                PhraseTypeSequence = new List<string> {
                    "Insult",
                    "ShutUp"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Insult, Insult, Exc",
                AddedOnDateTime = firstRound, Popularity = 0.1,
                PhraseTypeSequence = new List<string> {
                    "Insult",
                    "Insult",
                    "Insult",
                    "Exclamation"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Threat, Retreat",
                AddedOnDateTime = firstRound, Popularity = 0.1,
                PhraseTypeSequence = new List<string> {
                    "Insult",
                    "Threat",
                    "Retreat"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "ReqAct, GuessAct, CorrectAct ",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<string> {
                    "RequestActivity",
                    "GiveActivity",
                    "GiveActivity"
                }
            });
            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "ReqMotiv, Givemotiv",
                AddedOnDateTime = firstRound,
                Popularity = 1.2,
                PhraseTypeSequence = new List<string> {
                    "RequestMotivation",
                    "GiveMotivation"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "ReqAdv,  GiveAdv",
                AddedOnDateTime = firstRound, Popularity = 1.3,
                PhraseTypeSequence = new List<string> {
                    "RequestAdvice",
                    "GiveAdvice"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "Request location Give location",
                AddedOnDateTime = firstRound, Popularity = 2.1,
                PhraseTypeSequence = new List<string> {
                    "RequestLocation",
                    "GiveLocation"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "Request who Give who",
                AddedOnDateTime = firstRound, Popularity = 2.1,
                PhraseTypeSequence = new List<string> {
                    "RequestWho",
                    "GiveWho"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "Request who insult",
                AddedOnDateTime = firstRound, Popularity = 2.1,
                PhraseTypeSequence = new List<string> {
                    "RequestWho",
                    "Insult"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestMotivation, Insult",
                AddedOnDateTime = firstRound, Popularity = 1.2,
                PhraseTypeSequence = new List<string> {
                    "RequestMotivation",
                    "Insult"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "ReqAdv,  GiveAdv",
                AddedOnDateTime = firstRound, Popularity = 1.3,
                PhraseTypeSequence = new List<string> {
                    "RequestAdvice",
                    "Insult"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Request location, Insult",
                AddedOnDateTime = firstRound, Popularity = 2.1,
                PhraseTypeSequence = new List<string> {
                    "RequestLocation",
                    "Insult"
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "CB SM Script 1 innuendo",
                AddedOnDateTime = new DateTime(2016, 6, 18), Popularity = 3.1,
                PhraseTypeSequence = new List<string> {
                    "SmCb_01A",
                    "SmCb_01B",
                    "SmCb_01C",
                    "SmCb_01D",
                    "SmCb_01E"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog {
                Name = "Lunch Money Adventure LM01 CM+SB Start Fight",
                AddedOnDateTime = adventureRound, Popularity = 32.2,
                Adventure = "LM",
                Provides =  new List<string> { "LM01_CM+SB_Fight" },
                PhraseTypeSequence = new List<string> {
                    "LM01A",
                    "LM01B",
                    "LM01C",
                    "LM01D",
                    "LM01E",
                    "LM01F"
                }
            });

            inObj.ModelDialogs.Add(item: new ModelDialog {
                Name = "LM02_SM+SB_Why_Fight_Corroberate",
                AddedOnDateTime = adventureRound, Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM01_CM+SB_Fight"},
                Provides = new List<string>{ "LM02_SM+SB_Why_Fight_Corroberate", "LM4_Enabled"},
                PhraseTypeSequence = new List<string>{
                   "LM02A",
                   "LM02B",
                   "LM02C",
                   "LM02D",
                   "LM02E",
                   "LM02F",
                   "LM02G",
                   "LM02H",
                   "LM02I"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM03_CM+RL_Why_Fight",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM01_CM+SB_Fight" },
                Provides = new List<string> { "LM03_CM+RL_Why_Fight" },
                PhraseTypeSequence = new List<string>{
                   "LM03A",
                   "LM03B",
                   "LM03C",
                   "LM03D",
                   "LM03E",
                   "LM03F",
                   "LM03G",
                   "LM03H"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog()
            {
                Name = "LM04_SB+CM_What_Else_Besides_2Dollars",
                AddedOnDateTime = adventureRound, Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM4_Enabled" },
                Provides = new List<string> { "LM9_Enabled", "LM8_Enabled" },
                PhraseTypeSequence = new List<string>{
                   "LM04A",
                   "LM04B",
                   "LM04C",
                   "LM04D",
                   "LM04E"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM05_SB+RL_Can_You_Corroberate",
                AddedOnDateTime = adventureRound, Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM02_SM+SB_Why_Fight_Corroberate" },
                Provides = new List<string> { "LM05_SB+RL_Can_You_Corroberate", "LM4_Enabled" },
                PhraseTypeSequence = new List<string>{
                   "LM05A",
                   "LM05B",
                   "LM05C",
                   "LM05D",
                   "LM05E",
                   "LM05F"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM06_CM+SC_Better_To_Forgive",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM03_CM+RL_Why_Fight" },
                Provides = new List<string> { "LM10_Enabled", "LM06_CM+SC_Better_To_Forgive" },
                PhraseTypeSequence = new List<string>{
                   "LM06A",
                   "LM06B",
                   "LM06C"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM07_CM+SH_Kitty_Found",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM03_CM+RL_Why_Fight" },
                Provides = new List<string> { "LM_END" },
                PhraseTypeSequence = new List<string>{
                   "LM07A",
                   "SHSilence"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM08_SB+SH_Digging_Bad_Dropped_Money",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM8_Enabled" },
                Provides = new List<string> { "LM08_SB+SH_Digging_Bad_Dropped_Money" },
                PhraseTypeSequence = new List<string>{
                   "LM08A",
                   "SHSilence"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM09_SB+CM_Gretchen_Win",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM9_Enabled" },
                Provides = new List<string> { "LM_END" },
                PhraseTypeSequence = new List<string>{
                   "LM09A",
                   "LM09B",
                   "LM09C",
                   "LM09D",
                   "LM09E",
                   "LM09F"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM10_SB+CM_Forgiven_Kitty_Located",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM10_Enabled" },
                Provides = new List<string> { "LM_END" },
                PhraseTypeSequence = new List<string>{
                   "LM10A",
                   "LM10B",
                   "LM10C",
                   "LM10D",
                   "LM10E",
                   "LM10F",
                   "LM10G"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM11_SC+CM_I_Sense_Kitty",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM06_CM+SC_Better_To_Forgive" },
                Provides = new List<string> { "LM11_SC+CM_I_Sense_Kitty", "LM10_Enabled" },
                PhraseTypeSequence = new List<string>{
                   "LM11A",
                   "LM11B"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog()
            {
                Name = "LM13_SB+SC_Eric_likes_Gretchen_Kitty",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM05_SB+RL_Can_You_Corroberate" },
                Provides = new List<string> { "LM9_Enabled", "LM8_Enabled" },
                PhraseTypeSequence = new List<string>{
                   "LM13A",
                   "LM13B",
                   "LM13C",
                   "LM13D",
                   "LM13E",
                   "LM13F",
                   "LM13G",
                   "LM13H"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog()
            {
                Name = "LM14_SB+SC_Eric_likes_Gretchen_Kitty",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM05_SB+RL_Can_You_Corroberate" },
                Provides = new List<string> { "LM14_SB+SC_Eric_likes_Gretchen_Kitty" },
                PhraseTypeSequence = new List<string>{
                   "LM14A",
                   "LM14B",
                   "LM14C",
                   "LM14D",
                   "LM14E",
                   "LM14F",
                   "LM14G"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog()
            {
                Name = "LM15_CM+SM_Kittys_Not_A_Beast",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM11_SC+CM_I_Sense_Kitty" },
                Provides = new List<string> { "LM_END" },
                PhraseTypeSequence = new List<string>{
                   "LM15A",
                   "LM15B",
                   "LM15C",
                   "LM15D",
                   "LM15E"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog()
            {
                Name = "LM16_SB+CM_Heres_Your_2Dollars",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM08_SB+SH_Digging_Bad_Dropped_Money" },
                Provides = new List<string> { "LM_END" },
                PhraseTypeSequence = new List<string>{
                   "LM16A",
                   "LM16B"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog()
            {
                Name = "LM17_SB+SM_Lunchlady_Dropped_Dollars",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM08_SB+SH_Digging_Bad_Dropped_Money" },
                Provides = new List<string> { "LM_END" },
                PhraseTypeSequence = new List<string>{
                   "LM17A",
                   "LM17B",
                   "LM17C"
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog()
            {
                Name = "LM18_RL+SH_Found_Billy",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM14_SB+SC_Eric_likes_Gretchen_Kitty" },
                Provides = new List<string> { "LM_END" },
                PhraseTypeSequence = new List<string>{
                   "LM18A",
                   "SHSilence"                }
            });


            inObj.DialogModelPopularitySum = 0;
            foreach (var currentDialog in inObj.ModelDialogs) {
                inObj.DialogModelPopularitySum += currentDialog.Popularity;
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
