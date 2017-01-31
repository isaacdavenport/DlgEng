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

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Greeting, Greeting" ,
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Greeting,
                    PhraseTypes.Greeting
                }
                });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Greeting",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Greeting
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Exclamation, Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Exclamation,
                    PhraseTypes.Exclamation
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Single Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Exclamation
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Threat Retreat",
                AddedOnDateTime = firstRound, Popularity = 0.9,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Threat,
                    PhraseTypes.Retreat
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestAffirmation, Give Affirmation",
                AddedOnDateTime = firstRound, Popularity = 0.4,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestAffirmation,
                    PhraseTypes.GiveAffirmation
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Request Affirmation Give Joke",
                AddedOnDateTime = firstRound, Popularity = 0.5,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestAffirmation,
                    PhraseTypes.GiveJoke
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Request Joke, Give Joke",
                AddedOnDateTime = firstRound, Popularity = 0.9,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestJoke,
                    PhraseTypes.GiveJoke
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveSurprisingSt, GiveDisbelief",
                AddedOnDateTime = firstRound, Popularity = 0.3,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.GiveSurprisingStatement,
                    PhraseTypes.GiveDisbelief
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "YesNoQuestion, No",
                AddedOnDateTime = firstRound, Popularity = 0.7,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.YesNoQuestion,
                    PhraseTypes.No
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "YesNoQuestion, Yes",
                AddedOnDateTime = firstRound, Popularity = 0.5,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.YesNoQuestion,
                    PhraseTypes.Yes
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveSurprisingStatement Exclamation",
                AddedOnDateTime = firstRound, Popularity = 0.4,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.GiveSurprisingStatement,
                    PhraseTypes.Exclamation
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveJoke, Disbelief -That wasn't funny",
                AddedOnDateTime = firstRound, Popularity = 0.3,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.GiveJoke,
                    PhraseTypes.GiveDisbelief
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Insult, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Insult,
                    PhraseTypes.Insult,
                    PhraseTypes.Insult
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestAffirmation, Insult, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestAffirmation,
                    PhraseTypes.Insult,
                    PhraseTypes.Insult
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "YesNoQuestion, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.YesNoQuestion,
                    PhraseTypes.Insult
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestCatchup, Insult, Retreat",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestCatchup,
                    PhraseTypes.Insult,
                    PhraseTypes.Retreat
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name =
                    "Guess what They/I did, Guess Activity, Correct Activity ",
                AddedOnDateTime = firstRound, Popularity = 0.8,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestActivity,
                    PhraseTypes.GiveActivity,
                    PhraseTypes.GiveActivity
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "GiveJoke, Insult",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.GiveJoke,
                    PhraseTypes.Insult
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestAff GiveAff ReqCat Ramble retreat",
                AddedOnDateTime = firstRound, Popularity = 0.3,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestAffirmation,
                    PhraseTypes.GiveAffirmation,
                    PhraseTypes.RequestCatchup,
                    PhraseTypes.Ramble,
                    PhraseTypes.Retreat
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestCatchup GiveRecentHistory",
                AddedOnDateTime = firstRound, Popularity = 1.6,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestCatchup,
                    PhraseTypes.GiveRecentHistory
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "RequestCatchup, Ramble, Shutup",
                AddedOnDateTime = firstRound, Popularity = 0.7,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestActivity,
                    PhraseTypes.Ramble,
                    PhraseTypes.ShutUp
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Shutup",
                AddedOnDateTime = firstRound, Popularity = 0.15,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Insult,
                    PhraseTypes.ShutUp
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Insult, Insult, Exc",
                AddedOnDateTime = firstRound, Popularity = 0.1,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Insult,
                    PhraseTypes.Insult,
                    PhraseTypes.Insult,
                    PhraseTypes.Exclamation
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Insult, Threat, Retreat",
                AddedOnDateTime = firstRound, Popularity = 0.1,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.Insult,
                    PhraseTypes.Threat,
                    PhraseTypes.Retreat
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "ReqAct, GuessAct, CorrectAct ",
                AddedOnDateTime = firstRound, Popularity = 0.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestActivity,
                    PhraseTypes.GiveActivity,
                    PhraseTypes.GiveActivity
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "ReqMotiv, Givemotiv",
                AddedOnDateTime = firstRound, Popularity = 1.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestMotivation,
                    PhraseTypes.GiveMotivation
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "ReqAdv,  GiveAdv",
                AddedOnDateTime = firstRound, Popularity = 1.3,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestAdvice,
                    PhraseTypes.GiveAdvice
                }
            });
            
            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "Request location Give location",
                AddedOnDateTime = firstRound, Popularity = 2.1,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.RequestLocation,
                    PhraseTypes.GiveLocation
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "At Schoolhouse, Silent SH",
                AddedOnDateTime = adventureRound,
                Popularity = 3.2,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.AtSchoolhouse,
                    PhraseTypes.SHSilence
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog{
                Name = "CB SM Script 1 innuendo",
                AddedOnDateTime = new DateTime(2016, 6, 18), Popularity = 3.1,
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.SmCb_01A,
                    PhraseTypes.SmCb_01B,
                    PhraseTypes.SmCb_01C,
                    PhraseTypes.SmCb_01D,
                    PhraseTypes.SmCb_01E
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog {
                Name = "LM01_CM+SB_Fight",
                AddedOnDateTime = adventureRound,
                Popularity = 42.2,
                Adventure = "LM",
                Provides =  new List<string> { "LM01_CM+SB_Fight" },
                PhraseTypeSequence = new List<PhraseTypes> {
                    PhraseTypes.LM01A,
                    PhraseTypes.LM01B,
                    PhraseTypes.LM01C,
                    PhraseTypes.LM01D,
                    PhraseTypes.LM01E,
                    PhraseTypes.LM01F
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog {
                Name = "LM02_SM+SB_Why_Fight_Corroberate",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM01_CM+SB_Fight"},
                Provides = new List<string>{ "LM02_SM+SB_Why_Fight_Corroberate", "LM4_Enabled"},
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM02A,
                   PhraseTypes.LM02B,
                   PhraseTypes.LM02C,
                   PhraseTypes.LM02D,
                   PhraseTypes.LM02E,
                   PhraseTypes.LM02F,
                   PhraseTypes.LM02G,
                   PhraseTypes.LM02H,
                   PhraseTypes.LM02I
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM03A,
                   PhraseTypes.LM03B,
                   PhraseTypes.LM03C,
                   PhraseTypes.LM03D,
                   PhraseTypes.LM03E,
                   PhraseTypes.LM03F,
                   PhraseTypes.LM03G,
                   PhraseTypes.LM03H
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM04_SB+CM_What_Else_Besides_2Dollars",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM4_Enabled" },
                Provides = new List<string> { "LM9_Enabled", "LM8_Enabled" },
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM04A,
                   PhraseTypes.LM04B,
                   PhraseTypes.LM04C,
                   PhraseTypes.LM04D,
                   PhraseTypes.LM04E
                }
            });

            inObj.ModelDialogs.Add(new ModelDialog
            {
                Name = "LM05_SB+RL_Can_You_Corroberate",
                AddedOnDateTime = adventureRound,
                Popularity = 2.2,
                Adventure = "LM",
                Requires = new List<string> { "LM02_SM+SB_Why_Fight_Corroberate" },
                Provides = new List<string> { "LM05_SB+RL_Can_You_Corroberate", "LM4_Enabled" },
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM05A,
                   PhraseTypes.LM05B,
                   PhraseTypes.LM05C,
                   PhraseTypes.LM05D,
                   PhraseTypes.LM05E,
                   PhraseTypes.LM05F
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM06A,
                   PhraseTypes.LM06B,
                   PhraseTypes.LM06C
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM07A,
                   PhraseTypes.SHSilence
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM08A,
                   PhraseTypes.SHSilence
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM09A,
                   PhraseTypes.LM09B,
                   PhraseTypes.LM09C,
                   PhraseTypes.LM09D,
                   PhraseTypes.LM09E,
                   PhraseTypes.LM09F
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM10A,
                   PhraseTypes.LM10B,
                   PhraseTypes.LM10C,
                   PhraseTypes.LM10D,
                   PhraseTypes.LM10E,
                   PhraseTypes.LM10F,
                   PhraseTypes.LM10G
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM11A,
                   PhraseTypes.LM11B
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM13A,
                   PhraseTypes.LM13B,
                   PhraseTypes.LM13C,
                   PhraseTypes.LM13D,
                   PhraseTypes.LM13E,
                   PhraseTypes.LM13F,
                   PhraseTypes.LM13G,
                   PhraseTypes.LM13H
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM14A,
                   PhraseTypes.LM14B,
                   PhraseTypes.LM14C,
                   PhraseTypes.LM14D,
                   PhraseTypes.LM14E,
                   PhraseTypes.LM14F,
                   PhraseTypes.LM14G
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM15A,
                   PhraseTypes.LM15B,
                   PhraseTypes.LM15C,
                   PhraseTypes.LM15D,
                   PhraseTypes.LM15E
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM16A,
                   PhraseTypes.LM16B
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM17A,
                   PhraseTypes.LM17B,
                   PhraseTypes.LM17C
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
                PhraseTypeSequence = new List<PhraseTypes>{
                   PhraseTypes.LM18A,
                   PhraseTypes.SHSilence                }
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
