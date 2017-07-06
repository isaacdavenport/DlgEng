﻿//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading; // for thread.sleep()
using Newtonsoft.Json;
using System.Windows;

// TODO: JSON Input
//       build unit tests around json imports
//       Clean error recovery and handling when non-recoverable.
//       Vectorize RSSI to see movement over proximity. (increased prox most recently, center of mass.)
//          vector should be method calculated when needed to select new chars.
//          (who became closer most recently?)

//  before tue 5/16
    //unit tests on 3-4 easy methods
    //unit test on generateADialog in dialogtracker.cs

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
        
        //vb : variable to store input string
        public string keyboardInput;

        public static Application WinApp { get; private set; }
        public static Window MainWindow { get; private set; }
        public ShutdownMode ShutdownMode { get; set; }

        static void InitializeWindows()
        {
            WinApp = new Application();
            WinApp.ShutdownMode = ShutdownMode.OnLastWindowClose;
            WinApp.Run(MainWindow = new MainWindow()); // note: blocking call
            WinApp.Shutdown();
        }

        public static void WriteStartupInfo() { //vb: changed to public static void from static void
            if (SessionVars.WriteSerialLog)
            {
                string versionTimeStr = "Dialog Engine ver 0.67 " + DateTime.Now;
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine; // vb : is this a good practice
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += versionTimeStr + Environment.NewLine;
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine;
                //Console.WriteLine("");
                //Console.WriteLine(versionTimeStr);
                //Console.WriteLine("");
                

                using (StreamWriter serialLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.HexLogFileName), true))
                {
                    serialLog.WriteLine("");
                    serialLog.WriteLine("");
                    serialLog.WriteLine(versionTimeStr);
                    serialLog.Close();
                }
                using (StreamWriter serialLogDec = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DecimalLogFileName), true))
                {
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine(versionTimeStr);
                    serialLogDec.Close();
                }
                using (StreamWriter serialLogDialog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                {
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine(versionTimeStr);
                    serialLogDialog.Close();
                }
            }
        }

        public static void CheckForMissingPhrases() {
            if (!SessionVars.AudioDialogsOn) {
                return;
            }
            foreach (var character in TheDialogs.CharacterList)
            {
                foreach (PhraseEntry phrase in character.Phrases)
                {
                    if (!File.Exists(SessionVars.AudioDirectory + character.CharacterPrefix + "_" + phrase.FileName + ".mp3"))  //Char name and prefix are being left blank...
                    {
                        ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "missing " + character.CharacterPrefix + "_" + phrase.FileName + ".mp3 " + phrase.DialogStr + Environment.NewLine; // vb : += is this a good practice
                        //Console.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + ".mp3 " + phrase.DialogStr);
                        if (SessionVars.WriteSerialLog)
                        {
                            using (StreamWriter JSONLog = new StreamWriter(
                            (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                            {
                                JSONLog.WriteLine("missing " + character.CharacterPrefix + "_" + phrase.FileName + ".mp3 " + phrase.DialogStr);
                            }
                        }
                    }
                }
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += Environment.NewLine;
                //Console.WriteLine();
            }
            //TODO check that all dialog models have unique names
        }

        public static void checkTagsUsed() //changed from static void to public static void
        {
            //spit out all dialog model names and associated number.
            ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine; // vb : is this a good practice
            ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "Dialogs Index: " + Environment.NewLine;
            ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine;
            //Console.WriteLine("");
            //Console.WriteLine("Dialogs Index: ");
            //Console.ReadLine();
            foreach (ModelDialog _dialog in TheDialogs.ModelDialogs)
            {
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += " " + TheDialogs.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name + Environment.NewLine;
                //Console.WriteLine(" " + TheDialogs.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);

                if (SessionVars.WriteSerialLog)
                {
                    using (StreamWriter JSONLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                    {
                        JSONLog.WriteLine(" " + TheDialogs.ModelDialogs.IndexOf(_dialog) + " : " + _dialog.Name);
                    }
                }
            }
            //Console.ReadLine();

            //test that all character tags are used by a dialog model.
            ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "Check characters tags are used ";
            //Console.WriteLine("Check characters tags are used ");
            //Console.ReadLine();
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
                            ((MainWindow)Application.Current.MainWindow).TestOutput.Text += " " + _phrasetag + " is not used." + Environment.NewLine;
                            //Console.WriteLine(" " + _phrasetag + " is not used.");
                            if (SessionVars.WriteSerialLog)
                            {
                                using (StreamWriter JSONLog = new StreamWriter(
                                (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                                {
                                    JSONLog.WriteLine(" " + _phrasetag + " is not used.");
                                }
                            }
                        }
                    }
                }
            }
            //Console.ReadLine();

            //test that all dialogs have character tags to use them
            //bad runtime, NxM for N and M phrases and dialogs worst case.
            ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "Check dialogs tags are used " + Environment.NewLine;
            //Console.WriteLine("Check dialogs tags are used ");
            
            //Console.ReadLine();
            foreach (ModelDialog _dialog in TheDialogs.ModelDialogs)
            {
                foreach (string _dialogtag in _dialog.PhraseTypeSequence)//each dialog model tag
                {
                    usedFlag = false;
                    foreach (Character _character in TheDialogs.CharacterList)
                    {
                        foreach (PhraseEntry _characterPhrase in _character.Phrases)
                        {
                            foreach (string _phraseTag in _characterPhrase.phraseWeights.Keys)//each character phrase tag
                            {
                                if (_dialogtag == _phraseTag)
                                {
                                    usedFlag = true;
                                    break;
                                }
                            }
                            if (usedFlag)
                            { break; }
                        }
                        if (usedFlag)
                        { break; }
                    }
                    if (!usedFlag)
                    {
                        ((MainWindow)Application.Current.MainWindow).TestOutput.Text += " " + _dialogtag + " not used in " + _dialog.Name + Environment.NewLine;
                        //Console.WriteLine(" " + _dialogtag + " not used in " + _dialog.Name);
                        if (SessionVars.WriteSerialLog)
                        {
                            using (StreamWriter JSONLog = new StreamWriter(
                            (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                            {
                                JSONLog.WriteLine(" " + _dialogtag + " not used in " + _dialog.Name);
                            }
                        }
                    }
                }
            }
            //Console.ReadLine();
        }

        [STAThread] //vb : STAT thread that takes care of starting an application.exe with WPF window

        static void Main(string[] args) {


            //Console.SetBufferSize(Console.BufferWidth, 32766);
            //Console.WriteLine("Opening window...");


            InitializeWindows(); // vb : opens the WPF window and waits here


            //Console.WriteLine("Exiting main...");
            //WriteStartupInfo();
            //Console.WriteLine();
            //TheDialogs.intakeCharacters();
            //SerialComs.InitSerial();    //  vb : need to understand this implementation
            //InitModelDialogs.SetDefaults(TheDialogs);

            /*if(SessionVars.TagUsageCheck)
            {   checkTagsUsed();    }*/

            /*if (SessionVars.DebugFlag) {
                CheckForMissingPhrases();
                Console.WriteLine("  press enter to continue");
                Console.ReadLine();
                if(!SessionVars.ForceCharactersAndDialogModel)
                { Console.WriteLine("   you may enter two characters initials to make them talk"); }
            }*/
            
            //Select Debug Output
            /*if (SessionVars.ForceCharactersAndDialogModel)
            {
                Console.WriteLine("   enter three numbers to set the next: DialogModel, Char1, Char2");
                Console.WriteLine();
            }*/

            /*while (true) {
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
                            FirmwareDebuggingTools.PrintHeatMap();
                        }
                        if (SessionVars.HeatMapSumsMode) {
                            FirmwareDebuggingTools.PrintHeatMapSums();
                        }
                        Thread.Sleep(400);
                    }
                }
            }*/
        }
    }
}
