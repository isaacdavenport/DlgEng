//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using log4net;
using Newtonsoft.Json;
using DialogEngine.Models.Logger;
using System.Threading.Tasks;
using DialogEngine.ViewModels.Dialog;
using System.Collections.ObjectModel;


// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace DialogEngine
{

    public class DialogTracker
    {
        #region - Fields -

        #region - Private fields -
        private static readonly ILog mcLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static DialogTracker msInstance = null;
        private static int msMovementWaitCount;
        private static int msCurrentDialogModel;
        private static readonly object mcPadlock = new object();

        private DialogViewModel mDialogViewModel;
        private int mPriorCharacter1Num = 100;
        private int mPriorCharacter2Num = 100;
        private Random mRandom = new Random();
        private PrintMethod mAddItem;

        #endregion

        #region - Protected fields -

        protected const int RecentDialogsQueSize = 6;

        #endregion

        #region - Public fields -
    
        public int Character1Num = 0;
        public int Character2Num = 1;
        public double DialogModelPopularitySum;
        public bool LastPhraseImpliedMovement;
        public bool SameCharactersAsLast;

        public List<HistoricalDialog> HistoricalDialogs = new List<HistoricalDialog>();
        public List<HistoricalPhrase> HistoricalPhrases = new List<HistoricalPhrase>();
        public List<ModelDialog> ModelDialogs = new List<ModelDialog>();
        public Queue<int> RecentDialogs = new Queue<int>();

        public delegate void PrintMethod(Object  _message);

        #endregion

        #endregion

        #region - Constructor - 

        /// <summary>
        /// Creates instance of DialogTracker.cs
        /// </summary>
        /// <param name="_dialogViewModel"></param>
        public DialogTracker(DialogViewModel _dialogViewModel)
        {
            mDialogViewModel = _dialogViewModel;
        }

        #endregion

        #region - Singleton -

        /// <summary>
        /// Singleton for DialogTracker.cs
        /// </summary>
        /// <param name="_dialogViewModel"></param>
        /// <returns>Instance of DialogTracker.cs</returns>
        public static DialogTracker GetInstance(DialogViewModel _dialogViewModel)
        {

                lock (mcPadlock)
                {
                    if (msInstance == null)
                    {
                        msInstance = new DialogTracker(_dialogViewModel);
                    }
                    return msInstance;
                }
            
        }

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
                        throw new Exception("DialogTracker.cs is not created.");
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
        public int CurrentDialogModel
        {
            get { return msCurrentDialogModel; }

            set { msCurrentDialogModel = value; }
        }


        /// <summary>
        /// Delegate which is used to externally set log method to debug console
        /// </summary>
        public PrintMethod AddItem
        {
            get
            {
                return mAddItem;               
            }
            set
            {
                mAddItem = value;
            }
        }

        #endregion

        #region - Private methods -



        private static void _removePhrasesOverParentalRating(Character _inCharacter)
        {
            var _maxParentalRating = ParentalRatings.GetNumeric(SessionVariables.CurrentParentalRating);
            var _minParentalRating = ParentalRatings.GetNumeric("G");


            _inCharacter.Phrases.RemoveAll(_item =>
                                              ParentalRatings.GetNumeric(_item.PhraseRating) > _maxParentalRating
                                           || ParentalRatings.GetNumeric(_item.PhraseRating) < _minParentalRating);
        }


        private void _playAudio(string _pathAndFileName)
        {
            mcLogger.Debug("Start _playAudio method");


            if (!SessionVariables.AudioDialogsOn)
            {
                Task.Delay(3200);
                return;
            }


            if (File.Exists(_pathAndFileName))
            {
                var _playSuccess = MP3Player.Instance.PlayMp3(_pathAndFileName);

                if (_playSuccess != 0)
                {
                    AddItem(new ErrorMessage("MP3 Play Error  ---  " + _playSuccess));
                }


                var i = 0;
                Task.Delay(300);


                while (MP3Player.Instance.IsPlaying() && i < 250)
                {
                    // 20 seconds is max
                    Task.Delay(100);
                }

                Task.Delay(800); // wait around a second after the audio is done for between phrase pause
            }
            else
            {
                AddItem(new ErrorMessage("Could not find: " + _pathAndFileName));
            }

            mcLogger.Debug("End _playAudio method");

        }


        private void _addDialogModelToHistory(int _dialogModelIndex, int _ch1, int _ch2)
        {
            mcLogger.Debug("Start _addDialogModelToHistory method.");


            HistoricalDialogs.Add(new HistoricalDialog
            {
                DialogIndex = _dialogModelIndex,
                DialogName = ModelDialogs[_dialogModelIndex].Name,
                StartedTime = DateTime.Now,
                Completed = false,
                Character1 = _ch1,
                Character2 = _ch2
            });

            mcLogger.Debug("End _addDialogModelToHistory method.");

        }


        private List<int> _findMostRecentAdventureDialogIndexes()
        {
            var _mostRecentAdventureDialogs = new List<int>();
            // most recent will be in the 0 index of list
            var _foundAdventures = new List<string>();
            var j = 0;


            for (var i = HistoricalDialogs.Count - 1; i >= 0; i--)
            {
                var _dialog = ModelDialogs[HistoricalDialogs[i].DialogIndex];

                if (_dialog.Adventure.Length > 0 && !_foundAdventures.Contains(_dialog.Adventure))
                {
                    //if the dialog was part of an adventure and we haven't already found the most recent 
                    //from that adventure add the dialog to the most recent adventure list
                    _foundAdventures.Add(_dialog.Adventure);
                    _mostRecentAdventureDialogs.Add(HistoricalDialogs[i].DialogIndex);
                }


                j++;

                if (j > 400) break; //don't go through all of time looking for active adventures
            }



            return _mostRecentAdventureDialogs;
        }



        private bool checkIfDialogModelUsedRecently(int _dialogModel)
        {
            foreach (var _recentDialogQueueEntry in RecentDialogs) // try again if dialog model recentlyused{
            {
                if (_recentDialogQueueEntry == _dialogModel)
                    return true;
            }

            return false;
        }



        private bool checkIfCharactersHavePhrasesForDialog(int _dialogModel, int _character1Num, int _character2Num)
        {
            var _currentCharacter = _character1Num;

            foreach (var _element in ModelDialogs[_dialogModel].PhraseTypeSequence)
            {
                //try again if characters lack phrases for this model
                if (mDialogViewModel.CharacterCollection[_currentCharacter].PhraseTotals.PhraseWeights.ContainsKey(_element))
                {
                    if (mDialogViewModel.CharacterCollection[_currentCharacter].PhraseTotals.PhraseWeights[_element] < 0.015f)
                        return false;

                    if (_currentCharacter == _character1Num)
                        _currentCharacter = _character2Num;
                    else
                        _currentCharacter = _character1Num;

                }
                else
                    return false;
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



        private int _findNextAdventureDialogForCharacters(int _character1Num, int _character2Num, List<int> _mostRecentAdventureDialogIndexes)
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


        private void _addPhraseToHistory(PhraseEntry _selectedPhrase, int _speakingCharacter)
        {
            mcLogger.Debug("Start _addPhraseToHistory method");


            HistoricalPhrases.Add(new HistoricalPhrase
            {
                CharacterIndex = _speakingCharacter,
                CharacterPrefix = mDialogViewModel.CharacterCollection[_speakingCharacter].CharacterPrefix,
                PhraseIndex = mDialogViewModel.CharacterCollection[_speakingCharacter].Phrases.IndexOf(_selectedPhrase),
                PhraseFile = _selectedPhrase.FileName,
                StartedTime = DateTime.Now
            });

            LoggerHelper.Info(SessionVariables.DialogLogFileName, mDialogViewModel.CharacterCollection[_speakingCharacter].CharacterName + ": " + _selectedPhrase.DialogStr);

            mcLogger.Debug("End _addPhraseToHistory method");

        }



        private int _pickAWeightedDialog(int _character1Num, int _character2Num)
        {
            mcLogger.Debug("Start _pickAWeightedDialog method.");

            //TODO check that all characters/phrasetypes required for adventure are included before starting adventure?
            var _dialogModel = 0;

            var _mostRecentAdventureDialogIndexes = _findMostRecentAdventureDialogIndexes();

            // most recent will be in the 0 index of list which will be hit first in foreach
            if (_mostRecentAdventureDialogIndexes.Count > 0)
            {
                var _nextAdventureDialogIdx = _findNextAdventureDialogForCharacters(_character1Num, _character2Num, _mostRecentAdventureDialogIndexes);


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

                var _greetingAppropriate = !(ModelDialogs[_dialogModel].PhraseTypeSequence[0].Equals("Greeting") && SameCharactersAsLast); // don't want a greeting with same characters as last

                if (_dialogPreRequirementsMet && _charactersHavePhrases && _greetingAppropriate && !_dialogModelUsedRecently)
                    _dialogModelFits = true;

            }



            return _dialogModel;
        }


        private bool _importClosestSerialComsCharacters()
        {
            mcLogger.Debug("Start _importClosestSerialComsCharacters() method.");

            var _tempChar1 = SelectNextCharacters.NextCharacter1;
            var _tempChar2 = SelectNextCharacters.NextCharacter2;

            if (_tempChar1 == _tempChar2 || _tempChar1 >= mDialogViewModel.CharacterCollection.Count || _tempChar2 >= mDialogViewModel.CharacterCollection.Count)
                return false;


            SameCharactersAsLast = (_tempChar1 == mPriorCharacter1Num || _tempChar1 == mPriorCharacter2Num)
                                  && (_tempChar2 == mPriorCharacter1Num || _tempChar2 == mPriorCharacter2Num);


            Character1Num = _tempChar1;
            Character2Num = _tempChar2;
            mPriorCharacter1Num = Character1Num;
            mPriorCharacter2Num = Character2Num;

            mcLogger.Debug("End _importClosestSerialComsCharacters() method.");


            return true;
        }


        private void _processDebugFlags(params int[] _dialogDirectives)
        {
            mcLogger.Debug("Start _processDebugFlags method.");


            switch (_dialogDirectives.Count())
            {
                case 0:  // we didn't pass characters and dialog model

                    Character1Num = SelectNextCharacters.NextCharacter1;
                    Character2Num = SelectNextCharacters.NextCharacter2;

                    break;

                case 1:  // only dialog model selected

                    CurrentDialogModel = _dialogDirectives[0];

                    Character1Num = SelectNextCharacters.NextCharacter1;
                    Character2Num = SelectNextCharacters.NextCharacter2;

                    break;

                case 2: // only characters selected

                    Character1Num = _dialogDirectives[0];

                    Character2Num = _dialogDirectives[1];

                    break;

                case 3: // both characters and dialog model selected

                    Character1Num = _dialogDirectives[0];

                    Character2Num = _dialogDirectives[1];

                    CurrentDialogModel = _dialogDirectives[2];

                    break;

            }


            if (SessionVariables.DebugFlag)
                WriteDialogInfo(Character1Num, Character2Num);

            HeatMapUpdate.PrintHeatMap();

            mcLogger.Debug("End _processDebugFlags method.");


        }

        private bool _waitingForMovement()  // this method breaks that paradigm that we are always in a dialog model
        {
            // unless waiting to start a dialog model, that may be an issue eventually

            mcLogger.Debug("Start _waitingForMovement method.");

            if (LastPhraseImpliedMovement && SameCharactersAsLast && SessionVariables.UseSerialPort)
            {
                Task.Delay(mRandom.Next(0, 2000));
                msMovementWaitCount++;


                if (msMovementWaitCount == 3)
                {
                    var _ch1RetreatPhrase = PickAWeightedPhrase(Character1Num, "Retreat");

                    if(_ch1RetreatPhrase == null)
                    {
                        AddItem(new WarningMessage("Retreat phrase  was not found in " + mDialogViewModel.CharacterCollection[Character1Num].CharacterName) + ".");
                        return false;
                    }

                    AddItem(new InfoMessage("Wait3"));
                    AddItem(new DialogItem() { Character = mDialogViewModel.CharacterCollection[Character1Num], PhraseEntry = _ch1RetreatPhrase });
                    
                    _playAudio(SessionVariables.AudioDirectory + mDialogViewModel.CharacterCollection[Character1Num].CharacterPrefix +
                              "_" + _ch1RetreatPhrase.FileName + ".mp3");

                    return true;
                }


                if (msMovementWaitCount == 7)
                {
                    LastPhraseImpliedMovement = false;
                    var _ch2RetreatPhrase = PickAWeightedPhrase(Character2Num, "Retreat");

                    if (_ch2RetreatPhrase == null)
                    {
                        AddItem(new WarningMessage("Retreat phrase  was not found in " + mDialogViewModel.CharacterCollection[Character2Num].CharacterName) + ".");
                        return false;
                    }

                    AddItem(new InfoMessage("Wait5"));
                    AddItem(new DialogItem() { Character = mDialogViewModel.CharacterCollection[Character2Num], PhraseEntry = _ch2RetreatPhrase });
                    
                    _playAudio(SessionVariables.AudioDirectory
                               + mDialogViewModel.CharacterCollection[Character2Num].CharacterPrefix
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

            mcLogger.Debug("End _waitingForMovement method.");


            return false;
        }



        private bool _determineIfMovementImplied(PhraseEntry _selectedPhrase)
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

        #region - Public methods -


        /// <summary>
        /// Generates dialog 
        /// </summary>
        /// <param name="_dialogDirectives">If not empty, we passed characters and dialog model indexes</param>
        // TODO generate parallel dialogs, using string tags.
        public void GenerateADialog(CancellationToken _cancellationToken,params int[] _dialogDirectives)
        {

            mcLogger.Info("start GenerateADialog");
            //check is async method cancelled
            if (_cancellationToken.IsCancellationRequested)
                return;
            
            if (!_importClosestSerialComsCharacters())
                return;
            
            CurrentDialogModel = _pickAWeightedDialog(Character1Num, Character2Num);
            
            if (_waitingForMovement() || SameCharactersAsLast && SessionVariables.WaitIndefinatelyForMove)
                return;

            _processDebugFlags(_dialogDirectives);
            _addDialogModelToHistory(CurrentDialogModel, Character1Num, Character2Num);
            var _speakingCharacter = Character1Num;
            var _selectedPhrase = mDialogViewModel.CharacterCollection[_speakingCharacter].Phrases[0]; //initialize to unused placeholder phrase
            
            foreach (var _currentPhraseType in ModelDialogs[CurrentDialogModel].PhraseTypeSequence)
            {
                //check is async method cancelled
                if (_cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (mDialogViewModel.CharacterCollection[_speakingCharacter].PhraseTotals.PhraseWeights.ContainsKey(_currentPhraseType))
                    if (SessionVariables.TextDialogsOn)
                    {
                        AddItem(new InfoMessage(mDialogViewModel.CharacterCollection[_speakingCharacter].CharacterName + ": "));
                        
                        if (mDialogViewModel.CharacterCollection[_speakingCharacter].PhraseTotals.PhraseWeights[_currentPhraseType] < 0.01f)
                        {
                            AddItem(new WarningMessage("Missing PhraseType: " + _currentPhraseType));
                        }

                        _selectedPhrase = PickAWeightedPhrase(_speakingCharacter, _currentPhraseType);

                        if(_selectedPhrase == null)
                        {
                            AddItem(new WarningMessage("Phrase type " + _currentPhraseType + " was not found."));

                            continue;
                        }

                        AddItem(new InfoMessage(_selectedPhrase.DialogStr));
                        
                        AddItem(new DialogItem() { Character = mDialogViewModel.CharacterCollection[_speakingCharacter], PhraseEntry = _selectedPhrase  });


                        _addPhraseToHistory(_selectedPhrase, _speakingCharacter);

                        var _pathAndFileName =  SessionVariables.AudioDirectory 
                                                + mDialogViewModel.CharacterCollection[_speakingCharacter].CharacterPrefix 
                                                + "_" + _selectedPhrase.FileName + ".mp3";

                        _playAudio(_pathAndFileName); // vb: code stops here so commented out for debugging purpose

                        if ( !DialogTrackerAndSerialComsCharactersSame()
                            && DialogViewModel.SelectedCharactersOn != 1)
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
                LastPhraseImpliedMovement = _determineIfMovementImplied(_selectedPhrase);
            }

            mcLogger.Info("end GenerateADialog");

        }


        /// <summary>
        /// Writes dialog info
        /// </summary>
        /// <param name="_character1Num"></param>
        /// <param name="_character2Num"></param>
        public void WriteDialogInfo(int _character1Num, int _character2Num)
        {
            if (ModelDialogs.Count > 0)
            {

                var _dialogModelString = " --DiMod " + CurrentDialogModel + " "
                                         + ModelDialogs[CurrentDialogModel].Name
                                         + " NextChars: " + mDialogViewModel.CharacterCollection[_character1Num].CharacterPrefix + " "
                                         + mDialogViewModel.CharacterCollection[_character2Num].CharacterPrefix + " " + DateTime.Now;


                AddItem(new InfoMessage(_dialogModelString));

                //var _result = MessageBox.Show(_dialogModelString);

                LoggerHelper.Info(SessionVariables.DialogLogFileName, _dialogModelString);
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


        /// <summary>
        /// Load characters from .json files
        /// </summary>
        /// <returns>Task</returns>
        public async Task IntakeCharactersAsync()
        {
            ObservableCollection<Character> _characterList = new ObservableCollection<Character>();

            await Task.Run(() =>
            {
                Thread.CurrentThread.Name = "IntakeCharactersAsyncThread";

                var _d = new DirectoryInfo(SessionVariables.CharactersDirectory);

                string _characterJsonMessage = "Character JSON in: " + SessionVariables.CharactersDirectory;

                AddItem(new InfoMessage(_characterJsonMessage));



                foreach (var _file in _d.GetFiles("*.json")) //file of type FileInfo for each .json in directory
                {


                    string _beginReadMessage = "Begin read of " + _file.Name;

                    AddItem(new InfoMessage(_beginReadMessage));

                    LoggerHelper.Info(SessionVariables.DialogLogFileName, "Begin read of " + _file.Name);


                    string _inChar;

                    try
                    {
                        var _fs = _file.OpenRead(); //open a read-only FileStream

                        using (var _reader = new StreamReader(_fs)) //creates new streamerader for fs stream. Could also construct with filename...
                        {
                            try
                            {
                                _inChar = _reader.ReadToEnd();

                                var _deserializedCharacterJson = JsonConvert.DeserializeObject<Models.Dialog.Character>(_inChar);

                                //if there is wrong json file, just ignore it
                                if(_deserializedCharacterJson.CharacterName == null)
                                {
                                    continue;
                                }

                                _deserializedCharacterJson.PhraseTotals = new PhraseEntry(); //init PhraseTotals

                                _deserializedCharacterJson.PhraseTotals.DialogStr = "phrase weights";

                                _deserializedCharacterJson.PhraseTotals.FileName = "silence";

                                _deserializedCharacterJson.PhraseTotals.PhraseRating = "G";

                                _deserializedCharacterJson.PhraseTotals.PhraseWeights = new Dictionary<string, double>();

                                _deserializedCharacterJson.PhraseTotals.PhraseWeights.Add("Greeting", 0.0f);


                                _removePhrasesOverParentalRating(_deserializedCharacterJson);



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



                                for (var i = 0; i < Character.RecentPhrasesQueueSize; i++)
                                {
                                    // we always deque after enque so this sets que size
                                    _deserializedCharacterJson.RecentPhrases.Enqueue(_deserializedCharacterJson.Phrases[0]);
                                }



                                //list Chars as they come in.

                                string _finishReadMessage = "Finish read of " + _deserializedCharacterJson.CharacterName;


                                AddItem(new InfoMessage(_finishReadMessage));

                                LoggerHelper.Info(SessionVariables.DialogLogFileName, "Finish read of " + _deserializedCharacterJson.CharacterName);


                                //Add to Char List
                                _characterList.Add(_deserializedCharacterJson);


                            }
                            catch (JsonReaderException e)
                            {
                                string _errorReadingMessage = "Error reading " + _file.Name;

                                AddItem(new ErrorMessage(_errorReadingMessage));


                                string _jsonParseErrorMessage = "JSON Parse error at " + e.LineNumber + ", " + e.LinePosition;

                                AddItem(new ErrorMessage(_jsonParseErrorMessage));
                            }
                            catch (Exception ex)
                            {
                                mcLogger.Error("Error during parsing json file " + ex.Message);

                                AddItem(new ErrorMessage("Error during parsing json file."));
                            }
                        }
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        mcLogger.Error(e.Message);

                        AddItem(new ErrorMessage("Unauthorized access exception while reading: " + _file.FullName));

                    }
                    catch (DirectoryNotFoundException e)
                    {
                        mcLogger.Error(e.Message);

                        AddItem(new ErrorMessage("Directory not found exception while reading: " + _file.FullName));

                    }
                    catch (OutOfMemoryException e)
                    {
                        mcLogger.Error(e.Message);

                        AddItem(new ErrorMessage("You probably need to restart your computer..."));
                    }
                }


                // Fill the queue with greeting dialogs
                for (var _i = 0; _i < RecentDialogsQueSize; _i++)
                {
                    RecentDialogs.Enqueue(0); // Fill the que with greeting dialogs
                }

                mDialogViewModel.CharacterCollection = _characterList;


                if (mDialogViewModel.CharacterCollection.Count < 2)
                {
                    string _errorMessage = "Insufficient readable character json files found in "
                                           + SessionVariables.CharactersDirectory + " .  Exiting.";

                    AddItem(new ErrorMessage(_errorMessage));

                }


            });

        }


        public void SwapCharactersOneAndTwo()
        {
            var _tempCh1 = Character1Num;
            Character1Num = Character2Num;
            Character2Num = _tempCh1;
            // it doesn't appear we should update prior characters 1 and 2 here
        }


        public PhraseEntry PickAWeightedPhrase(int _speakingCharacter, string _currentPhraseType)
        {
            mcLogger.Debug("Start PickAWeightedPhrase method");

            PhraseEntry _selectedPhrase = null;

            try
            {
                _selectedPhrase = mDialogViewModel.CharacterCollection[_speakingCharacter].Phrases[0]; //initialize to unused phrase

                //Randomly select a phrase of correct Type
                var _phraseIsDuplicate = true;


                for (var k = 0; k < 6 && _phraseIsDuplicate; k++) //do retries if selected phrase is recently used
                {
                    _phraseIsDuplicate = false;

                    var _phraseTableWeightedIndex = mRandom.NextDouble(); // rand 0.0 - 1.0

                    _phraseTableWeightedIndex *= mDialogViewModel.CharacterCollection[_speakingCharacter].PhraseTotals.PhraseWeights[_currentPhraseType];

                    double _amountOfCurrentPhraseType = 0;



                    foreach (var _currentPhraseTableEntry in mDialogViewModel.CharacterCollection[_speakingCharacter].Phrases)
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


                    foreach (var _recentPhraseQueueEntry in mDialogViewModel.CharacterCollection[_speakingCharacter].RecentPhrases)
                    {
                        if (_recentPhraseQueueEntry.Equals(_selectedPhrase))
                        {
                            _phraseIsDuplicate = true; //send through retry loop k again
                            break; // doesn't matter if duplicated more than once
                        }
                    }
                }

                //eventually overload enque to remove first to keep size same or create a replace
                mDialogViewModel.CharacterCollection[_speakingCharacter].RecentPhrases.Dequeue();

                mDialogViewModel.CharacterCollection[_speakingCharacter].RecentPhrases.Enqueue(_selectedPhrase);

                mcLogger.Debug("End PickAWeightedPhrase method");
            }
            catch (Exception ex)
            {
                mcLogger.Error("PickAWeightedPhrase " + ex.Message);
            }




            return _selectedPhrase;
        }


        public void WriteStatusBarInfo(string _infoMessage, Brush _infoColor)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                try
                {
                    (Application.Current.MainWindow as MainWindow).WriteStatusInfo(_infoMessage, _infoColor);
                }
                catch (Exception e)
                {
                    mcLogger.Error(e.Message);
                }
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    try
                    {
                        (Application.Current.MainWindow as MainWindow).WriteStatusInfo(_infoMessage, _infoColor);
                    }
                    catch (Exception e)
                    {
                        mcLogger.Error(e.Message);
                    }
                }));
            }
        }

        #endregion


    }
}