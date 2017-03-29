using System;
using System.Collections.Generic;
using System.IO;
using System.Threading; // for thread.sleep()
using Newtonsoft.Json;


// TODO: JSON Input
//       Cleanup Character class for strings
//       Vectorize RSSI to see movement over proximity. (increased prox most recently, center of mass.)

namespace DialogEngine
{
    //list of strings that will contain all Phrase Types after character initialization.
    public static class GlobalPhraseTypes
    {
        public static List<string> TestPhraseTypes = new List<String> { };
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
        public static readonly bool AudioDialogsOn = Convert.ToBoolean(AppSet.ReadSetting("AudioDialogsOn"));
        public static readonly bool TextDialogsOn = Convert.ToBoolean(AppSet.ReadSetting("TextDialogsOn"));
        public static readonly bool ForceCharactersAndDialogModel = Convert.ToBoolean(AppSet.ReadSetting("ForceCharactersAndDialogModel"));
        public static readonly bool WaitIndefinatelyForMove = Convert.ToBoolean(AppSet.ReadSetting("WaitIndefinatelyForMove"));
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
        public static readonly string CharactersDirectory = AppSet.ReadSetting("CharactersDirectory");
        public static readonly string DialogsDirectory = AppSet.ReadSetting("DialogsDirectory");
        public static readonly string AudioDirectory = AppSet.ReadSetting("AudioDirectory");
        public static readonly string DecimalSerialLogFileName = AppSet.ReadSetting("DecimalSerialLogFileName");
        public static readonly string SerialLogFileName = AppSet.ReadSetting("SerialLogFileName");
        public static readonly string DialogSerialLogFileName = AppSet.ReadSetting("DialogSerialLogFileName");
    }

    public class PhraseEntry
    {
        public string DialogStr;
        public string FileName;
        public Dictionary<string, double> phraseWeights;    //to replace PhraseWeights, uses string tags.
        public ParentalRating PhraseRating;
    }

    //[JsonObject(MemberSerialization.OptIn)]
    public class Character
    {
        [JsonProperty("CharacterName")]
        public string CharacterName { get; protected set; }
        [JsonProperty("CharacterPrefix")]
        public string CharacterPrefix { get; protected set; }
        [JsonProperty("PhraseTotals")]
        public PhraseEntry PhraseTotals = new PhraseEntry();
        [JsonProperty("Phrases")]
        public List<PhraseEntry> Phrases = new List<PhraseEntry>(); //entry now has string phraseweight tags.
        // A character's Phrases list holds all the phrases they might say along with 
        // heuristic phraseWeights on what parts of a model dialog they might use them in.

        protected const int RecentPhrasesQueueSize = 4;
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
        public List<string> PhraseTypeSequence = new List<string>();
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
            string versionTimeStr = "Dialog Engine ver 0.43 Isaac, Aria, Joe " + DateTime.Now;
            Console.WriteLine(""); 
            Console.WriteLine(versionTimeStr);
            GlobalPhraseTypes.TestPhraseTypes.ForEach (Console.WriteLine) ;
            Console.Read();
            if (SessionVars.WriteSerialLog)
            {

                using (StreamWriter serialLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.SerialLogFileName), true))
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
                    (SessionVars.LogsDirectory + SessionVars.DialogSerialLogFileName), true))
{
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine(versionTimeStr);
                    serialLogDialog.Close();
                }
            }
        }

        static void CheckForMissingPhrases() {
            if (!SessionVars.AudioDialogsOn) {
                return;
            }
            foreach (var character in TheDialogs.CharacterList)
            {
                foreach (PhraseEntry phrase in character.Phrases)
                {
                    if (!File.Exists(SessionVars.AudioDirectory + character.CharacterPrefix + "_" + phrase.FileName + ".mp3"))  //Char name and prefix are being left blank...
                    {
                        Console.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + " " + phrase.DialogStr);//these are being checked with JSON input.
                    }
                }
                Console.WriteLine();
            }
            //TODO check that all dialog models have unique names
        }


        static void CheckAdventurePhrasesUsed() {
            //TODO string match adventures in DialogModels.cs or incoming JSON to strings in character JSON
            return;
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
                CheckForMissingPhrases();
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
