//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Collections.Generic;
using System.Configuration;
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
            if (SessionVars.WriteSerialLog)
            {
                string versionTimeStr = "Dialog Engine ver 0.55 " + DateTime.Now;
                Console.WriteLine("");
                Console.WriteLine(versionTimeStr);
                Console.WriteLine("");

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
                        Console.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + ".mp3 " + phrase.DialogStr);//these are being checked with JSON input.
                    }
                }
                Console.WriteLine();
            }
            //TODO check that all dialog models have unique names
        }

        static void checkTagsUsed()
        {
            //spit out all dialog model names and associated number.
            Console.WriteLine("");
            Console.WriteLine(" Dialogs Index: ");
            Console.ReadLine();
            foreach(ModelDialog _dialog in TheDialogs.ModelDialogs)
            {
                Console.WriteLine(TheDialogs.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);
            }
            Console.ReadLine();

            //test that all character tags are used by a dialog model.
            Console.WriteLine(" check characters tags are used ");
            Console.ReadLine();
            Boolean usedFlag = false;
            foreach (Character _character in TheDialogs.CharacterList)
            {
                foreach (PhraseEntry _phrase in _character.Phrases)
                {
                    foreach (string _phrasetag in _phrase.phraseWeights.Keys)
                    {
                        usedFlag = false;
                        foreach (ModelDialog _dialog in TheDialogs.ModelDialogs)
                        {
                            foreach (string _dialogtag in _dialog.PhraseTypeSequence)
                            {
                                if (_phrasetag == _dialogtag)
                                {
                                    usedFlag = true;
                                    break;
                                }
                            }
                            if (usedFlag)
                            { break; }
                        }
                        if (!usedFlag)
                        {
                            Console.WriteLine(_phrasetag + " is not used.");
                        }
                    }
                }
            }
            Console.ReadLine();

            //test that all dialogs have character tags to use them
            //bad runtime, NxM for N and M phrases and dialogs worst case.
            Console.WriteLine(" check dialogs tags are used ");
            Console.ReadLine();
            foreach (ModelDialog _dialog in TheDialogs.ModelDialogs)
            {
                foreach (string _dialogtag in _dialog.PhraseTypeSequence)//each dialog model tag
                {
                    usedFlag = false;
                    foreach(Character _character in TheDialogs.CharacterList)
                    {
                        foreach(PhraseEntry _characterPhrase in _character.Phrases)
                        {
                            foreach(string _phraseTag in _characterPhrase.phraseWeights.Keys)//each character phrase tag
                            {
                                if (_dialogtag == _phraseTag)
                                {
                                    usedFlag = true;
                                    break;
                                }
                            }
                            if(usedFlag)
                            { break; }
                        }
                        if(usedFlag)
                        { break; }
                    }
                    if(!usedFlag)
                    { Console.WriteLine(_dialogtag + " not used in " + _dialog.Name); }
                }
            }
            Console.ReadLine();
        }

        static void Main(string[] args) {
            Console.SetBufferSize(Console.BufferWidth, 32766);
            WriteStartupInfo();
            SerialComs.InitSerial();
            InitModelDialogs.SetDefaults(TheDialogs);

            if(SessionVars.TagUsageCheck)
            {   checkTagsUsed();    }

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
