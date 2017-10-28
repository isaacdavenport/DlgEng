//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using Newtonsoft.Json;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace DialogEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogTracker
    {
        #region - Fields -

        #region - Private fields -

        private static DialogTracker mcInstance = null;
        private static int mcMovementWaitCount;

        private static readonly object mcPadlock = new object();

        private int mPriorCharacter1Num = 100;
        private int mPriorCharacter2Num = 100;
        private int mUrrentDialogModel = 1;
        private Random mRandom = new Random();

        #endregion

        #region - Protected fields -

        protected const int RecentDialogsQueSize = 6;

        #endregion


        #region - Public fields -

       
        public WindowsMediaPlayerMp3 Audio = new WindowsMediaPlayerMp3();

        public int Character1Num;
        public int Character2Num = 1;
        public double DialogModelPopularitySum;
        public bool LastPhraseImpliedMovement;
        public bool SameCharactersAsLast;

        public List<Character> CharacterList = new List<Character>();
        public List<HistoricalDialog> HistoricalDialogs = new List<HistoricalDialog>();
        public List<HistoricalPhrase> HistoricalPhrases = new List<HistoricalPhrase>();
        public List<ModelDialog> ModelDialogs = new List<ModelDialog>();
        public Queue<int> RecentDialogs = new Queue<int>();

        public delegate void PrintMethod(string message);

        public PrintMethod _adddDialogItem;

        #endregion

        #endregion

        #region -Constructor-

        public DialogTracker()
        {
        }

        #endregion


        #region - Singleton -

        /// <summary>
        /// Implementation of singleton pattern for DialogTracker class
        /// </summary>
        public static DialogTracker Instance
        {
            get
            {
                lock (mcPadlock)
                {
                    if (mcInstance == null)
                    {
                        mcInstance = new DialogTracker();
                    }
                    return mcInstance;
                }
            }
        }

        #endregion


        #region - Properties -

        public int CurrentDialogModel { get; set; }


        public PrintMethod AddDialogItem
        {
            get
            {
                return new PrintMethod(((MainWindow) Application.Current.MainWindow).CurrentPrintMethod);
                ;
            }


            set { _adddDialogItem = value; }
        }

        #endregion


        #region - Public methods -

        public Character ParseCharJSON(FileInfo CharFile)
        {
            using (var fi = File.OpenText(CharFile.FullName))
            {
                var serializer = new JsonSerializer();
                var CharObj = (Character) serializer.Deserialize(fi, typeof(Character));

                return CharObj;
            }
        }


        // TODO generate parallel dialogs, using string tags.
        public void GenerateADialog(params int[] dialogDirectives)
        {
            if (!ImportClosestSerialComsCharacters())
                return;

            CurrentDialogModel = PickAWeightedDialog(Character1Num, Character2Num);

            if (WaitingForMovement() || SameCharactersAsLast && SessionVars.WaitIndefinatelyForMove)
                return;

            ProcessDebugFlags(dialogDirectives);

            AddDialogModelToHistory(CurrentDialogModel, Character1Num, Character2Num);

            var speakingCharacter = Character1Num;


            var selectedPhrase = CharacterList[speakingCharacter].Phrases[0]; //initialize to unused placeholder phrase


            foreach (var currentPhraseType in ModelDialogs[mUrrentDialogModel].PhraseTypeSequence)
            {
                if (CharacterList[speakingCharacter].PhraseTotals.PhraseWeights.ContainsKey(currentPhraseType))
                    if (SessionVars.TextDialogsOn)
                    {
                        //Here we try to change property of object which is created in main thread and we have to add code  to main thread's dispatcher object to do that

                        if (Application.Current.Dispatcher.CheckAccess())
                        {
                            AddDialogItem(CharacterList[speakingCharacter].CharacterName);


                            if (CharacterList[speakingCharacter].PhraseTotals.PhraseWeights[currentPhraseType] < 0.01f)
                            {
                                AddDialogItem("   Missing PhraseType: " + currentPhraseType);
                            }

                            selectedPhrase = PickAWeightedPhrase(speakingCharacter, currentPhraseType);

                            if (SessionVars.TextDialogsOn)
                            {
                                AddDialogItem(selectedPhrase.DialogStr);
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.BeginInvoke(() =>
                            {
                                AddDialogItem(CharacterList[speakingCharacter].CharacterName);


                                if (CharacterList[speakingCharacter].PhraseTotals.PhraseWeights[currentPhraseType] <
                                    0.01f)
                                {
                                    AddDialogItem(" Missing PhraseType: " + currentPhraseType + "\r\n");
                                }

                                selectedPhrase = PickAWeightedPhrase(speakingCharacter, currentPhraseType);

                                if (SessionVars.TextDialogsOn)
                                {
                                    AddDialogItem(selectedPhrase.DialogStr);
                                }
                            });
                        }


                        AddPhraseToHistory(selectedPhrase, speakingCharacter);

                        var pathAndFileName =  SessionVars.AudioDirectory 
                                             + CharacterList[speakingCharacter].CharacterPrefix 
                                             + "_" + selectedPhrase.FileName + ".mp3";

                        PlayAudio(pathAndFileName); // vb: code stops here so commented out for debugging purpose

                        if (   !SessionVars.ForceCharactersAndDialogModel 
                            && !DialogTrackerAndSerialComsCharactersSame())
                        {
                            SameCharactersAsLast = false;

                            return; // the characters have moved  TODO break into charactersSame() and use also with prior
                        }
                        //Toggle character
                        if (speakingCharacter == Character1Num) //toggle which character is speaking next
                            speakingCharacter = Character2Num;
                        else
                            speakingCharacter = Character1Num;
                    }

                HistoricalDialogs[HistoricalDialogs.Count - 1].Completed = true;
                if (HistoricalDialogs.Count > 2000)
                    HistoricalDialogs.RemoveRange(0, 100);
                if (HistoricalPhrases.Count > 8000)
                    HistoricalPhrases.RemoveRange(0, 100);

                RecentDialogs.Dequeue(); //move to use HistoricalDialogs
                RecentDialogs.Enqueue(CurrentDialogModel);
                LastPhraseImpliedMovement = DetermineIfMovementImplied(selectedPhrase);
            }
        }


        public void WriteDialogInfo(int character1Num, int character2Num)
        {
            var dialogModelString = "\r\n  --DiMod " + CurrentDialogModel + " " +
                                    ModelDialogs[CurrentDialogModel].Name +
                                    " NextChars: " + CharacterList[character1Num].CharacterPrefix + " " +
                                    CharacterList[character2Num].CharacterPrefix + " " + DateTime.Now;

            //((MainWindow)Application.Current.MainWindow).TestOutput.Text += dialogModelString + Environment.NewLine;

            var result = MessageBox.Show(dialogModelString);

            //Console.WriteLine(dialogModelString);
            if (SessionVars.WriteSerialLog)
                using (var serialLogDialogModels = new StreamWriter(
                    SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                {
                    serialLogDialogModels.WriteLine(dialogModelString);
                    serialLogDialogModels.Close();
                }
        }


        public bool DialogTrackerAndSerialComsCharactersSame()
        {
            if ((  Character1Num == SelectNextCharacters.NextCharacter1 
                || Character1Num == SelectNextCharacters.NextCharacter2)
                && (Character2Num == SelectNextCharacters.NextCharacter2 
                || Character2Num == SelectNextCharacters.NextCharacter1))
            {
                return true;
            }
            return false;
        }

        public void IntakeCharacters()
        {
            var d = new DirectoryInfo(SessionVars.CharactersDirectory);

            string characterJsonMessage = "Character JSON in: " + SessionVars.CharactersDirectory;

            AddDialogItem(characterJsonMessage);

            foreach (var file in d.GetFiles("*.json")) //file of type FileInfo for each .json in directory
            {
                string beginReadMessage = " Begin read of " + file.Name;

                AddDialogItem(beginReadMessage);

                if (SessionVars.WriteSerialLog)
                {
                    using (var JSONLog =
                        new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                    {
                        JSONLog.WriteLine(" Begin read of " + file.Name);
                    }
                }

                string inChar;

                try
                {
                    var fs = file.OpenRead(); //open a read-only FileStream

                    using (var reader = new StreamReader(fs)
                    ) //creates new streamerader for fs stream. Could also construct with filename...
                    {
                        try
                        {
                            inChar = reader.ReadToEnd();

                            var deserializedCharacterJSON =
                                JsonConvert.DeserializeObject<Models.Dialog.Character>(inChar);

                            deserializedCharacterJSON.PhraseTotals = new PhraseEntry(); //init PhraseTotals

                            deserializedCharacterJSON.PhraseTotals.DialogStr = "phrase weights";

                            deserializedCharacterJSON.PhraseTotals.FileName = "silence";

                            deserializedCharacterJSON.PhraseTotals.PhraseRating = "G";

                            deserializedCharacterJSON.PhraseTotals.PhraseWeights = new Dictionary<string, double>();

                            deserializedCharacterJSON.PhraseTotals.PhraseWeights.Add("Greeting", 0.0f);


                            RemovePhrasesOverParentalRating(deserializedCharacterJSON);

                            //Calculate Phrase Weight Totals here.
                            foreach (var curPhrase in deserializedCharacterJSON.Phrases)
                            {
                                foreach (var tag in curPhrase.PhraseWeights.Keys)
                                {
                                    if (deserializedCharacterJSON.PhraseTotals.PhraseWeights.Keys.Contains(tag))
                                    {
                                        deserializedCharacterJSON.PhraseTotals.PhraseWeights[tag] +=
                                            curPhrase.PhraseWeights[tag];
                                    }
                                    else
                                    {
                                        deserializedCharacterJSON.PhraseTotals.PhraseWeights.Add(tag,
                                            curPhrase.PhraseWeights[tag]);
                                    }
                                }
                            }

                            for (var i = 0; i < Character.RecentPhrasesQueueSize; i++)
                            {
                                // we always deque after enque so this sets que size
                                deserializedCharacterJSON.RecentPhrases.Enqueue(deserializedCharacterJSON.Phrases[0]);
                            }

                            //list Chars as they come in.

                            string finishReadMessage = " Finish read of " + deserializedCharacterJSON.CharacterName;

                            AddDialogItem(finishReadMessage);

                            if (SessionVars.WriteSerialLog)
                            {
                                using (var JSONLog =
                                    new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                                {
                                    JSONLog.WriteLine(" Finish read of " + deserializedCharacterJSON.CharacterName);
                                }
                            }

                            //Add to Char List
                            CharacterList.Add(deserializedCharacterJSON);
                        }
                        catch (JsonReaderException e)
                        {
                            string errorReadingMessage = "Error reading " + file.Name;

                            AddDialogItem(errorReadingMessage);


                            string jsonParseErrorMessage =
                                "JSON Parse error at " + e.LineNumber + ", " + e.LinePosition;

                            AddDialogItem(jsonParseErrorMessage);
                        }
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    AddDialogItem(e.Message);

                    AddDialogItem("Unauthorized access exception while reading: " + file.FullName);

                    AddDialogItem("Check the Character JSON path in your config file");
                }
                catch (DirectoryNotFoundException e)
                {
                    AddDialogItem(e.Message);

                    AddDialogItem("Directory not found exception while reading: " + file.FullName);

                    AddDialogItem("check the Character JSON path in your config file");
                }
                catch (OutOfMemoryException e)
                {
                    AddDialogItem(e.Message);

                    AddDialogItem("You probably need to restart your computer...");
                }
            }


            if (CharacterList.Count < 2)
            {
                string errorMessage = "  Insufficient readable character json files found in " +
                                      SessionVars.CharactersDirectory + " .  Exiting.";

                AddDialogItem(errorMessage);

                Environment.Exit(0);
            }

            // Fill the queue with greeting dialogs
            for (var i = 0; i < RecentDialogsQueSize; i++)
            {
                RecentDialogs.Enqueue(0); // Fill the que with greeting dialogs
            }

            AddDialogItem(string.Empty);
        }


        //    if (CharacterList.Count < 2)
        //    {
        //        ((MainWindow) Application.Current.MainWindow).TestOutput.Text +=
        //            "  Insufficient readable character json files found in "
        //            + SessionVars.CharactersDirectory + " .  Exiting." + Environment.NewLine;
        //        //Console.WriteLine("  Insufficient readable character json files found in "
        //        //    + SessionVars.CharactersDirectory + " .  Exiting.");
        //        //Console.ReadLine();
        //        Environment.Exit(0);
        //    }

        //    // Fill the queue with greeting dialogs
        //    for (var i = 0; i < RecentDialogsQueSize; i++)
        //        RecentDialogs.Enqueue(0); // Fill the que with greeting dialogs

        //    ((MainWindow) Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine;
        //    //Console.WriteLine();    //for break beofer dialogs intake in console.
        //}


        public void SwapCharactersOneAndTwo()
        {
            var tempCh1 = Character1Num;
            Character1Num = Character2Num;
            Character2Num = tempCh1;
            // it doesn't appear we should update prior characters 1 and 2 here
        }


        public PhraseEntry PickAWeightedPhrase(int speakingCharacter, string currentPhraseType)
        {
            var selectedPhrase = CharacterList[speakingCharacter].Phrases[0]; //initialize to unused phrase
            //Randomly select a phrase of correct Type
            var phraseIsDuplicate = true;

            for (var k = 0; k < 6 && phraseIsDuplicate; k++) //do retries if selected phrase is recently used
            {
                phraseIsDuplicate = false;

                var phraseTableWeightedIndex = mRandom.NextDouble(); // rand 0.0 - 1.0

                phraseTableWeightedIndex *=
                    CharacterList[speakingCharacter].PhraseTotals.PhraseWeights[currentPhraseType];
                double amountOfCurrentPhraseType = 0;

                foreach (var currentPhraseTableEntry in CharacterList[speakingCharacter].Phrases)
                {
                    if (currentPhraseTableEntry.PhraseWeights.ContainsKey(currentPhraseType))
                        amountOfCurrentPhraseType += currentPhraseTableEntry.PhraseWeights[currentPhraseType];

                    if (amountOfCurrentPhraseType > phraseTableWeightedIndex)
                    {
                        selectedPhrase = currentPhraseTableEntry;
                        break; //inner foreach since we have the phrase we want
                    }
                }
                foreach (var recentPhraseQueueEntry in CharacterList[speakingCharacter].RecentPhrases)
                {
                    if (recentPhraseQueueEntry == selectedPhrase)
                    {
                        phraseIsDuplicate = true; //send through retry loop k again
                        if (SessionVars.ShowDupePhrases)
                            if (Application.Current.Dispatcher.CheckAccess())
                            {
                                AddDialogItem("Duplicate [" + selectedPhrase.DialogStr + "]");
                            }
                            else
                                Application.Current.Dispatcher.BeginInvoke(() =>
                                {
                                    AddDialogItem("Duplicate [" + selectedPhrase.DialogStr + "]");
                                });
                        break; // doesn't matter if duplicated more than once
                    }
                }
            }

            //eventually overload enque to remove first to keep size same or create a replace
            CharacterList[speakingCharacter].RecentPhrases.Dequeue();
            CharacterList[speakingCharacter].RecentPhrases.Enqueue(selectedPhrase);
            return selectedPhrase;
        }

        #endregion

        #region - Private methods -

        private static void RemovePhrasesOverParentalRating(Character inCharacter)
        {
            var maxParentalRating = ParentalRatings.GetNumeric(SessionVars.CurrentParentalRating);
            var minParentalRating = ParentalRatings.GetNumeric("G");
            inCharacter.Phrases.RemoveAll(item =>
                ParentalRatings.GetNumeric(item.PhraseRating) > maxParentalRating ||
                ParentalRatings.GetNumeric(item.PhraseRating) < minParentalRating);
        }


        private void PlayAudio(string pathAndFileName)
        {
            if (!SessionVars.AudioDialogsOn)
            {
                Thread.Sleep(2200);
                return;
            }
            if (File.Exists(pathAndFileName))
            {
                var playSuccess = Audio.PlayMp3(pathAndFileName);

                if (playSuccess != 0)
                {
                    AddDialogItem("   MP3 Play Error  ---  " + playSuccess);

                    AddDialogItem(string.Empty);
                    AddDialogItem(String.Empty);
                }


                var i = 0;
                Thread.Sleep(600);
                while (Audio.IsPlaying() && i < 250)
                {
                    // 20 seconds is max
                    Thread.Sleep(100);
                }

                Thread.Sleep(1600); // wait around a second after the audio is done for between phrase pause
            }
            else
            {
                AddDialogItem("Could not find: " + pathAndFileName + Environment.NewLine);
            }
        }


        private void AddDialogModelToHistory(int dialogModelIndex, int ch1, int ch2)
        {
            HistoricalDialogs.Add(new HistoricalDialog
            {
                DialogIndex = dialogModelIndex,
                DialogName = ModelDialogs[dialogModelIndex].Name,
                StartedTime = DateTime.Now,
                Completed = false,
                Character1 = ch1,
                Character2 = ch2
            });
        }


        private List<int> findMostRecentAdventureDialogIndexes()
        {
            var mostRecentAdventureDialogs = new List<int>();
            // most recent will be in the 0 index of list
            var foundAdventures = new List<string>();
            var j = 0;
            for (var i = HistoricalDialogs.Count - 1; i >= 0; i--)
            {
                var dialog = ModelDialogs[HistoricalDialogs[i].DialogIndex];
                if (dialog.Adventure.Length > 0 && !foundAdventures.Contains(dialog.Adventure))
                {
                    //if the dialog was part of an adventure and we haven't already found the most recent 
                    //from that adventure add the dialog to the most recent adventure list
                    foundAdventures.Add(dialog.Adventure);
                    mostRecentAdventureDialogs.Add(HistoricalDialogs[i].DialogIndex);
                }
                j++;
                if (j > 400) break; //don't go through all of time looking for active adventures
            }
            return mostRecentAdventureDialogs;
        }

        private bool CheckIfDialogModelUsedRecently(int dialogModel)
        {
            foreach (var recentDialogQueueEntry in RecentDialogs) // try again if dialog model recentlyused
                if (recentDialogQueueEntry == dialogModel)
                {
                    if (SessionVars.ShowDupePhrases)
                        Console.WriteLine("Duplicate Dialog [" + dialogModel + "]");
                    return true;
                }
            return false;
        }

        private bool CheckIfCharactersHavePhrasesForDialog(int dialogModel, int character1Num, int character2Num)
        {
            var currentCharacter = character1Num;
            foreach (var element in ModelDialogs[dialogModel].PhraseTypeSequence)
                //try again if characters lack phrases for this model
                if (CharacterList[currentCharacter].PhraseTotals.PhraseWeights.ContainsKey(element))
                {
                    if (CharacterList[currentCharacter].PhraseTotals.PhraseWeights[element] < 0.015f)
                        return false;
                    if (currentCharacter == character1Num)
                        currentCharacter = character2Num;
                    else
                        currentCharacter = character1Num;
                }
                else
                {
                    return false;
                }
            return true;
        }

        private bool CheckIfDialogPreRequirementMet(int dialogModel)
        {
            if (ModelDialogs[dialogModel].Requires == null || ModelDialogs[dialogModel].Requires.Count == 0)
                return true;
            if (!HistoricalDialogs.Any())
                return false;
            var lastHistoricalDialog = HistoricalDialogs.Last();
            foreach (var requiredTag in ModelDialogs[dialogModel].Requires)
            {
                var currentRequiredTagSatisfied = false;
                foreach (var histDialog in HistoricalDialogs)
                {
                    // could speed by only going through unique historical dialog index #s
                    if (ModelDialogs[histDialog.DialogIndex].Adventure == ModelDialogs[dialogModel].Adventure)
                        foreach (var providedTag in ModelDialogs[histDialog.DialogIndex].Provides)
                            if (providedTag == requiredTag)
                            {
                                currentRequiredTagSatisfied = true;
                                break;
                            }
                    if (currentRequiredTagSatisfied)
                        break;
                    if (histDialog == lastHistoricalDialog)
                        return false;
                }
            }
            return true;
        }

        private int FindNextAdventureDialogForCharacters(int character1Num, int character2Num,
            List<int> mostRecentAdventureDialogIndexes)
        {
            var ch1First = new bool();
            var ch2First = new bool();

            //if we have recently done adventures give priority to adventure dialogs check them first
            foreach (var recentAdventureIdx in mostRecentAdventureDialogIndexes)
                //given recent adventures
            foreach (var possibleDialog in ModelDialogs
            ) //TODO probably a cleaner way to do this with Linq and lamda expressions
            {
                //look for follow on adventure possibilities
                var possibleDialogIdx = ModelDialogs.IndexOf(possibleDialog);
                if (ModelDialogs[recentAdventureIdx].Adventure == possibleDialog.Adventure)
                    foreach (var providedStringKey in ModelDialogs[recentAdventureIdx].Provides)
                        if (possibleDialog.Requires.Contains(providedStringKey))
                        {
                            //if a the most recent adventure dialog in the adventure provides what we require we won't 
                            //go backwards in adventures
                            ch1First = CheckIfCharactersHavePhrasesForDialog(possibleDialogIdx,
                                character1Num, character2Num);
                            ch2First = CheckIfCharactersHavePhrasesForDialog(possibleDialogIdx,
                                character2Num, character1Num);
                            if (ch1First || ch2First)
                            {
                                if (ch2First)
                                    SwapCharactersOneAndTwo();
                                return possibleDialogIdx;
                            }
                        }
            }
            return -1; // code for no next adventure continuance found
        }


        private void AddPhraseToHistory(PhraseEntry selectedPhrase, int speakingCharacter)
        {
            HistoricalPhrases.Add(new HistoricalPhrase
            {
                CharacterIndex = speakingCharacter,
                CharacterPrefix = CharacterList[speakingCharacter].CharacterPrefix,
                PhraseIndex = CharacterList[speakingCharacter].Phrases.IndexOf(selectedPhrase),
                PhraseFile = selectedPhrase.FileName,
                StartedTime = DateTime.Now
            });

            if (SessionVars.WriteSerialLog)
                using (var serialLogDialogLines = new StreamWriter(
                    SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                {
                    serialLogDialogLines.WriteLine(CharacterList[speakingCharacter].CharacterName + ": " +
                                                   selectedPhrase.DialogStr);
                    serialLogDialogLines.Close();
                }
        }


        private int PickAWeightedDialog(int character1Num, int character2Num)
        {
            //TODO check that all characters/phrasetypes required for adventure are included before starting adventure?
            var dialogModel = 0;

            var mostRecentAdventureDialogIndexes = findMostRecentAdventureDialogIndexes();

            // most recent will be in the 0 index of list which will be hit first in foreach
            if (mostRecentAdventureDialogIndexes.Count > 0)
            {
                var nextAdventureDialogIdx =
                    FindNextAdventureDialogForCharacters(character1Num, character2Num,
                        mostRecentAdventureDialogIndexes);

                if (nextAdventureDialogIdx > 0 && nextAdventureDialogIdx < ModelDialogs.Count)
                    return nextAdventureDialogIdx; // we have an adventure dialog for these characters go with it
            }

            var dialogWeightIndex = 0.0;
            var attempts = 0;
            var dialogModelFits = false;


            while (!dialogModelFits && attempts < 4000)
            {
                attempts++;
                // exclude greetings at 0 and 1 TODO use .Greeting instead of hard coded const
                dialogWeightIndex = mRandom.NextDouble();
                dialogWeightIndex *= DialogModelPopularitySum - 0.4;
                dialogWeightIndex += 0.4; // TODO better way to avoid greetings than by weight 0.2 each
                double currentDialogWeightSum = 0;


                foreach (var dialog in ModelDialogs)
                {
                    currentDialogWeightSum += dialog.Popularity;
                    if (currentDialogWeightSum > dialogWeightIndex)
                    {
                        dialogModel = ModelDialogs.IndexOf(dialog);
                        break;
                    }
                }
                var dialogModelUsedRecently = CheckIfDialogModelUsedRecently(dialogModel);

                var charactersHavePhrases =
                    CheckIfCharactersHavePhrasesForDialog(dialogModel, Character1Num, Character2Num);

                var dialogPreRequirementsMet = CheckIfDialogPreRequirementMet(dialogModel);

                var greetingAppropriate = !(ModelDialogs[dialogModel].PhraseTypeSequence[0] == "Greeting"
                                            && SameCharactersAsLast
                ); // don't want a greeting with same characters as last

                if (dialogPreRequirementsMet && charactersHavePhrases
                    && greetingAppropriate && !dialogModelUsedRecently)
                    dialogModelFits = true;
            }
            return dialogModel;
        }


        private bool ImportClosestSerialComsCharacters()
        {
            var tempChar1 = SelectNextCharacters.NextCharacter1;
            var tempChar2 = SelectNextCharacters.NextCharacter2;
            if (tempChar1 == tempChar2 || tempChar1 >= CharacterList.Count || tempChar2 >= CharacterList.Count)
                return false;
            SameCharactersAsLast =
                (tempChar1 == mPriorCharacter1Num || tempChar1 == mPriorCharacter2Num) &&
                (tempChar2 == mPriorCharacter1Num || tempChar2 == mPriorCharacter2Num);

            Character1Num = tempChar1;
            Character2Num = tempChar2;
            mPriorCharacter1Num = Character1Num;
            mPriorCharacter2Num = Character2Num;
            return true;
        }


        private void ProcessDebugFlags(params int[] dialogDirectives)
        {
            if (dialogDirectives.Count() == 3
            ) //if the array input is correct size and inputs don't exceed bounds set dialog parameters 
            {
                if (dialogDirectives[0] < ModelDialogs.Count)
                    CurrentDialogModel = dialogDirectives[0];
                if (dialogDirectives[1] < CharacterList.Count)
                    Character1Num = dialogDirectives[1];
                if (dialogDirectives[2] < CharacterList.Count)
                    Character2Num = dialogDirectives[2];
            }

            if (SessionVars.DebugFlag)
                WriteDialogInfo(Character1Num, Character2Num);
            if (SessionVars.HeatMapFullMatrixDispMode)
                FirmwareDebuggingTools.PrintHeatMap();
            if (SessionVars.HeatMapSumsMode)
                FirmwareDebuggingTools.PrintHeatMapSums();
        }

        private bool WaitingForMovement()
        {
            if (LastPhraseImpliedMovement && SameCharactersAsLast && !SessionVars.NoSerialPort)
            {
                Thread.Sleep(mRandom.Next(0, 3000));
                mcMovementWaitCount++;
                if (mcMovementWaitCount == 3)
                {
                    var ch1RetreatPhrase = PickAWeightedPhrase(Character1Num, "Retreat");


                    //if we can reach object created by main thread we don't need to queue our code to dispatcher object of main thread
                    if (Application.Current.Dispatcher.CheckAccess())
                    {
                    }
                    else
                    {
                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            AddDialogItem(CharacterList[Character1Num].CharacterName + " Wait3 : ");

                            AddDialogItem(ch1RetreatPhrase.DialogStr);
                        });
                    }


                    //Console.Write(CharacterList[Character1Num].CharacterName + " Wait3 : ");
                    //Console.WriteLine(ch1RetreatPhrase.DialogStr);
                    PlayAudio(SessionVars.AudioDirectory + CharacterList[Character1Num].CharacterPrefix +
                              "_" + ch1RetreatPhrase.FileName + ".mp3");
                    return true;
                }
                if (mcMovementWaitCount == 7)
                {
                    LastPhraseImpliedMovement = false;
                    var ch2RetreatPhrase = PickAWeightedPhrase(Character2Num, "Retreat");
                    Console.Write(CharacterList[Character2Num].CharacterName + " Wait5 : ");
                    Console.WriteLine(ch2RetreatPhrase.DialogStr);
                    PlayAudio(SessionVars.AudioDirectory + CharacterList[Character2Num].CharacterPrefix +
                              "_" + ch2RetreatPhrase.FileName + ".mp3");
                    return true;
                }
                if (mcMovementWaitCount > 11)
                {
                    mcMovementWaitCount = 0;
                    LastPhraseImpliedMovement = false; //reset the wait for movement flag after waiting a long time
                }
                return true;
            }
            LastPhraseImpliedMovement =
                false; //reset the wait for movement flag when we are no longer same characters as last time
            mcMovementWaitCount = 0;
            return false;
        }

        private bool DetermineIfMovementImplied(PhraseEntry selectedPhrase)
        {
            double insultWeight, retrWeight, threatWeight, shutUpWeight;

            selectedPhrase.PhraseWeights.TryGetValue("Insult", out insultWeight);

            selectedPhrase.PhraseWeights.TryGetValue("Retreat", out retrWeight);

            selectedPhrase.PhraseWeights.TryGetValue("Threat", out threatWeight);

            selectedPhrase.PhraseWeights.TryGetValue("ShutUp", out shutUpWeight);

            if (insultWeight + retrWeight + threatWeight + shutUpWeight > 0.1)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}