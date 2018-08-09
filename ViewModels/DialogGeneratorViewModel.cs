
using DialogEngine.Core;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Events.EventArgs;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Logger;
using DialogEngine.Models.Shared;
using DialogEngine.Services;
using DialogEngine.Workflows.DialogGeneratorWorkflow;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DialogEngine.Controls.ViewModels
{
    public class DialogGeneratorViewModel : ViewModelBase
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const int RecentDialogsQueSize = 6;
        private int mPriorCharacter1Num = 100;
        private int mPriorCharacter2Num = 100;
        private int mCharacter1Num = 0;
        private int mCharacter2Num = 1;
        private int mIndexOfCurrentDialogModel;
        private double mDialogModelPopularitySum;
        private bool mSameCharactersAsLast;
        private bool mIsItemAdded;
        private bool mIsForcedDialogModel;
        private delegate void PrintMethod(LogMessage message);
        private PrintMethod AddItem = DialogDataHelper.AddMessage;
        private StateMachine mStateMachine;
        private States mCurrentState;
        private Random mRandom = new Random();
        private SelectedCharactersPairEventArgs mRandomSelectionDataCached;
        private static EventWaitHandle mEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        private CancellationTokenSource mCancellationTokenSource;
        private CancellationTokenSource mStateMachineTaskTokenSource = new CancellationTokenSource();
        private ObservableCollection<Character> mCharactersList;
        private ObservableCollection<ModelDialog> mDialogModelsList = new ObservableCollection<ModelDialog>();
        private ObservableCollection<object> mDialogLinesCollection = new ObservableCollection<object>();
        private List<HistoricalDialog> mHistoricalDialogs = new List<HistoricalDialog>();
        private List<HistoricalPhrase> mHistoricalPhrases = new List<HistoricalPhrase>();
        private Queue<int> mRecentDialogs = new Queue<int>();

        #endregion

        #region - constructor -

        public DialogGeneratorViewModel()
        {
            StateMachine = new StateMachine
                (
                  action : () => { }
                );

            _configureStateMachine();
            _subscribeForEvents();
        }

        #endregion

        #region - event handlers -

        private void _stateMachine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("State"))
            {
                CurrentState = StateMachine.State;
            }
        }

        private void _dialogLinesCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // notify items control for new item is added, so its scroller can be scrolled to bottom
            IsItemAdded = true;
            IsItemAdded = false;
        }


        private void _charactersList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                Character _newCharacter = e.NewItems[0] as Character;

                _initializeCharacter(_newCharacter);
            }
            else if (e.OldItems != null)
            {
                //TODO
            }
        }

        #endregion

        #region - private functions -

        #region - handler functions -

        private void _configureStateMachine()
        {
            StateMachine.Configure(States.Start)
                .Permit(Triggers.Initialize,States.Init);

            StateMachine.Configure(States.Init)
                .OnEntry(t => _initialize())
                .Permit(Triggers.WaitForNewCharacters, States.Idle);

            StateMachine.Configure(States.Idle)
                .Permit(Triggers.PrepareDialogParameters, States.PreparingDialogParameters)
                .Permit(Triggers.FinishDialog, States.DialogFinished);

            StateMachine.Configure(States.GenerateADialog)
                .Permit(Triggers.WaitForNewCharacters, States.Idle)
                .Permit(Triggers.FinishDialog,States.DialogFinished);

            StateMachine.Configure(States.PreparingDialogParameters)
                .SubstateOf(States.GenerateADialog)
                .Permit(Triggers.WaitForNewCharacters,States.Idle)
                .Permit(Triggers.StartDialog, States.DialogStarted)
                .Permit(Triggers.FinishDialog, States.DialogFinished);

            StateMachine.Configure(States.DialogStarted)
                .SubstateOf(States.GenerateADialog)
                .Permit(Triggers.WaitForNewCharacters,States.Idle)
                .Permit(Triggers.FinishDialog, States.DialogFinished);

            StateMachine.Configure(States.DialogFinished)
                .Permit(Triggers.WaitForNewCharacters, States.Idle);
        }


        private void _initialize()
        {
            mCharactersList = DialogData.Instance.CharacterCollection;

            mCharactersList.CollectionChanged += _charactersList_CollectionChanged;
            
            foreach(ModelDialogInfo _modelDialogInfo in DialogData.Instance.DialogModelCollection)
            {
                mDialogModelPopularitySum += _modelDialogInfo.ArrayOfDialogModels.Sum(_modelDialogItem => _modelDialogItem.Popularity);

                foreach(ModelDialog _dialogModel in _modelDialogInfo.ArrayOfDialogModels)
                {
                    mDialogModelsList.Add(_dialogModel);
                }
            }

            foreach(Character character in mCharactersList)
            {
                _initializeCharacter(character);
            }

            // Fill the queue with greeting dialogs
            for (var _i = 0; _i < RecentDialogsQueSize; _i++)
            {
                mRecentDialogs.Enqueue(0); // Fill the que with greeting dialogs
            }
        }


        private void _initializeCharacter(Character character)
        {
            if (string.IsNullOrEmpty(character.CharacterName)) // if character is not loaded correctly we will skip character
                return;

            character.PhraseTotals = new PhraseEntry();  // init PhraseTotals
            character.PhraseTotals.DialogStr = "phrase weights";
            character.PhraseTotals.FileName = "silence";
            character.PhraseTotals.PhraseRating = "G";
            character.PhraseTotals.PhraseWeights = new Dictionary<string, double>();
            character.PhraseTotals.PhraseWeights.Add("Greeting", 0.0f);

            _removePhrasesOverParentalRating(character);

            //Calculate Phrase Weight Totals here.
            foreach (var _curPhrase in character.Phrases)
            {
                foreach (var tag in _curPhrase.PhraseWeights.Keys)
                {
                    if (character.PhraseTotals.PhraseWeights.Keys.Contains(tag))
                    {
                        character.PhraseTotals.PhraseWeights[tag] += _curPhrase.PhraseWeights[tag];
                    }
                    else
                    {
                        character.PhraseTotals.PhraseWeights.Add(tag, _curPhrase.PhraseWeights[tag]);
                    }
                }
            }

            for (var i = 0; i < Character.RecentPhrasesQueueSize && i < character.Phrases.Count; i++)
            {
                // we always deque after enque so this sets que size
                character.RecentPhrases.Enqueue(character.Phrases[0]);
            }
        }



        private void _subscribeForEvents()
        {
            EventAggregator.Instance.GetEvent<DialogDataLoadedEvent>().Subscribe(_onDialogDataLoaded);
            EventAggregator.Instance.GetEvent<DialogModelChangedEvent>().Subscribe(_onDialogModelChanged);
            EventAggregator.Instance.GetEvent<SelectedCharactersPairChangedEvent>().Subscribe(_onSelectedCharactersPairChanged);
            EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Subscribe(_onChangedCharacterState);

            StateMachine.PropertyChanged += _stateMachine_PropertyChanged;
            DialogLinesCollection.CollectionChanged += _dialogLinesCollection_CollectionChanged;
        }

        private void _onChangedCharacterState()
        {
            if(CurrentState != States.Idle)
            {
                mRandomSelectionDataCached = null;
                mStateMachineTaskTokenSource.Cancel();
            }                            
        }

        private void _onDialogModelChanged(SelectionChangedEventArgs args)
        {
          if(args.IsSelected)
            {
                mIndexOfCurrentDialogModel = args.Index;
                mIsForcedDialogModel = true;
            }
            else
            {
                mIndexOfCurrentDialogModel = 0;
                mIsForcedDialogModel = false;
            }
        }


        private void _onSelectedCharactersPairChanged(SelectedCharactersPairEventArgs args)
        {
            if (SessionHelper.UseSerialPort)
            {
                mCharacter1Num = args.Character1Index;
                mCharacter2Num = args.Character2Index;

                if (CurrentState != States.Idle)
                {
                    mStateMachineTaskTokenSource.Cancel();
                    StateMachine.Fire(Triggers.WaitForNewCharacters);
                }

                mEventWaitHandle.Set();
            }
            else
            {
                mRandomSelectionDataCached = args;

                if(CurrentState == States.Idle)
                {
                    mEventWaitHandle.Set();
                }
            }
        }

        #endregion

        #region - dialog generator  helper functions -



        private void _addDialogModelToHistory(int _dialogModelIndex, int _ch1, int _ch2)
        {
            mHistoricalDialogs.Add(new HistoricalDialog
            {
                DialogIndex = _dialogModelIndex,
                DialogName = mDialogModelsList[_dialogModelIndex].Name,
                StartedTime = DateTime.Now,
                Completed = false,
                Character1 = _ch1,
                Character2 = _ch2
            });
        }


        private void _addPhraseToHistory(PhraseEntry _selectedPhrase, int _speakingCharacter)
        {
            mHistoricalPhrases.Add(new HistoricalPhrase
            {
                CharacterIndex = _speakingCharacter,
                CharacterPrefix = mCharactersList[_speakingCharacter].CharacterPrefix,
                PhraseIndex = mCharactersList[_speakingCharacter].Phrases.IndexOf(_selectedPhrase),
                PhraseFile = _selectedPhrase.FileName,
                StartedTime = DateTime.Now
            });

            LoggerHelper.Info(SessionHelper.DialogLogFileName, mCharactersList[_speakingCharacter].CharacterName + ": " + _selectedPhrase.DialogStr);
        }


        private void _playAudio(string _pathAndFileName)
        {
            try
            {
                if (File.Exists(_pathAndFileName))
                {
                    var i = 0;
                    bool _isPlaying = false;
                    Thread.Sleep(300);

                    Dispatcher.Invoke(() => {
                        var _playSuccess = MP3Player.Instance.PlayMp3(_pathAndFileName);

                        if (_playSuccess != 0)
                        {
                            AddItem(new ErrorMessage("MP3 Play Error  ---  " + _playSuccess));
                        }
                    });

                    do
                    {
                        Thread.Sleep(800);

                        Dispatcher.Invoke(() =>
                        {
                            _isPlaying = MP3Player.Instance.IsPlaying();
                        });

                        Thread.Sleep(100);
                        i++;
                    }
                    while (_isPlaying && i < 400);  // don't get stuck,, 40 seconds max phrase

                    Debug.WriteLine("MP3 finishedeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");

                    Thread.Sleep(800); // wait around a second after the audio is done for between phrase pause
                }
                else
                {
                    AddItem(new ErrorMessage("Could not find: " + _pathAndFileName));
                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error(" _playAudio " + ex.Message);
            }
        }


        private bool _importClosestCharacters()
        {
            
            if (!SessionHelper.UseSerialPort)  // check is selection in random mode
            {
                SelectedCharactersPairEventArgs args = mRandomSelectionDataCached;

                mCharacter1Num = args.Character1Index;
                mCharacter2Num = args.Character2Index;
            }

            var _tempChar1 = mCharacter1Num;
            var _tempChar2 = mCharacter2Num;

            if (_tempChar1 == _tempChar2 || _tempChar1 >= mCharactersList.Count || _tempChar2 >= mCharactersList.Count)
                return false;

            mSameCharactersAsLast = (_tempChar1 == mPriorCharacter1Num || _tempChar1 == mPriorCharacter2Num)
                                  && (_tempChar2 == mPriorCharacter1Num || _tempChar2 == mPriorCharacter2Num);

            mCharacter1Num = _tempChar1;
            mCharacter2Num = _tempChar2;
            mPriorCharacter1Num = mCharacter1Num;
            mPriorCharacter2Num = mCharacter2Num;

            return true;
        }


        private int _pickAWeightedDialog(int _character1Num, int _character2Num)
        {
            //TODO check that all characters/phrasetypes required for adventure are included before starting adventure?
            var _dialogModel = 0;
            var _dialogWeightIndex = 0.0;
            var _attempts = 0;
            var _dialogModelFits = false;
            var _mostRecentAdventureDialogIndexes = _findMostRecentAdventureDialogIndexes();

            // most recent will be in the 0 index of list which will be hit first in foreach
            if (_mostRecentAdventureDialogIndexes.Count > 0)
            {
                var _nextAdventureDialogIdx = _findNextAdventureDialogForCharacters(_character1Num, _character2Num, _mostRecentAdventureDialogIndexes);

                if (_nextAdventureDialogIdx > 0 && _nextAdventureDialogIdx < mDialogModelsList.Count)
                    return _nextAdventureDialogIdx; // we have an adventure dialog for these characters go with it
            }

            while (!_dialogModelFits && _attempts < 4000)
            {
                _attempts++;
                _dialogWeightIndex = mRandom.NextDouble();
                _dialogWeightIndex *= mDialogModelPopularitySum;
                double _currentDialogWeightSum = 0;

                foreach (var _dialog in mDialogModelsList)
                {
                    _currentDialogWeightSum += _dialog.Popularity;

                    if (_currentDialogWeightSum > _dialogWeightIndex)
                    {
                        _dialogModel = mDialogModelsList.IndexOf(_dialog);
                        break;
                    }
                }

                var _dialogModelUsedRecently = _checkIfDialogModelUsedRecently(_dialogModel);
                var _charactersHavePhrases = checkIfCharactersHavePhrasesForDialog(_dialogModel,
                    mCharacter1Num, mCharacter2Num);
                var _dialogPreRequirementsMet = _checkIfDialogPreRequirementMet(_dialogModel);
                // don't want a greeting with same characters as last
                var _greetingAppropriate = !(mDialogModelsList[_dialogModel].PhraseTypeSequence[0].Equals("Greeting")
                                             && mSameCharactersAsLast);

                if (_dialogPreRequirementsMet && _charactersHavePhrases && _greetingAppropriate &&
                    !_dialogModelUsedRecently)
                {
                    _dialogModelFits = true;
                }
            }

            return _dialogModel;
        }


        private bool _checkIfDialogModelUsedRecently(int _dialogModel)
        {
            foreach (var _recentDialogQueueEntry in mRecentDialogs) // try again if dialog model recentlyused{
            {
                if (_recentDialogQueueEntry == _dialogModel)
                    return true;
            }

            return false;
        }


        private bool _checkIfDialogPreRequirementMet(int _dialogModel)
        {
            if (mDialogModelsList[_dialogModel].Requires == null || mDialogModelsList[_dialogModel].Requires.Count == 0)
                return true;

            if (!mHistoricalDialogs.Any())
                return false;

            var _lastHistoricalDialog = mHistoricalDialogs.Last();

            foreach (var _requiredTag in mDialogModelsList[_dialogModel].Requires)
            {
                var _currentRequiredTagSatisfied = false;

                foreach (var _histDialog in mHistoricalDialogs)
                {
                    // could speed by only going through unique historical dialog index #s
                    if (mDialogModelsList[_histDialog.DialogIndex].Adventure == mDialogModelsList[_dialogModel].Adventure)
                    {
                        foreach (var _providedTag in mDialogModelsList[_histDialog.DialogIndex].Provides)
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


        private PhraseEntry _pickAWeightedPhrase(int _speakingCharacter, string _currentPhraseType)
        {
            PhraseEntry _selectedPhrase = null;

            try
            {
                _selectedPhrase = mCharactersList[_speakingCharacter].Phrases[0]; //initialize to unused phrase
                //Randomly select a phrase of correct Type
                var _phraseIsDuplicate = true;

                for (var k = 0; k < 6 && _phraseIsDuplicate; k++) //do retries if selected phrase is recently used
                {
                    _phraseIsDuplicate = false;
                    var _phraseTableWeightedIndex = mRandom.NextDouble(); // rand 0.0 - 1.0
                    _phraseTableWeightedIndex *= mCharactersList[_speakingCharacter].PhraseTotals.PhraseWeights[_currentPhraseType];
                    double _amountOfCurrentPhraseType = 0;

                    foreach (var _currentPhraseTableEntry in mCharactersList[_speakingCharacter].Phrases)
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

                    foreach (var _recentPhraseQueueEntry in mCharactersList[_speakingCharacter].RecentPhrases)
                    {
                        if (_recentPhraseQueueEntry.Equals(_selectedPhrase))
                        {
                            _phraseIsDuplicate = true; //send through retry loop k again
                            break; // doesn't matter if duplicated more than once
                        }
                    }
                }

                //eventually overload enque to remove first to keep size same or create a replace
                mCharactersList[_speakingCharacter].RecentPhrases.Dequeue();
                mCharactersList[_speakingCharacter].RecentPhrases.Enqueue(_selectedPhrase);
            }
            catch (Exception ex)
            {
                mcLogger.Error("PickAWeightedPhrase " + ex.Message);
            }

            return _selectedPhrase;
        }


        private int _findNextAdventureDialogForCharacters(int _character1Num, int _character2Num, List<int> _mostRecentAdventureDialogIndexes)
        {
            var _ch1First = new bool();
            var _ch2First = new bool();

            //if we have recently done adventures give priority to adventure dialogs check them first
            foreach (var _recentAdventureIdx in _mostRecentAdventureDialogIndexes)
            {
                //given recent adventures
                foreach (var _possibleDialog in mDialogModelsList) //TODO probably a cleaner way to do this with Linq and lamda expressions
                {
                    //look for follow on adventure possibilities
                    var _possibleDialogIdx = mDialogModelsList.IndexOf(_possibleDialog);

                    if (mDialogModelsList[_recentAdventureIdx].Adventure == _possibleDialog.Adventure)
                    {
                        foreach (var _providedStringKey in mDialogModelsList[_recentAdventureIdx].Provides)
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


        private void _removePhrasesOverParentalRating(Character _inCharacter)
        {
            var _maxParentalRating = ParentalRatings.GetNumeric(SessionHelper.CurrentParentalRating);
            var _minParentalRating = ParentalRatings.GetNumeric("G");

            _inCharacter.Phrases.RemoveAll(_item =>
                                              ParentalRatings.GetNumeric(_item.PhraseRating) > _maxParentalRating
                                           || ParentalRatings.GetNumeric(_item.PhraseRating) < _minParentalRating);
        }


        private List<int> _findMostRecentAdventureDialogIndexes()
        {
            var _mostRecentAdventureDialogs = new List<int>();
            // most recent will be in the 0 index of list
            var _foundAdventures = new List<string>();
            var j = 0;

            for (var i = mHistoricalDialogs.Count - 1; i >= 0; i--)
            {
                var _dialog = mDialogModelsList[mHistoricalDialogs[i].DialogIndex];

                if (_dialog.Adventure.Length > 0 && !_foundAdventures.Contains(_dialog.Adventure))
                {
                    //if the dialog was part of an adventure and we haven't already found the most recent 
                    //from that adventure add the dialog to the most recent adventure list
                    _foundAdventures.Add(_dialog.Adventure);
                    _mostRecentAdventureDialogs.Add(mHistoricalDialogs[i].DialogIndex);
                }

                j++;

                if (j > 400) break; //don't go through all of time looking for active adventures
            }

            return _mostRecentAdventureDialogs;
        }


        private bool checkIfCharactersHavePhrasesForDialog(int _dialogModel, int _character1Num, int _character2Num)
        {
            var _currentCharacter = _character1Num;

            foreach (var _element in mDialogModelsList[_dialogModel].PhraseTypeSequence)
            {
                //try again if characters lack phrases for this model
                if (mCharactersList[_currentCharacter].PhraseTotals.PhraseWeights.ContainsKey(_element))
                {
                    if (mCharactersList[_currentCharacter].PhraseTotals.PhraseWeights[_element] < 0.015f)
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


        private bool _dialogTrackerAndSerialComsCharactersSame()
        {
            if ((mCharacter1Num == SerialSelectionService.NextCharacter1
                 || mCharacter1Num == SerialSelectionService.NextCharacter2)
                 && (mCharacter2Num == SerialSelectionService.NextCharacter2
                 || mCharacter2Num == SerialSelectionService.NextCharacter1))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region - dialog generator state machine  functions -

        public void _onDialogDataLoaded()
        {
            StateMachine.Fire(Triggers.Initialize);
        }

        private Triggers _waitForNewCharacters()
        {
            // we no longer want to wait for new characters return next state at once
            return Triggers.PrepareDialogParameters;

            if (SessionHelper.UseSerialPort)
            {
                mEventWaitHandle.WaitOne();
            }
            else
            {
                if (mRandomSelectionDataCached != null)
                {
                    return Triggers.PrepareDialogParameters;
                }
                else
                {
                    mEventWaitHandle.WaitOne();
                }
            }

            return Triggers.PrepareDialogParameters;
        }

        private async Task<Triggers> _prepareDialogParameters(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                if (!_importClosestCharacters())
                    return Triggers.WaitForNewCharacters;

                token.ThrowIfCancellationRequested();

                mIndexOfCurrentDialogModel = mIsForcedDialogModel ?
                                             mIndexOfCurrentDialogModel
                                            : _pickAWeightedDialog(mCharacter1Num, mCharacter2Num);

                token.ThrowIfCancellationRequested();
                _addDialogModelToHistory(mIndexOfCurrentDialogModel, mCharacter1Num, mCharacter2Num);

                if (SessionHelper.DebugFlag)
                    WriteDialogInfo(mCharacter1Num, mCharacter2Num);

                HeatMapUpdate.PrintHeatMap();

                return Triggers.StartDialog;
            }
            catch (OperationCanceledException ex) { }
            catch (Exception ex)
            {
                mcLogger.Error("_prepareDialogParameters " + ex.Message);
            }

            return Triggers.WaitForNewCharacters;
        }



        private async Task<Triggers>  _startDialog(CancellationToken token)
        {
            try
            {
                var _speakingCharacter = mCharacter1Num;
                var _selectedPhrase = mCharactersList[_speakingCharacter].Phrases[0]; //initialize to unused placeholder phrase

                foreach (var _currentPhraseType in mDialogModelsList[mIndexOfCurrentDialogModel].PhraseTypeSequence)
                {
                    token.ThrowIfCancellationRequested();

                    if (mCharactersList[_speakingCharacter].PhraseTotals.PhraseWeights.ContainsKey(_currentPhraseType))
                    {
                        AddItem(new InfoMessage(mCharactersList[_speakingCharacter].CharacterName + ": "));

                        if (mCharactersList[_speakingCharacter].PhraseTotals.PhraseWeights[_currentPhraseType] < 0.01f)
                        {
                            AddItem(new WarningMessage("Missing PhraseType: " + _currentPhraseType));
                        }

                        token.ThrowIfCancellationRequested();

                        _selectedPhrase = _pickAWeightedPhrase(_speakingCharacter, _currentPhraseType);

                        if (_selectedPhrase == null)
                        {
                            AddItem(new WarningMessage("Phrase type " + _currentPhraseType + " was not found."));
                            continue;
                        }

                        token.ThrowIfCancellationRequested();

                        AddItem(new InfoMessage(_selectedPhrase.DialogStr));

                        if (SessionHelper.TextDialogsOn)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                DialogLinesCollection.Add(new DialogItem() { Character = 
                                    mCharactersList[_speakingCharacter], PhraseEntry = _selectedPhrase });
                            });
                        }

                        token.ThrowIfCancellationRequested();

                        _addPhraseToHistory(_selectedPhrase, _speakingCharacter);

                        var _pathAndFileName = SessionHelper.WizardAudioDirectory
                                              + mCharactersList[_speakingCharacter].CharacterPrefix
                                              + "_" + _selectedPhrase.FileName + ".mp3";
                        Debug.WriteLine(_selectedPhrase.DialogStr + " started");
                        _playAudio(_pathAndFileName); // vb: code stops here so commented out for debugging purpose


                        if (!_dialogTrackerAndSerialComsCharactersSame() && SessionHelper.UseSerialPort)
                            //&& DialogViewModel.SelectedCharactersOn == 0)
                        {
                            mSameCharactersAsLast = false;
                            return Triggers.WaitForNewCharacters; // the characters have moved  TODO break into charactersSame() and use also with prior
                        }
                        //Toggle character
                        if (_speakingCharacter == mCharacter1Num) //toggle which character is speaking next
                            _speakingCharacter = mCharacter2Num;
                        else
                            _speakingCharacter = mCharacter1Num;
                    }

                    mHistoricalDialogs[mHistoricalDialogs.Count - 1].Completed = true;

                    if (mHistoricalDialogs.Count > 2000)
                        mHistoricalDialogs.RemoveRange(0, 100);

                    if (mHistoricalPhrases.Count > 8000)
                        mHistoricalPhrases.RemoveRange(0, 100);

                    mRecentDialogs.Dequeue(); //move to use HistoricalDialogs
                    mRecentDialogs.Enqueue(mIndexOfCurrentDialogModel);
                }
            }
            catch (OperationCanceledException){}
            catch (Exception ex)
            {
                mcLogger.Error("_startDialog " + ex.Message);
            }

            return Triggers.WaitForNewCharacters;
        }

        #endregion

        #endregion

        #region - public functions -

        public Task StartDialogGenerator()
        {
            mCancellationTokenSource = new CancellationTokenSource();

            if (CurrentState != States.Idle)
                StateMachine.Fire(Triggers.WaitForNewCharacters);

            return Task.Run(async() => {

                Thread.CurrentThread.Name = "DialogGeneratorThread";

                do
                {
                    mStateMachineTaskTokenSource = new CancellationTokenSource();

                    switch (StateMachine.State)
                    {
                        case States.Idle:
                            {
                                Triggers _nextTrigger = _waitForNewCharacters();
                                //Debug.WriteLine(_nextTrigger.ToString());
                                if(StateMachine.CanFire(_nextTrigger))
                                    StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.PreparingDialogParameters:
                            {
                                Triggers _nextTrigger = await _prepareDialogParameters(mStateMachineTaskTokenSource.Token);
                                //Debug.WriteLine(_nextTrigger.ToString());

                                if (StateMachine.CanFire(_nextTrigger))
                                    StateMachine.Fire(_nextTrigger);
                                break;
                            }
                        case States.DialogStarted:
                            {
                                Triggers _nextTrigger = await _startDialog(mStateMachineTaskTokenSource.Token);
                                //Debug.WriteLine(_nextTrigger.ToString());

                                if (StateMachine.CanFire(_nextTrigger))
                                    StateMachine.Fire(_nextTrigger);
                                break;
                            }                 
                    }
                }
                while (!mCancellationTokenSource.Token.IsCancellationRequested);
            });
        }


        public void StopDialogGenerator()
        {
            mCancellationTokenSource.Cancel();

            if (CurrentState != States.DialogFinished)
                StateMachine.Fire(Triggers.FinishDialog);

            DialogLinesCollection.Clear();
        }


        public void SwapCharactersOneAndTwo()
        {
            var _tempCh1 = mCharacter1Num;
            mCharacter1Num = mCharacter2Num;
            mCharacter2Num = _tempCh1;
            // it doesn't appear we should update prior characters 1 and 2 here
        }

        /// <summary>
        /// Writes dialog info
        /// </summary>
        /// <param name="_character1Num"></param>
        /// <param name="_character2Num"></param>
        public void WriteDialogInfo(int _character1Num, int _character2Num)
        {
            if (mDialogModelsList.Count > 0)
            {
                var _dialogModelString = " --DiMod " + mIndexOfCurrentDialogModel + " "
                                         + mDialogModelsList[mIndexOfCurrentDialogModel].Name
                                         + " NextChars: " + mCharactersList[_character1Num].CharacterPrefix + " "
                                         + mCharactersList[_character2Num].CharacterPrefix + " " + DateTime.Now;


                AddItem(new InfoMessage(_dialogModelString));
                LoggerHelper.Info(SessionHelper.DialogLogFileName, _dialogModelString);
            }
        }


        #endregion

        #region - properties -

        public StateMachine StateMachine
        {
            get { return mStateMachine; }
            set
            {
                mStateMachine = value;
                OnPropertyChanged("StateMachine");
            }
        }


        public States CurrentState
        {
            get { return mCurrentState; }
            set
            {
                mCurrentState = value;
                OnPropertyChanged("CurrentState");
            }
        }


        public ObservableCollection<object> DialogLinesCollection
        {
            get { return mDialogLinesCollection; }
            set
            {
                mDialogLinesCollection = value;
                OnPropertyChanged("DialogLinesCollection");
            }
        }


        public bool IsItemAdded
        {
            get { return mIsItemAdded; }
            set
            {
                mIsItemAdded = value;
                OnPropertyChanged("IsItemAdded");
            }
        }

        #endregion
    }
}
