using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading; // for thread.sleep()
// ReSharper disable ConditionIsAlwaysTrueOrFalse


// for count on keyboard inputs to manually force which characters speak 


namespace DialogEngine
{
    public enum PhraseTypes
    {  //TODO make these strings instead of an enumeration for model dialogs and character phrase tags
        Exclamation,
        Greeting,
        Threat,
        Retreat,
        YesNoQuestion,
        Yes,
        No,
        RequestCatchup,
        GiveAffirmation,
        RequestAffirmation,
        GiveDisbelief,
        GiveRecentHistory,
        GiveSurprisingStatement,
        Ramble,
        ShutUp,
        RequestJoke,
        GiveJoke,
        Insult,
        RequestActivity,
        GiveActivity,
        RequestAdvice,
        GiveAdvice,
        RequestMotivation,
        GiveMotivation,
        RequestLocation,
        GiveLocation,
        AtSchoolhouse,
        SmCb_01A,SmCb_01B,SmCb_01C,SmCb_01D,SmCb_01E,
        SHSilence,
        LM01A,LM01B,LM01C,LM01D,LM01E,LM01F,LM02A,LM02B,LM02C,LM02D,LM02E,LM02F,LM02G,LM02H,LM02I,LM03A,LM03B,LM03C,LM03D,LM03E,LM03F,
        LM03G,LM03H,LM04A,LM04B,LM04C,LM04D,LM04E,LM05A,LM05B,LM05C,LM05D,LM05E,LM05F,LM06A,LM06B,LM06C,LM07A,LM08A,LM09A,
        LM09B,LM09C,LM09D,LM09E,LM09F,LM10A,LM10B,LM10C,LM10D,LM10E,LM10F,LM10G, LM11A, LM11B, LM13A,LM13B,LM13C,LM13D,LM13E,LM13F,
        LM13G,LM13H,LM14A,LM14B,LM14C,LM14D,LM14E,LM14F,LM14G,LM15A,LM15B,LM15C,LM15D,LM15E,LM16A,LM16B,LM17A,LM17B,LM17C,LM18A,
        PhraseTypesSize
    }

    public enum ParentalRating
    {
        G,
        PG,
        PG13,
        R,
        X
    }

    public static class SessionVars
    {
        public static readonly bool DebugFlag = Convert.ToBoolean(AppSet.ReadSetting("DebugFlag"));
        public static readonly bool ForceCharactersAndDialogModel = Convert.ToBoolean(AppSet.ReadSetting("ForceCharactersAndDialogModel"));
        public static readonly bool ShowDupePhrases = Convert.ToBoolean(AppSet.ReadSetting("ShowDupePhrases"));
        public static readonly bool HeatMapFullMatrixDispMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapFullMatrixDispMode"));
        public static readonly bool HeatMapSumsMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapSumsMode"));
        public static readonly bool HeatMapOnlyMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapOnlyMode"));
        public static readonly bool WriteSerialLog = Convert.ToBoolean(AppSet.ReadSetting("WriteSerialLog"));
        public static readonly bool NoSerialPort = Convert.ToBoolean(AppSet.ReadSetting("NoSerialPort"));
        public static readonly bool CheckStuckTransmissions = Convert.ToBoolean(AppSet.ReadSetting("CheckStuckTransmissions"));
        public static readonly bool MonitorReceiveBufferSize = Convert.ToBoolean(AppSet.ReadSetting("MonitorReceiveBufferSize"));
        public static readonly bool MonitorMessageParseFails = Convert.ToBoolean(AppSet.ReadSetting("MonitorMessageParseFails"));
        public static readonly ParentalRating CurrentParentalRating = 
            (ParentalRating)Enum.Parse(typeof(ParentalRating), AppSet.ReadSetting("CurrentParentalRating"));
        public static readonly string LogsDirectory = AppSet.ReadSetting("LogsDirectory");
        public static readonly string AudioDirectory = AppSet.ReadSetting("AudioDirectory");
        public static readonly string DecimalSerialLogFileName = AppSet.ReadSetting("DecimalSerialLogFileName");
        public static readonly string HexSerialLogFileName = AppSet.ReadSetting("HexSerialLogFileName");
        public static readonly string LogTheDialogFileName = AppSet.ReadSetting("LogTheDialogFileName");
    }

    public class PhraseEntry
    {
        public string DialogStr;
        public string FileName;
        public Dictionary<PhraseTypes, double> PhraseWeights;
        public ParentalRating PhraseRating;
    }

    public class Character
    {
        public string CharacterName { get; protected set; }
        public string CharacterPrefix { get; protected set; }
        public PhraseEntry PhraseTotals = new PhraseEntry();
        public List<PhraseEntry> Phrases = new List<PhraseEntry>();
        // A character's Phrases list holds all the phrases they might say along with 
        // heuristic phraseWeights on what parts of a model dialog they might use them in.
        protected const int RecentPhrasesQueueSize = 8;
        public Queue<PhraseEntry> RecentPhrases = new Queue<PhraseEntry>();  //TODO make this a method that runs over the history
    }

    public class ModelDialog
    {
        // a ModelDialog is a sequence of phrase types that represent an exchange between characters 
        // the model dialog will be filled with randomly selected character phrases of the appropriate phrase type
        public string Name;
        public DateTime AddedOnDateTime = new DateTime(2016, 1, 2, 3, 4, 5);
        public double Popularity = 1.0;
        public string Adventure = "";
        public List<string> Requires = new List<string>();
        public List<string> Provides = new List<string>();
        public List<PhraseTypes> PhraseTypeSequence = new List<PhraseTypes>();

        public bool AreDialogsRequirementsMet() {

            return true;
        }
    }

    public static class RandomNumbers
    {
        public static Random Gen = new Random();
    }

    public class Program
    {
        public static DialogTracker TheDialogs = new DialogTracker();

        static void WriteStartupInfo() {
            string versionTimeStr = "Dialog Engine ver 0.33 Isaac, Aria, Joe, Brielle " + DateTime.Now + "\r\n";
            Console.WriteLine(versionTimeStr);
            if (SessionVars.WriteSerialLog)
            {

                using (StreamWriter serialLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.HexSerialLogFileName), true))
                {
                    serialLog.WriteLine("");
                    serialLog.WriteLine("");
                    serialLog.WriteLine(versionTimeStr);
                    serialLog.Close();
                }
                using (StreamWriter serialLogDec = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DecimalSerialLogFileName), true))
                {
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine(versionTimeStr);
                    serialLogDec.Close();
                }
                using (StreamWriter serialLogDialog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.LogTheDialogFileName), true))
{
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine(versionTimeStr);
                    serialLogDialog.Close();
                }
            }
        }

        static void CheckForMissingAudioFiles() {
            foreach (var character in TheDialogs.CharacterList)
            {
                foreach (PhraseEntry phrase in character.Phrases)
                {
                    if (!File.Exists(SessionVars.AudioDirectory + character.CharacterPrefix + "_" + phrase.FileName + ".mp3"))
                    {
                        Console.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + " " + phrase.DialogStr);
                    }
                }
            }
            //TODO check that all dialog models have unique names
        }

        static void CheckAdventurePhrasesUsed() {
            for (PhraseTypes i = PhraseTypes.LM01A; i < PhraseTypes.PhraseTypesSize; i++) {
                int j = 0;
                foreach (var character in TheDialogs.CharacterList) {
                    foreach (var phrase in character.Phrases) {
                        if (phrase.PhraseWeights.ContainsKey(i)) {
                            j++;
                        }
                    }
                }
                if (j != 1) {
                    Console.WriteLine("Adventure PhraseType {0} used " + j + " times.", i.ToString());
                }
            }
        }

        static void CheckEachCharacterHasEachPhraseType() {
            //TODO create a unit test that ensure each character is minimally complete similar to CheckAdventurePhrasesUsed() and CheckForMissingAudioFiles()
        }


        static void Main(string[] args) {
            Console.SetBufferSize(Console.BufferWidth, 32766);
            WriteStartupInfo();
            SerialComs.InitSerial();
            InitModelDialogs.SetDefaults(TheDialogs);

            //Select Debug Output
            if (SessionVars.ForceCharactersAndDialogModel) {
                Console.WriteLine("   enter three numbers to set the next: DialogModel, Char1, Char2");
                Console.WriteLine();
            }

            if (SessionVars.DebugFlag) {
                CheckForMissingAudioFiles();
                CheckAdventurePhrasesUsed();
                CheckEachCharacterHasEachPhraseType();
            }

            while (true) {
                if (SessionVars.ForceCharactersAndDialogModel) {
                    string[] keyboardInput = Console.ReadLine().Split(' ');

                    //if keyboard input has three numbers for debug mode to force dialog model and characters
                    if (keyboardInput.Length == 3) {
                        int j = 0;
                        int[] modelAndCharacters = new int[3];
                        foreach (string asciiInt in keyboardInput) {
                            modelAndCharacters[j] = Int32.Parse(asciiInt);
                            j++;
                        }
                        TheDialogs.GenerateADialog(modelAndCharacters);
                    }
                    else {
                        Console.WriteLine("Incorrect input, generating random dialog.");
                        TheDialogs.GenerateADialog(); // wrong number of user input select rand dialog and characters
                    }
                }
                else {
                    if (!SessionVars.HeatMapOnlyMode) {
                        TheDialogs.GenerateADialog();  //normal operation
                        Thread.Sleep(1100);
                        Thread.Sleep(RandomNumbers.Gen.Next(0, 2000));
                    }
                    else {
                        Console.Clear();
                        if (SessionVars.HeatMapFullMatrixDispMode) {
                            SerialComs.PrintHeatMap();
                        }
                        if (SessionVars.HeatMapSumsMode) {
                            SerialComs.PrintHeatMapSums();
                        }
                        Thread.Sleep(400);
                    }
                }
            }
        }
    }
}
