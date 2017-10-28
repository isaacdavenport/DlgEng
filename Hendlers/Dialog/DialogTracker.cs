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

    public class DialogTracker
    {
        #region - Fields -

        #region - Private fields -

        private static DialogTracker msInstance = null;
        private static int msMovementWaitCount;

        private static readonly object mcPadlock = new object();

        private int mPriorCharacter1Num = 100;
        private int mPriorCharacter2Num = 100;
        private int mUrrentDialogModel = 1;
        private Random mRandom = new Random();
        private PrintMethod mAdddDialogItem;


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

        public delegate void PrintMethod(string _message);


        #endregion

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
                    if (msInstance == null)
                    {
                        msInstance = new DialogTracker();
                    }
                    return msInstance;
                }
            }
        }

        #endregion


        #region - Properties -

        /// <summary>
        /// Curent dialog model
        /// </summary>
        public int CurrentDialogModel { get; set; }


        public PrintMethod AddDialogItem
        {
            get
            {
                return new PrintMethod(((MainWindow) Application.Current.MainWindow).CurrentPrintMethod);
                
            }


            set
            {
                mAdddDialogItem = value;
            }
        }

        #endregion



        #region - Public methods -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_charFile"></param>
        /// <returns></returns>
        public Character ParseCharJson(FileInfo _charFile)
        {
            using (var _fi = File.OpenText(_charFile.FullName))
            {
                var _serializer = new JsonSerializer();
                var _charObj = (Character)_serializer.Deserialize(_fi, typeof(Character));

                return _charObj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dialogDirectives"></param>
        // TODO generate parallel dialogs, using string tags.
        public void GenerateADialog(params int[] _dialogDirectives)
        {
            if (!importClosestSerialComsCharacters())
                return;

            CurrentDialogModel = pickAWeightedDialog(Character1Num, Character2Num);

            if (waitingForMovement() || SameCharactersAsLast && SessionVars.WaitIndefinatelyForMove)
                return;

            processDebugFlags(_dialogDirectives);

            addDialogModelToHistory(CurrentDialogModel, Character1Num, Character2Num);

            var _speakingCharacter = Character1Num;


            var _selectedPhrase = CharacterList[_speakingCharacter].Phrases[0]; //initialize to unused placeholder phrase


            foreach (var _currentPhraseType in ModelDialogs[mUrrentDialogModel].PhraseTypeSequence)
            {
                if (CharacterList[_speakingCharacter].PhraseTotals.PhraseWeights.ContainsKey(_currentPhraseType))
                    if (SessionVars.TextDialogsOn)
                    {
                        //Here we try to change property of object which is created in main thread and we have to add code  to main thread's dispatcher object to do that

                        if (Application.Current.Dispatcher.CheckAccess())
                        {
                            AddDialogItem(CharacterList[_speakingCharacter].CharacterName);


                            if (CharacterList[_speakingCharacter].PhraseTotals.PhraseWeights[_currentPhraseType] < 0.01f)
                            {
                                AddDialogItem("   Missing PhraseType: " + _currentPhraseType);
                            }

                            _selectedPhrase = PickAWeightedPhrase(_speakingCharacter, _currentPhraseType);

                            if (SessionVars.TextDialogsOn)
                            {
                                AddDialogItem(_selectedPhrase.DialogStr);
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.BeginInvoke(() =>
                            {
                                AddDialogItem(CharacterList[_speakingCharacter].CharacterName);


                                if (CharacterList[_speakingCharacter].PhraseTotals.PhraseWeights[_currentPhraseType] <
                                    0.01f)
                                {
                                    AddDialogItem(" Missing PhraseType: " + _currentPhraseType + "\r\n");
                                }

                                _selectedPhrase = PickAWeightedPhrase(_speakingCharacter, _currentPhraseType);

                                if (SessionVars.TextDialogsOn)
                                {
                                    AddDialogItem(_selectedPhrase.DialogStr);
                                }
                            });
                        }


                        addPhraseToHistory(_selectedPhrase, _speakingCharacter);

                        var _pathAndFileName =  SessionVars.AudioDirectory 
                                                + CharacterList[_speakingCharacter].CharacterPrefix 
                                                + "_" + _selectedPhrase.FileName + ".mp3";

                        playAudio(_pathAndFileName); // vb: code stops here so commented out for debugging purpose

                        if (   !SessionVars.ForceCharactersAndDialogModel 
                            && !DialogTrackerAndSerialComsCharactersSame())
                        {
                            SameCharactersAsLast = false;

                            return; // the characters have moved  TODO break into charactersSame() and use also with prior
                        }
                        //Toggle character
                        if (_speakingCharacter == Character1Num) //toggle which character is speaking next
                            _speakingCharacter = Character2Num;
                        else
                            _speakingCharacter = Character1Num;
                    }

                HistoricalDialogs[HistoricalDialogs.Count - 1].Completed = true;

                if (HistoricalDialogs.Count > 2000)
                    HistoricalDialogs.RemoveRange(0, 100);


                if (HistoricalPhrases.Count > 8000)
                    HistoricalPhrases.RemoveRange(0, 100);

                RecentDialogs.Dequeue(); //move to use HistoricalDialogs
                RecentDialogs.Enqueue(CurrentDialogModel);
                LastPhraseImpliedMovement = determineIfMovementImplied(_selectedPhrase);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_character1Num"></param>
        /// <param name="_character2Num"></param>
        public void WriteDialogInfo(int _character1Num, int _character2Num)
        {
            var _dialogModelString = "\r\n  --DiMod " + CurrentDialogModel + " " 
                                     + ModelDialogs[CurrentDialogModel].Name 
                                     + " NextChars: " + CharacterList[_character1Num].CharacterPrefix + " " 
                                     + CharacterList[_character2Num].CharacterPrefix + " " + DateTime.Now;


            var _result = MessageBox.Show(_dialogModelString);

            if (SessionVars.WriteSerialLog)
            {
                using (var _serialLogDialogModels = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                {
                    _serialLogDialogModels.WriteLine(_dialogModelString);
                    _serialLogDialogModels.Close();
                }
            }

        }


        public bool DialogTrackerAndSerialComsCharactersSame()
        {
            if ((   Character1Num == SelectNextCharacters.NextCharacter1 
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
            var _d = new DirectoryInfo(SessionVars.CharactersDirectory);

            string _characterJsonMessage = "Character JSON in: " + SessionVars.CharactersDirectory;

            AddDialogItem(_characterJsonMessage);

            foreach (var _file in _d.GetFiles("*.json")) //file of type FileInfo for each .json in directory
            {
                string _beginReadMessage = " Begin read of " + _file.Name;

                AddDialogItem(_beginReadMessage);

                if (SessionVars.WriteSerialLog)
                {
                    using (var _jsonLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                    {
                        _jsonLog.WriteLine(" Begin read of " + _file.Name);
                    }
                }

                string _inChar;

                try
                {
                    var _fs = _file.OpenRead(); //open a read-only FileStream

                    using (var _reader = new StreamReader(_fs)
                    ) //creates new streamerader for fs stream. Could also construct with filename...
                    {
                        try
                        {
                            _inChar = _reader.ReadToEnd();

                            var _deserializedCharacterJson = JsonConvert.DeserializeObject<Models.Dialog.Character>(_inChar);

                            _deserializedCharacterJson.PhraseTotals = new PhraseEntry(); //init PhraseTotals

                            _deserializedCharacterJson.PhraseTotals.DialogStr = "phrase weights";

                            _deserializedCharacterJson.PhraseTotals.FileName = "silence";

                            _deserializedCharacterJson.PhraseTotals.PhraseRating = "G";

                            _deserializedCharacterJson.PhraseTotals.PhraseWeights = new Dictionary<string, double>();

                            _deserializedCharacterJson.PhraseTotals.PhraseWeights.Add("Greeting", 0.0f);


                            removePhrasesOverParentalRating(_deserializedCharacterJson);

                            //Calculate Phrase Weight Totals here.
                            foreach (var _curPhrase in _deserializedCharacterJson.Phrases)
                            {
                                foreach (var _tag in _curPhrase.PhraseWeights.Keys)
                                {
                                    if (_deserializedCharacterJson.PhraseTotals.PhraseWeights.Keys.Contains(_tag))
                                    {
                                        _deserializedCharacterJson.PhraseTotals.PhraseWeights[_tag] +=
                                            _curPhrase.PhraseWeights[_tag];
                                    }
                                    else
                                    {
                                        _deserializedCharacterJson.PhraseTotals.PhraseWeights.Add(_tag,
                                            _curPhrase.PhraseWeights[_tag]);
                                    }
                                }
                            }

                            for (var _i = 0; _i < Character.RecentPhrasesQueueSize; _i++)
                            {
                                // we always deque after enque so this sets que size
                                _deserializedCharacterJson.RecentPhrases.Enqueue(_deserializedCharacterJson.Phrases[0]);
                            }

                            //list Chars as they come in.

                            string _finishReadMessage = " Finish read of " + _deserializedCharacterJson.CharacterName;

                            AddDialogItem(_finishReadMessage);

                            if (SessionVars.WriteSerialLog)
                            {
                                using (var _jsonLog = new StreamWriter(SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                                {
                                    _jsonLog.WriteLine(" Finish read of " + _deserializedCharacterJson.CharacterName);
                                }
                            }

                            //Add to Char List
                            CharacterList.Add(_deserializedCharacterJson);
                        }
                        catch (JsonReaderException _e)
                        {
                            string _errorReadingMessage = "Error reading " + _file.Name;

                            AddDialogItem(_errorReadingMessage);


                            string _jsonParseErrorMessage = "JSON Parse error at " + _e.LineNumber + ", " + _e.LinePosition;

                            AddDialogItem(_jsonParseErrorMessage);
                        }
                    }
                }
                catch (UnauthorizedAccessException _e)
                {
                    AddDialogItem(_e.Message);

                    AddDialogItem("Unauthorized access exception while reading: " + _file.FullName);

                    AddDialogItem("Check the Character JSON path in your config file");
                }
                catch (DirectoryNotFoundException _e)
                {
                    AddDialogItem(_e.Message);

                    AddDialogItem("Directory not found exception while reading: " + _file.FullName);

                    AddDialogItem("check the Character JSON path in your config file");
                }
                catch (OutOfMemoryException _e)
                {
                    AddDialogItem(_e.Message);

                    AddDialogItem("You probably need to restart your computer...");
                }
            }


            if (CharacterList.Count < 2)
            {
                string _errorMessage = "  Insufficient readable character json files found in " 
                                       + SessionVars.CharactersDirectory + " .  Exiting.";

                AddDialogItem(_errorMessage);

                Environment.Exit(0);
            }

            // Fill the queue with greeting dialogs
            for (var _i = 0; _i < RecentDialogsQueSize; _i++)
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
            var _tempCh1 = Character1Num;
            Character1Num = Character2Num;
            Character2Num = _tempCh1;
            // it doesn't appear we should update prior characters 1 and 2 here
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_speakingCharacter"></param>
        /// <param name="_currentPhraseType"></param>
        /// <returns></returns>
        public PhraseEntry PickAWeightedPhrase(int _speakingCharacter, string _currentPhraseType)
        {
            var _selectedPhrase = CharacterList[_speakingCharacter].Phrases[0]; //initialize to unused phrase

            //Randomly select a phrase of correct Type
            var _phraseIsDuplicate = true;


            for (var _k = 0; _k < 6 && _phraseIsDuplicate; _k++) //do retries if selected phrase is recently used
            {
                _phraseIsDuplicate = false;

                var _phraseTableWeightedIndex = mRandom.NextDouble(); // rand 0.0 - 1.0

                _phraseTableWeightedIndex *= CharacterList[_speakingCharacter].PhraseTotals.PhraseWeights[_currentPhraseType];

                double _amountOfCurrentPhraseType = 0;



                foreach (var _currentPhraseTableEntry in CharacterList[_speakingCharacter].Phrases)
                {
                    if (_currentPhraseTableEntry.PhraseWeights.ContainsKey(_currentPhraseType))
                    {
                        _amountOfCurrentPhraseType += _currentPhraseTableEntry.PhraseWeights[_currentPhraseType];
                    }

                    if (_amountOfCurrentPhraseType > _phraseTableWeightedIndex)
                    {
                        _selectedPhrase = _currentPhraseTableEntry;
                        break; //inner foreach since we have the phrase we want
                    }
                }


                foreach (var _recentPhraseQueueEntry in CharacterList[_speakingCharacter].RecentPhrases)
                {
                    if (_recentPhraseQueueEntry == _selectedPhrase)
                    {
                        _phraseIsDuplicate = true; //send through retry loop k again
                        if (SessionVars.ShowDupePhrases)
                        {
                            if (Application.Current.Dispatcher.CheckAccess())
                            {
                                AddDialogItem("Duplicate [" + _selectedPhrase.DialogStr + "]");
                            }
                            else
                                Application.Current.Dispatcher.BeginInvoke(() =>
                                {
                                    AddDialogItem("Duplicate [" + _selectedPhrase.DialogStr + "]");
                                });
                        }

                        break; // doesn't matter if duplicated more than once
                    }
                }
            }

            //eventually overload enque to remove first to keep size same or create a replace
            CharacterList[_speakingCharacter].RecentPhrases.Dequeue();

            CharacterList[_speakingCharacter].RecentPhrases.Enqueue(_selectedPhrase);


            return _selectedPhrase;
        }

        #endregion

        #region - Private methods -


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_inCharacter"></param>
        private static void removePhrasesOverParentalRating(Character _inCharacter)
        {
            var _maxParentalRating = ParentalRatings.GetNumeric(SessionVars.CurrentParentalRating);
            var _minParentalRating = ParentalRatings.GetNumeric("G");


            _inCharacter.Phrases.RemoveAll( _item => 
                                               ParentalRatings.GetNumeric(_item.PhraseRating) > _maxParentalRating 
                                            || ParentalRatings.GetNumeric(_item.PhraseRating) < _minParentalRating);
        }


        private void playAudio(string _pathAndFileName)
        {
            if (!SessionVars.AudioDialogsOn)
            {
                Thread.Sleep(2200);
                return;
            }


            if (File.Exists(_pathAndFileName))
            {
                var _playSuccess = Audio.PlayMp3(_pathAndFileName);

                if (_playSuccess != 0)
                {
                    AddDialogItem("   MP3 Play Error  ---  " + _playSuccess);

                    AddDialogItem(string.Empty);
                    AddDialogItem(String.Empty);
                }


                var _i = 0;
                Thread.Sleep(600);


                while (Audio.IsPlaying() && _i < 250)
                {
                    // 20 seconds is max
                    Thread.Sleep(100);
                }

                Thread.Sleep(1600); // wait around a second after the audio is done for between phrase pause
            }
            else
            {
                AddDialogItem("Could not find: " + _pathAndFileName + Environment.NewLine);
            }
        }


        private void addDialogModelToHistory(int _dialogModelIndex, int _ch1, int _ch2)
        {
            HistoricalDialogs.Add(new HistoricalDialog
            {
                DialogIndex = _dialogModelIndex,
                DialogName = ModelDialogs[_dialogModelIndex].Name,
                StartedTime = DateTime.Now,
                Completed = false,
                Character1 = _ch1,
                Character2 = _ch2
            });
        }


        private List<int> findMostRecentAdventureDialogIndexes()
        {
            var _mostRecentAdventureDialogs = new List<int>();
            // most recent will be in the 0 index of list
            var _foundAdventures = new List<string>();
            var _j = 0;


            for (var _i = HistoricalDialogs.Count - 1; _i >= 0; _i--)
            {
                var _dialog = ModelDialogs[HistoricalDialogs[_i].DialogIndex];

                if (_dialog.Adventure.Length > 0 && !_foundAdventures.Contains(_dialog.Adventure))
                {
                    //if the dialog was part of an adventure and we haven't already found the most recent 
                    //from that adventure add the dialog to the most recent adventure list
                    _foundAdventures.Add(_dialog.Adventure);
                    _mostRecentAdventureDialogs.Add(HistoricalDialogs[_i].DialogIndex);
                }


                _j++;

                if (_j > 400) break; //don't go through all of time looking for active adventures
            }



            return _mostRecentAdventureDialogs;
        }



        private bool checkIfDialogModelUsedRecently(int _dialogModel)
        {
            foreach (var _recentDialogQueueEntry in RecentDialogs) // try again if dialog model recentlyused{
            {
                if (_recentDialogQueueEntry == _dialogModel)
                {
                    if (SessionVars.ShowDupePhrases)
                        Console.WriteLine("Duplicate Dialog [" + _dialogModel + "]");

                    return true;
                }
            }



            return false;
        }

        private bool checkIfCharactersHavePhrasesForDialog(int _dialogModel, int _character1Num, int _character2Num)
        {
            var _currentCharacter = _character1Num;

            foreach (var _element in ModelDialogs[_dialogModel].PhraseTypeSequence)
            {
                //try again if characters lack phrases for this model
                if (CharacterList[_currentCharacter].PhraseTotals.PhraseWeights.ContainsKey(_element))
                {
                    if (CharacterList[_currentCharacter].PhraseTotals.PhraseWeights[_element] < 0.015f)
                        return false;

                    if (_currentCharacter == _character1Num)
                        _currentCharacter = _character2Num;
                    else
                        _currentCharacter = _character1Num;


                }
                else
                {
                    return false;
                }
            }



            return true;
        }

        private bool checkIfDialogPreRequirementMet(int _dialogModel)
        {
            if (ModelDialogs[_dialogModel].Requires == null || ModelDialogs[_dialogModel].Requires.Count == 0)
                return true;


            if (!HistoricalDialogs.Any())
                return false;


            var _lastHistoricalDialog = HistoricalDialogs.Last();


            foreach (var _requiredTag in ModelDialogs[_dialogModel].Requires)
            {
                var _currentRequiredTagSatisfied = false;

                foreach (var _histDialog in HistoricalDialogs)
                {
                    // could speed by only going through unique historical dialog index #s
                    if (ModelDialogs[_histDialog.DialogIndex].Adventure == ModelDialogs[_dialogModel].Adventure)
                    {
                        foreach (var _providedTag in ModelDialogs[_histDialog.DialogIndex].Provides)
                        {
                            if (_providedTag == _requiredTag)
                            {
                                _currentRequiredTagSatisfied = true;
                                break;
                            }
                        }

                    }

                    if (_currentRequiredTagSatisfied)
                        break;

                    if (_histDialog == _lastHistoricalDialog)
                        return false;
                }
            }
            return true;
        }



        private int findNextAdventureDialogForCharacters(int _character1Num, int _character2Num, List<int> _mostRecentAdventureDialogIndexes)
        {
            var _ch1First = new bool();
            var _ch2First = new bool();

            //if we have recently done adventures give priority to adventure dialogs check them first
            foreach (var _recentAdventureIdx in _mostRecentAdventureDialogIndexes)
            {
                //given recent adventures
                foreach (var _possibleDialog in ModelDialogs) //TODO probably a cleaner way to do this with Linq and lamda expressions
                {
                    //look for follow on adventure possibilities
                    var _possibleDialogIdx = ModelDialogs.IndexOf(_possibleDialog);

                    if (ModelDialogs[_recentAdventureIdx].Adventure == _possibleDialog.Adventure)
                    {
                        foreach (var _providedStringKey in ModelDialogs[_recentAdventureIdx].Provides)
                        {
                            if (_possibleDialog.Requires.Contains(_providedStringKey))
                            {
                                //if a the most recent adventure dialog in the adventure provides what we require we won't 
                                //go backwards in adventures
                                _ch1First = checkIfCharactersHavePhrasesForDialog(_possibleDialogIdx, _character1Num, _character2Num);

                                _ch2First = checkIfCharactersHavePhrasesForDialog(_possibleDialogIdx, _character2Num, _character1Num);
                                if (_ch1First || _ch2First)
                                {
                                    if (_ch2First)
                                        SwapCharactersOneAndTwo();


                                    return _possibleDialogIdx;
                                }
                            }
                        }

                    }

                }
            }

            return -1; // code for no next adventure continuance found
        }


        private void addPhraseToHistory(PhraseEntry _selectedPhrase, int _speakingCharacter)
        {
            HistoricalPhrases.Add(new HistoricalPhrase
            {
                CharacterIndex = _speakingCharacter,
                CharacterPrefix = CharacterList[_speakingCharacter].CharacterPrefix,
                PhraseIndex = CharacterList[_speakingCharacter].Phrases.IndexOf(_selectedPhrase),
                PhraseFile = _selectedPhrase.FileName,
                StartedTime = DateTime.Now
            });

            if (SessionVars.WriteSerialLog)
                using (var _serialLogDialogLines = new StreamWriter(
                    SessionVars.LogsDirectory + SessionVars.DialogLogFileName, true))
                {
                    _serialLogDialogLines.WriteLine(CharacterList[_speakingCharacter].CharacterName + ": " +
                                                   _selectedPhrase.DialogStr);
                    _serialLogDialogLines.Close();
                }
        }


        private int pickAWeightedDialog(int _character1Num, int _character2Num)
        {
            //TODO check that all characters/phrasetypes required for adventure are included before starting adventure?
            var _dialogModel = 0;

            var _mostRecentAdventureDialogIndexes = findMostRecentAdventureDialogIndexes();

            // most recent will be in the 0 index of list which will be hit first in foreach
            if (_mostRecentAdventureDialogIndexes.Count > 0)
            {
                var _nextAdventureDialogIdx =
                    findNextAdventureDialogForCharacters(_character1Num, _character2Num,
                        _mostRecentAdventureDialogIndexes);


                if (_nextAdventureDialogIdx > 0 && _nextAdventureDialogIdx < ModelDialogs.Count)
                    return _nextAdventureDialogIdx; // we have an adventure dialog for these characters go with it
            }

            var _dialogWeightIndex = 0.0;
            var _attempts = 0;
            var _dialogModelFits = false;


            while (!_dialogModelFits && _attempts < 4000)
            {
                _attempts++;
                // exclude greetings at 0 and 1 TODO use .Greeting instead of hard coded const
                _dialogWeightIndex = mRandom.NextDouble();
                _dialogWeightIndex *= DialogModelPopularitySum - 0.4;
                _dialogWeightIndex += 0.4; // TODO better way to avoid greetings than by weight 0.2 each
                double _currentDialogWeightSum = 0;


                foreach (var _dialog in ModelDialogs)
                {
                    _currentDialogWeightSum += _dialog.Popularity;

                    if (_currentDialogWeightSum > _dialogWeightIndex)
                    {
                        _dialogModel = ModelDialogs.IndexOf(_dialog);
                        break;
                    }
                }
                var _dialogModelUsedRecently = checkIfDialogModelUsedRecently(_dialogModel);

                var _charactersHavePhrases = checkIfCharactersHavePhrasesForDialog(_dialogModel, Character1Num, Character2Num);

                var _dialogPreRequirementsMet = checkIfDialogPreRequirementMet(_dialogModel);

                var _greetingAppropriate = !(ModelDialogs[_dialogModel].PhraseTypeSequence[0] == "Greeting" && SameCharactersAsLast); // don't want a greeting with same characters as last

                if (   _dialogPreRequirementsMet && _charactersHavePhrases && _greetingAppropriate && !_dialogModelUsedRecently)
                    _dialogModelFits = true;

            }



            return _dialogModel;
        }


        private bool importClosestSerialComsCharacters()
        {
            var _tempChar1 = SelectNextCharacters.NextCharacter1;
            var _tempChar2 = SelectNextCharacters.NextCharacter2;

            if (_tempChar1 == _tempChar2 || _tempChar1 >= CharacterList.Count || _tempChar2 >= CharacterList.Count)
                return false;


            SameCharactersAsLast =   (_tempChar1 == mPriorCharacter1Num || _tempChar1 == mPriorCharacter2Num) 
                                  && (_tempChar2 == mPriorCharacter1Num || _tempChar2 == mPriorCharacter2Num);


            Character1Num = _tempChar1;
            Character2Num = _tempChar2;
            mPriorCharacter1Num = Character1Num;
            mPriorCharacter2Num = Character2Num;



            return true;
        }


        private void processDebugFlags(params int[] _dialogDirectives)
        {

            //if the array input is correct size and inputs don't exceed bounds set dialog parameters 
            if (_dialogDirectives.Count() == 3)
            {
                if (_dialogDirectives[0] < ModelDialogs.Count)
                    CurrentDialogModel = _dialogDirectives[0];


                if (_dialogDirectives[1] < CharacterList.Count)
                    Character1Num = _dialogDirectives[1];


                if (_dialogDirectives[2] < CharacterList.Count)
                    Character2Num = _dialogDirectives[2];
            }

            if (SessionVars.DebugFlag)
                WriteDialogInfo(Character1Num, Character2Num);


            if (SessionVars.HeatMapFullMatrixDispMode)
                FirmwareDebuggingTools.PrintHeatMap();


            if (SessionVars.HeatMapSumsMode)
                FirmwareDebuggingTools.PrintHeatMapSums();
        }




        private bool waitingForMovement()
        {
            if (LastPhraseImpliedMovement && SameCharactersAsLast && !SessionVars.NoSerialPort)
            {
                Thread.Sleep(mRandom.Next(0, 3000));
                msMovementWaitCount++;


                if (msMovementWaitCount == 3)
                {
                    var _ch1RetreatPhrase = PickAWeightedPhrase(Character1Num, "Retreat");


                    //if we can reach object created by main thread we don't need to queue our code to dispatcher object of main thread
                    if (Application.Current.Dispatcher.CheckAccess())
                    {
                    }
                    else
                    {
                        Application.Current.Dispatcher.BeginInvoke(() =>
                        {
                            AddDialogItem(CharacterList[Character1Num].CharacterName + " Wait3 : ");

                            AddDialogItem(_ch1RetreatPhrase.DialogStr);
                        });
                    }


                    playAudio(SessionVars.AudioDirectory + CharacterList[Character1Num].CharacterPrefix +
                              "_" + _ch1RetreatPhrase.FileName + ".mp3");
                    return true;
                }


                if (msMovementWaitCount == 7)
                {
                    LastPhraseImpliedMovement = false;

                    var _ch2RetreatPhrase = PickAWeightedPhrase(Character2Num, "Retreat");

                    Console.Write(CharacterList[Character2Num].CharacterName + " Wait5 : ");
                    Console.WriteLine(_ch2RetreatPhrase.DialogStr);


                    playAudio( SessionVars.AudioDirectory 
                               + CharacterList[Character2Num].CharacterPrefix 
                               + "_" + _ch2RetreatPhrase.FileName + ".mp3");


                    return true;
                }
                if (msMovementWaitCount > 11)
                {
                    msMovementWaitCount = 0;
                    LastPhraseImpliedMovement = false; //reset the wait for movement flag after waiting a long time
                }


                return true;
            }


            //reset the wait for movement flag when we are no longer same characters as last time
            LastPhraseImpliedMovement = false; 

            msMovementWaitCount = 0;



            return false;
        }



        private bool determineIfMovementImplied(PhraseEntry _selectedPhrase)
        {
            double _insultWeight, _retrWeight, _threatWeight, _shutUpWeight;

            _selectedPhrase.PhraseWeights.TryGetValue("Insult", out _insultWeight);

            _selectedPhrase.PhraseWeights.TryGetValue("Retreat", out _retrWeight);

            _selectedPhrase.PhraseWeights.TryGetValue("Threat", out _threatWeight);

            _selectedPhrase.PhraseWeights.TryGetValue("ShutUp", out _shutUpWeight);

            if (_insultWeight + _retrWeight + _threatWeight + _shutUpWeight > 0.1)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}