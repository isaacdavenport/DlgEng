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
    static class ParentalRatings
    {
        /// <summary>
        /// Static string Dictionary example
        /// </summary>
        static Dictionary<string, int> _dict = new Dictionary<string, int>
        {
            {"G", 1},
            {"PG", 2},
            {"PG13", 3},
            {"R", 4}
        };

        public static int GetNumeric(string ratingString)
        {
            // Try to get the result in the static Dictionary
            int result;
            if (_dict.TryGetValue(ratingString, out result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }
    }

    public static class SessionVars
    {
        //private static string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        // TODO work this so that if BaseDirectory doesn't exist, or CharactersDirectory, we use the 
        // directory the .exe originally ran from with the default subdirectories for LogsDirectory 
        //  of Logs and CharacterDirectory of CharacterJSON and dialogsDirectory of DialogJSON
        //Typically for imaged machines for play tests, the base directory will be ..\Desktop\T2L\ 
        //  with subdirectories like ..\Desktop\T2L\CharacterJSON and ..\Desktop\T2L\Logs as seen on
        // the install directory in google drive

        public static string exeDir = "e"; //System.IO.Path.GetDirectoryName(exePath);
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
        public static readonly string CurrentParentalRating = AppSet.ReadSetting("CurrentParentalRating");
        public static          string LogsDirectory = AppSet.ReadSetting("LogsDirectory");
        public static          string BaseDirectory = AppSet.ReadSetting("BaseDirectory");
        public static          string CharactersDirectory = AppSet.ReadSetting("CharactersDirectory");
        public static          string DialogsDirectory = AppSet.ReadSetting("DialogsDirectory");
        public static          string AudioDirectory = AppSet.ReadSetting("AudioDirectory");
        public static readonly string DecimalSerialLogFileName = AppSet.ReadSetting("DecimalSerialLogFileName");
        public static readonly string SerialLogFileName = AppSet.ReadSetting("SerialLogFileName");
        public static readonly string DialogSerialLogFileName = AppSet.ReadSetting("DialogSerialLogFileName");
        public static readonly string ComPortName = AppSet.ReadSetting("ComPortName");
    }

    public class PhraseEntry
    {
        public string DialogStr;
        public string FileName;
        public Dictionary<string, double> phraseWeights;    //to replace PhraseWeights, uses string tags.
        public string PhraseRating;
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

        public const int RecentPhrasesQueueSize = 4;
        public Queue<PhraseEntry> RecentPhrases = new Queue<PhraseEntry>();  //TODO make this a method that runs over the history
    }
    
    //[JsonObject(MemberSerialization.OptIn)]
    public class ModelDialogInput
    {
        public List<ModelDialog> inList;
    }

    public class ModelDialog
    {
        // a ModelDialog is a sequence of phrase types that represent an exchange between characters 
        // the model dialog will be filled with randomly selected character phrases of the appropriate phrase type
        [JsonProperty("DialogName")]
        public string Name;

        [JsonProperty("AddedOnDateTime")]
        public DateTime AddedOnDateTime = new DateTime(2016, 1, 2, 3, 4, 5);

        [JsonProperty("Popularity")]
        public double Popularity = 1.0;

        [JsonProperty("Adventure")]
        public string Adventure = "";

        [JsonProperty("Requires")]
        public List<string> Requires = new List<string>();

        [JsonProperty("Provides")]
        public List<string> Provides = new List<string>();

        [JsonProperty("PhraseTypeSequence")]
        public List<string> PhraseTypeSequence = new List<string>();
        public bool AreDialogsRequirementsMet() {return true;}
    }

    public static class RandomNumbers
    {
        public static Random Gen = new Random();
    }

    public class Program
    {
        public static DialogTracker TheDialogs = new DialogTracker();

        static void WriteStartupInfo() {
            string versionTimeStr = "Dialog Engine ver 0.80 " + DateTime.Now;
            Console.WriteLine(""); 
            Console.WriteLine(versionTimeStr);
            Console.WriteLine(SessionVars.exeDir);
            if (SessionVars.WriteSerialLog)
            {

                using (StreamWriter serialLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.SerialLogFileName), true))
                {
                    serialLog.WriteLine("");
                    serialLog.WriteLine("");
                    serialLog.WriteLine(versionTimeStr);
                    serialLog.WriteLine(SessionVars.exeDir);
                    serialLog.Close();
                }
                using (StreamWriter serialLogDec = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DecimalSerialLogFileName), true))
                {
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine(versionTimeStr);
                    serialLogDec.WriteLine(SessionVars.exeDir);
                    serialLogDec.Close();
                }
                using (StreamWriter serialLogDialog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogSerialLogFileName), true))
{
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine(versionTimeStr);
                    serialLogDialog.WriteLine(SessionVars.exeDir);
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
                        Console.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + ".mp3 " + phrase.DialogStr);//these are being checked with JSON input.
                    }
                }
                Console.WriteLine();
            }
            //TODO check that all dialog models have unique names
        }



        static void NormalizeDataPaths() {
            if (SessionVars.BaseDirectory == "") {
                SessionVars.BaseDirectory = SessionVars.exeDir;
            }
            SessionVars.LogsDirectory = SessionVars.BaseDirectory + SessionVars.LogsDirectory + @"\";
            SessionVars.CharactersDirectory = SessionVars.BaseDirectory + SessionVars.CharactersDirectory + @"\";
            SessionVars.DialogsDirectory = SessionVars.BaseDirectory + SessionVars.DialogsDirectory + @"\";
            SessionVars.AudioDirectory = SessionVars.BaseDirectory + SessionVars.AudioDirectory + @"\";
        }

        static void Main(string[] args) {
            Console.SetBufferSize(Console.BufferWidth, 32766);
            WriteStartupInfo();
            NormalizeDataPaths();
            SerialComs.InitSerial();
            InitModelDialogs.SetDefaults(TheDialogs);


            if (SessionVars.DebugFlag) {
                CheckForMissingPhrases();
                Console.WriteLine("   press enter to continue");
                Console.ReadLine();
            }

            //Select Debug Output
            if (SessionVars.ForceCharactersAndDialogModel)
            {
                Console.WriteLine("   enter three numbers to set the next: DialogModel, Char1, Char2");
                Console.WriteLine();
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
