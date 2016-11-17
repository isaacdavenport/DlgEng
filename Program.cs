using System;
using System.Collections.Generic;
using System.IO;
using System.Threading; // for thread.sleep()

// for count on keyboard inputs to manually force which characters speak


namespace DialogEngine
{
    public enum PhraseTypes
    {
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
        SmCb_01A,
        SmCb_01B,
        SmCb_01C,
        SmCb_01D,
        SmCb_01E,
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
        public const bool DebugFlag = true;
        public const bool ForceCharacterSelection = true;
        public const bool ShowDupePhrases = false;
        public const bool HeatMapFullMatrixDispMode = false;
        public const bool HeatMapSumsMode = false;
        public const bool HeatMapOnlyMode = false;
        public const bool WriteSerialLog = false;
        public const bool NoSerialPort = true;
        public const bool CheckStuckTransmissions = false;
        public const bool MonitorReceiveBufferSize = false;
        public const bool MonitorMessageParseFails = false;
        public const ParentalRating CurrentParentalRating = ParentalRating.X;
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
        protected const int RecentPhrasesQueueSize = 4;
        public Queue<PhraseEntry> RecentPhrases = new Queue<PhraseEntry>();
    }

    public class ModelDialog
    {
        // a ModelDialog is a sequence of phrase types that represent an exchange between characters 
        // the model dialog will be filled with randomly selected character phrases of the appropriate phrase type
        public string Name;
        public DateTime AddedOnDateTime = new DateTime(2016, 1, 2, 3, 4, 5);
        public double Popularity = 1.0;
        public List<PhraseTypes> PhraseTypeSequence = new List<PhraseTypes>();
    }

    public static class RandomNumbers
    {
        public static Random Gen = new Random();
    }

    public class Program
    {
        public static DialogTracker theDialogs = new DialogTracker();

        static void Main(string[] args) {
            Console.SetBufferSize(Console.BufferWidth, 32766);
            string versionTimeStr = "Dialog Engine ver 0.30 Isaac, Aria " + DateTime.Now;
            Console.Write(versionTimeStr);
            if (SessionVars.WriteSerialLog) {
                using (StreamWriter serialLog = new StreamWriter(
                    @"Users/joey/Documents/Coding/Toys2Life/DialogEngine/CapturesAndAnalysis/SerialLog.txt", true)) {
                    serialLog.WriteLine("");
                    serialLog.WriteLine("");
                    serialLog.WriteLine(versionTimeStr);
                    serialLog.Close();
                }
                using (StreamWriter serialLogDec = new StreamWriter(
                    @"Users/joey/Documents/Coding/Toys2Life/DialogEngine/CapturesAndAnalysis/SerialLogDecimal.txt", true))
                {
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine(versionTimeStr);
                    serialLogDec.Close();
                }
                using (StreamWriter serialLogDialog = new StreamWriter(
                    @"Users/joey/Documents/Coding/Toys2Life/DialogEngine/CapturesAndAnalysis/SerialLogDialog.txt", true))
                {
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine(versionTimeStr);
                    serialLogDialog.Close();
                }
            }
            //SerialComs.InitSerial();
            InitModelDialogs.SetDefaults(theDialogs);

            //Select Debug Output
            if (SessionVars.ForceCharacterSelection) {
                Console.WriteLine("   enter three numbers to set the next: DialogModel, Char1, Char2");
                Console.WriteLine();
            }

            if (SessionVars.DebugFlag) {
                foreach (var character in theDialogs.CharacterList) {
                    foreach (PhraseEntry phrase in character.Phrases) {
                        if (!File.Exists(@"/Users/joey/Documents/Coding/Toys2Life/DialogAudio" + character.CharacterPrefix + "_" + phrase.FileName + ".mp3")) {
                            Console.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + " " + phrase.DialogStr);
                        }
                    }
                }
            }

            while (true) {
                if (SessionVars.ForceCharacterSelection) {
                    string[] keyboardInput = Console.ReadLine().Split(' ');

                    //if keyboard input has three numbers for debug mode to force dialog model and characters
                    if (keyboardInput.Length == 3) {
                        int j = 0;
                        int[] ModelAndCharacters = new int[3];
                        foreach (string AsciiInt in keyboardInput) {
                            ModelAndCharacters[j] = Int32.Parse(AsciiInt);
                            j++;
                        }
                        theDialogs.GenerateADialog(ModelAndCharacters);
                    }
                    else {
                        Console.WriteLine("Incorrect input, generating random dialog.");
                        theDialogs.GenerateADialog(); // wrong number of user input select rand dialog and characters
                    }
                }
                else {
                    if (!SessionVars.HeatMapOnlyMode) {
                        theDialogs.GenerateADialog();  //normal operation
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
