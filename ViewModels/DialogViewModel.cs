//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using DialogEngine.Core;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using log4net;
using DialogEngine.Models.Logger;
using System.Threading.Tasks;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using System.Windows.Data;
using DialogEngine.Views;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using DialogEngine.Models.Shared;
using DialogEngine.Services;
using DialogEngine.Workflows.DialogPageWorkflow;
using DialogEngine.Controls.ViewModels;
using System.Linq;
using MaterialDesignThemes.Wpf;
using DialogEngine.Dialogs;
using System.Diagnostics;

namespace DialogEngine.ViewModels
{
    /// <summary>
    ///     Implementation of <see cref="ViewModelBase" />
    ///     DataContext for Dialog.xaml/>
    /// </summary>
    public class DialogViewModel : ViewModelBase
    {
        #region - Fieds -

        //default application logger
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static bool[] mRadiosState = new bool[6];
        private int mRSSIsum;
        private string  mCharacter1Prefix = "--";
        private string mCharacter2Prefix = "--";
        private bool mRSSIstable;
        private StateMachine mDvmStateMachine;
        private States mCurrentState;
        private DialogView mView; 
        // private readonly Random mRandom = new Random();   seems we don't need this isaac
        // start position of drag operation from characters listbox to radio textbox
        private Point mStartPosition;
        private RandomSelectionService mRandomSelectionService;
        private SerialSelectionService mSerialSelectionService;
        private ICharacterSelection mCurrentSelectionService;

        public DialogGeneratorViewModel mDialogGeneratorViewModel;
        public static int NumberOfCharactersOn;
        public static int SelectedIndex1;
        public static int SelectedIndex2;

        #endregion

        #region - Constructor -

        /// <summary>
        /// Creates instance of DialogViewModel.cs
        /// </summary>
        public DialogViewModel(DialogView view)
        {
            mView = view;
            DvmStateMachine = new StateMachine
                (
                 action: () => { }
                );

            mRandomSelectionService = new RandomSelectionService();
            mSerialSelectionService = new SerialSelectionService();
            mDialogGeneratorViewModel = new DialogGeneratorViewModel();

            _configureStateMachine();
            _subscribeForEvents();
            _bindCommands();

            DvmStateMachine.Fire(Triggers.Initialize);

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1)
                .AddDays(version.Build).AddSeconds(version.Revision * 2);
            string _displayableVersion = $"Starting DialogEngine version {version} created {buildDate}";

            DialogDataHelper.AddMessage(new InfoMessage(_displayableVersion));
            LoggerHelper.Info(SessionHelper.DialogLogFileName, _displayableVersion);
        }

        #endregion

        public void SetData(string data)
        {
            Debug.WriteLine("received data : " + data);
        }

        #region - Commands -

        /// <summary>
        /// Starts dialog 
        /// </summary>
        public Core.RelayCommand GenerateDialog { get; set; }

        /// <summary>
        /// Clears messages from dialog or debug console depends on message type
        /// </summary>
        public Core.RelayCommand ClearAllMessages { get; set; }

        /// <summary>
        /// Stops dialog
        /// </summary>
        public Core.RelayCommand StopDialog { get; set; }

        public Core.RelayCommand EditCharacterCommand { get; set; }

        public Core.RelayCommand RemoveCharacterCommand { get; set; }

        /// <summary>
        /// Unbinds radio from character
        /// </summary>
        public Core.RelayCommand ClearRadioBindingCommand { get; set; }

        // MVVM light commands where we can pass event object

        public RelayCommand<MouseButtonEventArgs> PreviewMouseLeftButtonDownCommand { get; set; }


        public RelayCommand<MouseEventArgs> PreviewMouseMoveCommand { get; set; }

        /// <summary>
        /// Fires when dragging character from characters listbox enter textbox
        /// </summary>
        public RelayCommand<DragEventArgs> DragEnterCommand { get; set; }

        /// <summary>
        /// Fires when character from characters listbox dropped to textbox
        /// </summary>
        public RelayCommand<DragEventArgs> DropCommand { get; set; }

        /// <summary>
        /// Fires during dragging item form characters listbox to detect where it can be dropped  
        /// </summary>
        public RelayCommand<DragEventArgs> DragOverCommand { get; set; }

        /// <summary>
        /// Selected  dialog model from specified dialog .json file
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> DialogModelSelectionChangedCommand { get; set; }

        /// <summary>
        /// Recalculate width for TabItem columns in debug view
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> RefreshTabItem { get; set; }


        #endregion

        #region - event handlers -

        private void _dialogData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }


        private void _stateMachine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("State"))
            {
                CurrentState = DvmStateMachine.State;
            }
        }

        private async void CharacterCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _setCharacterToRadioBindings();
        }

        #endregion

        #region - Private methods -


        private void _configureStateMachine()
        {
            DvmStateMachine.Configure(States.Start)
                .Permit(Triggers.Initialize,States.Init);

            DvmStateMachine.Configure(States.Init)
                .OnEntry(t => _initDialogData())
                .Permit(Triggers.Idle, States.Idle);

            DvmStateMachine.Configure(States.Idle)
                .Permit(Triggers.StartRadnomSelection, States.RandomSelectionStarted)
                .Permit(Triggers.StartSerialSelection, States.SerialSelectionStarted);

            DvmStateMachine.Configure(States.CharacterSelectionStarted)
                .Permit(Triggers.Idle, States.Idle);

            DvmStateMachine.Configure(States.RandomSelectionStarted)
                .SubstateOf(States.CharacterSelectionStarted);

            DvmStateMachine.Configure(States.SerialSelectionStarted)
                .SubstateOf(States.CharacterSelectionStarted);
        }


        private void _subscribeForEvents()
        {
            EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Subscribe(_onChangedCharacterState);
            EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Subscribe(_onChangedModelDialogState);
            DialogData.Instance.PropertyChanged += _dialogData_PropertyChanged;
            DvmStateMachine.PropertyChanged += _stateMachine_PropertyChanged;
            DialogData.Instance.CharacterCollection.CollectionChanged += CharacterCollection_CollectionChanged;
        }

        private async void _initDialogData()
        {
            Task _checkTagsUsedTask = null;
            Task _checkForMissingPhrasesTask;

            await DialogDataHelper.LoadDialogDataAsync(SessionHelper.WizardDirectory);
          
            if (SessionHelper.TagUsageCheck)
                _checkTagsUsedTask = _checkTagsUsedAsync();

            _checkForMissingPhrasesTask = _checkForMissingPhrasesAsync();

            if (_checkTagsUsedTask != null)
            {
                await _checkTagsUsedTask;
            }

            await _checkForMissingPhrasesTask;

            _setCharacterToRadioBindings();

            DvmStateMachine.Fire(Triggers.Idle);
        }


        // refresh NumberOfCharactersOn when character state is changed
        private void _onChangedCharacterState()
        {
            try
            {
                int result = 0;
                int index = 0;
                NumberOfCharactersOn = 0;
                SelectedIndex1 = -1;
                SelectedIndex2 = -1;

                // iterate over characters and try to find characters in ON state
                // then assign indexes to mSelectedIndex 
                foreach (Character characterInfo in CharacterCollection)
                {
                    if (characterInfo.State == Models.Enums.CharacterState.On)
                    {
                        string fieldName = "SelectedIndex" + (result + 1);
                        // get field using reflection
                        var field = typeof(DialogViewModel).GetField(fieldName);
                        field.SetValue(null, index);
                        result += 1;

                        if (result >= 2)
                            break;
                    }
                    index++;
                }

                NumberOfCharactersOn = result;

                OnPropertyChanged("NumberOfCharactersOn");

                // when state of character changed, we want to cancel current dialog and reset MP3 player
                EventAggregator.Instance.GetEvent<StopPlayingCurrentDialogLineEvent>().Publish();
            }
            catch (Exception ex)
            {
                mcLogger.Error("Character state changed. " + ex.Message);
            }
        }


        // change indicator if state of dialog models changed
        private void _onChangedModelDialogState()
        {
            //mIsModelsDialogChanged = true;
        }


        private async void _setCharacterToRadioBindings()
        {
            // reset radio textboxes
            if (Dispatcher.CheckAccess())
            {
                _setBindings();
            }
            else
            {
                await Dispatcher.BeginInvoke((Action)(() =>
                {
                    _setBindings();
                }));
            }
        }


        private void _setBindings()
        {
            foreach (TextBox tb in VisualHelper.FindVisualChildren<TextBox>(this.View.RadioTextBoxesContainer))
            {
                tb.Tag = null;
                tb.Text = "";
            }

            for (int i = 0; i < SerialSelectionService.NumRadios && i < CharacterCollection.Count; i++)
            {
                CharacterCollection[i].RadioNum = i;

                string textBoxName = "Radio_" + i.ToString();
                (mView.FindName(textBoxName) as TextBox).Text = CharacterCollection[i].CharacterName;
                (mView.FindName(textBoxName) as TextBox).Tag = CharacterCollection[i];
                mRadiosState[i] = true;
            }
        }


        // choose collection where to add object depend on type of argument
        private void _processAddMessage(LogMessage entry)
        {
            try
            {
                if (entry is InfoMessage)
                {
                    InfoMessagesCollection.Insert(0, (InfoMessage)entry);
                    int _length = InfoMessagesCollection.Count;

                    if (_length > 300)
                    {
                        InfoMessagesCollection.RemoveAt(_length - 1);
                    }

                    OnPropertyChanged("InfoMessagesCollection");
                }
                else if (entry is WarningMessage)
                {
                    WarningMessagesCollection.Insert(0, (WarningMessage)entry);
                    int _length = WarningMessagesCollection.Count;

                    if (_length > 300)
                    {
                        WarningMessagesCollection.RemoveAt(_length - 1);
                    }

                    OnPropertyChanged("WarningMessagesCollection");
                }
                else
                {
                    ErrorMessagesCollection.Insert(0, (ErrorMessage)entry);
                    int _length = ErrorMessagesCollection.Count;

                    if (_length > 300)
                    {
                        ErrorMessagesCollection.RemoveAt(_length - 1);
                    }

                    OnPropertyChanged("ErrorMessagesCollection");
                }
            }
            catch (Exception e)
            {
                mcLogger.Error("process_addMessage " + e.Message);
            }
        }


        private void _bindCommands()
        {
            GenerateDialog = new Core.RelayCommand(_x => _startDialog());
            ClearAllMessages = new Core.RelayCommand(x => _clearAllMessages((string)x));
            StopDialog = new Core.RelayCommand(x => _stopDialog());
            ClearRadioBindingCommand = new Core.RelayCommand(x => _clearRadioBindingCommand((string)x));
            RemoveCharacterCommand = new Core.RelayCommand(x => _removeCharacter((Character)x));

            // MVVM light commands where we can pass event object

            PreviewMouseLeftButtonDownCommand = new RelayCommand<MouseButtonEventArgs>(_previewMouseLeftButtonCommand);
            PreviewMouseMoveCommand = new RelayCommand<MouseEventArgs>(_previewMouseMoveCommand);
            DragEnterCommand = new RelayCommand<DragEventArgs>(_dragEnterCommand);
            DropCommand = new RelayCommand<DragEventArgs>(_dropCommand);
            DragOverCommand = new RelayCommand<DragEventArgs>(_dragOverCommand);
            DialogModelSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(_dialogModelSelectionChanged);
            RefreshTabItem = new RelayCommand<SelectionChangedEventArgs>(_refreshTabItem);
        }

        private async void _removeCharacter(Character character)
        {
            var result = await DialogHost.Show(new YesNoDialog("", "Are you sure you want to delete this character? ", "Yes", "No"));

            if (result != null)
            {
                int index = DialogData.Instance.CharacterCollection.Select((c, i) => new { ch = c, index = i })
                                                  .First(x => x.ch.CharacterPrefix.Equals(character.CharacterPrefix)).index;

                DialogData.Instance.CharacterCollection.RemoveAt(index);
            }
        }


        private void _dialogModelSelectionChanged(SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                int index;
                int result = 0;
                ComboBox source = e.Source as ComboBox;
                int _indexOfSelectedDialogModel = source.SelectedIndex;
                ModelDialogInfo _selectedDialogModel = source.Tag as ModelDialogInfo;

                index = DialogModelCollection.IndexOf(_selectedDialogModel);

                // iterate over dialog model files
                for (int i = 0; i < index; i++)
                {
                    if (DialogModelCollection[i].State == Models.Enums.ModelDialogState.On)
                    {
                        result += DialogModelCollection[i].ArrayOfDialogModels.Count;
                    }
                }

                _indexOfSelectedDialogModel += result;

                EventAggregator.Instance.GetEvent<DialogModelChangedEvent>().
                    Publish(new Events.EventArgs.SelectionChangedEventArgs { IsSelected = true, Index = _indexOfSelectedDialogModel });
            }
            else
            {
                EventAggregator.Instance.GetEvent<DialogModelChangedEvent>().
                    Publish(new Events.EventArgs.SelectionChangedEventArgs { IsSelected = false });
            }
        }


        // unassign character from radio
        private void _clearRadioBindingCommand(string _elementName)
        {
            TextBox tb = mView.FindName(_elementName) as TextBox;
            string[] _nameRadioNum = tb.Name.Split('_');
            int _numRadio = int.Parse(_nameRadioNum[1]);
            tb.Text = "";
            (tb.Tag as Character).RadioNum = -1;
            tb.Tag = null;

            mRadiosState[_numRadio] = false;

            mView.CharactersListBox.Items.Refresh();
        }


        private void _dragOverCommand(DragEventArgs e)
        {
            //We need to add this to override the default PreviewDrag behavior. If we don’t, then the Drop Event will not fire.
            e.Handled = true;
        }


        private void _dropCommand(DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent("characterFormat"))
                {
                    Character character = e.Data.GetData("characterFormat") as Character;  // character from listbox
                    TextBox tb = e.Source as TextBox;  // textbox where we want to drop character
                    Character _tbCharacter = tb.Tag == null ? null : (Character)tb.Tag; //check is any character already assigned to this textbox

                    // prevent dropping of the same character
                    if (_tbCharacter != null && _tbCharacter.CharacterName.Equals(character))
                    {
                        e.Handled = true;
                        return;
                    }
                    else 
                    {
                        // TextBox has name in form of "Radio_x"  x - radio number
                        string[] _nameRadioNum = tb.Name.Split('_');
                        int _numRadio = int.Parse(_nameRadioNum[1]);

                        // if radioNum == -1 then character is already assigned
                        if (character.RadioNum < 0)
                        {
                            character.RadioNum = _numRadio;

                            if (tb.Tag != null) // reset values for character which was assigned before 
                            {
                                (tb.Tag as Character).RadioNum = -1;
                            }

                            tb.Text = character.CharacterName;
                            tb.Tag = character;
                        }
                        else
                        {
                            // if character already assigned we get textbox where it was dropped
                            TextBox _tbClear = mView.FindName("Radio_" + character.RadioNum) as TextBox;
                            // assign new radio number for dropping character
                            character.RadioNum = _numRadio;

                            // clear former textbox
                            _tbClear.Text = "";
                            _tbClear.Tag = null;

                            // assign new character
                            tb.Text = character.CharacterName;

                            if (_tbCharacter != null)
                            {
                                _tbCharacter.RadioNum = -1;
                            }

                            tb.Tag = character;
                        }

                        if (!mRadiosState[_numRadio])
                        {
                            mRadiosState[_numRadio] = true;
                        }

                        mView.CharactersListBox.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error("Error during executing drop command. "+ ex.Message);
            }
            e.Handled = true;
        }


        private void _dragEnterCommand(DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent("characterFormat") || !(e.Source is TextBox))               
                    e.Effects = DragDropEffects.None;                
                else                
                    e.Effects = DragDropEffects.Copy;
            }
            catch (Exception ex)
            {
                mcLogger.Error("Drag enter command. "+ ex.Message);
            }

            e.Handled = true;
        }


        private void _previewMouseMoveCommand(MouseEventArgs e)
        {
            try
            {
                Point _mousePos = e.GetPosition(null);
                Vector diff = mStartPosition - _mousePos;

                if ((  e.LeftButton == MouseButtonState.Pressed)
                    &&(Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    ListBox _listBox = e.Source as ListBox;
                    ListBoxItem _listBoxItem = VisualHelper.GetNearestContainer<ListBoxItem>((DependencyObject)e.OriginalSource);

                    Character character = (Character)_listBox.ItemContainerGenerator.ItemFromContainer(_listBoxItem);

                    DataObject _dragData = new DataObject("characterFormat", character);

                    DragDrop.DoDragDrop(_listBoxItem, _dragData, DragDropEffects.Copy);
                }

            }
            catch (Exception ex)
            {
                mcLogger.Error("Error during preview mosue down. "+ ex.Message);
            }

            e.Handled = true;
        }


        private void _previewMouseLeftButtonCommand(MouseButtonEventArgs e)
        {
            mStartPosition = e.GetPosition(null);
        }


        // forces TabItem to refresh binding for GridView columns width
        private void _refreshTabItem(SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem)
                {
                    string tag = (e.AddedItems[0] as TabItem).Tag?.ToString();

                    if (tag != null && !tag.Equals("HeatMapUpdate"))
                    {
                        GridViewColumn column = mView.InfoGridViewColumn;

                        BindingOperations.GetBindingExpression(column, GridViewColumn.WidthProperty).UpdateTarget();
                    }
                }
            }
            catch(Exception ex)
            {
                mcLogger.Error("Error during refreshing tab item binding. " + ex.Message);
            }
        }


        // clears collection depend of argument type
        private void _clearAllMessages(string type)
        {
            switch (type)
            {
                case "InfoMessagesCollection":
                    {
                        InfoMessagesCollection.Clear();
                        OnPropertyChanged(type);
                        break;
                    }

                case "WarningMessagesCollection":
                    {
                        WarningMessagesCollection.Clear();
                        OnPropertyChanged(type);
                        break;
                    }

                case "ErrorMessagesCollection":
                    {
                        ErrorMessagesCollection.Clear();
                        OnPropertyChanged(type);
                        break;
                    }
            }
        }


        private async Task _checkForMissingPhrasesAsync()
        {
            await Task.Run(() =>
            {
                Thread.CurrentThread.Name = "_checkForMissingPhrasesAsyncThread";

                foreach (var _character in CharacterCollection)
                {
                    foreach (var _phrase in _character.Phrases)
                    {
                        if (!File.Exists(SessionHelper.WizardAudioDirectory
                            + _character.CharacterPrefix + "_"
                            + _phrase.FileName + ".mp3")) //Char name and prefix are being left blank...
                        {
                            var _debugMessage = "Missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr;

                            _addMessage(new WarningMessage(_debugMessage));

                            LoggerHelper.Info(SessionHelper.DialogLogFileName, "missing " 
                                              + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr);
                        }
                    }
                }
            });
            //TODO check that all dialog models have unique names and that MPAA ratings like PG13 are all correct
        }


        private async Task _checkTagsUsedAsync()
        {
            await Task.Run(() =>
            {
                Thread.CurrentThread.Name = "_checkTagsUsedAsyncThread";

                //test that all character tags are used by a dialog model.
                _addMessage(new InfoMessage("Check characters tags are used "));

                var _usedFlag = false;

                foreach (var _character in CharacterCollection)
                    foreach (var _phrase in _character.Phrases)
                        foreach (var _phraseTag in _phrase.PhraseWeights.Keys)
                        {
                            _usedFlag = false;

                            foreach (var _dialogInfo in DialogModelCollection)
                            {
                                foreach(var dialog in _dialogInfo.ArrayOfDialogModels)
                                    foreach (var _dialogTag in dialog.PhraseTypeSequence)
                                        if (_phraseTag == _dialogTag)
                                        {
                                            _usedFlag = true;
                                            break;
                                        }

                                    if (_usedFlag)
                                        break;
                            }

                            if (!_usedFlag)
                            {
                                _addMessage(new InfoMessage(" " + _phraseTag + " is not used."));
                                LoggerHelper.Info(SessionHelper.DialogLogFileName, " " + _phraseTag + " is not used.");
                            }
                        }

                foreach (var _dialogInfo in DialogModelCollection)
                    foreach (var dialog in _dialogInfo.ArrayOfDialogModels)
                        foreach (var _dialogTag in dialog.PhraseTypeSequence) //each dialog model tag
                        {
                            _usedFlag = false;

                            foreach (var _character in CharacterCollection)
                            {
                                foreach (var _characterPhrase in _character.Phrases)
                                {
                                    foreach (var _phraseTag in _characterPhrase.PhraseWeights.Keys) //each character phrase tag{
                                        if (_dialogTag == _phraseTag)
                                        {
                                            _usedFlag = true;
                                            break;
                                        }

                                    if (_usedFlag)
                                        break;
                                }

                                if (_usedFlag)
                                    break;
                            }

                            if (!_usedFlag)
                            {
                                _addMessage(new InfoMessage(" " + _dialogTag + " not used in " + dialog.Name));
                                LoggerHelper.Info(SessionHelper.DialogLogFileName, " " + _dialogTag + " not used in " + dialog.Name);
                            }
                        }
            });
        }

        private void _addMessage(LogMessage _entry)
        {
            try
            {
                if (Dispatcher.CheckAccess())
                {
                    _processAddMessage(_entry);
                }
                else
                {
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        _processAddMessage(_entry);
                    }));
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error("Error during adding an item. " + ex.Message);
            }
        }

        // stops dialog
        private void _stopDialog()
        {


            EventAggregator.Instance.GetEvent<StopImmediatelyPlayingCurrentDialogLIne>().Publish();
            mCurrentSelectionService.Stop();
            mDialogGeneratorViewModel.StopDialogGenerator();

            DvmStateMachine.Fire(Triggers.Idle);
        }


        // starts new dialog
        private async  void _startDialog()
        {
            try
            {
                if (SessionHelper.UseSerialPort)
                {
                    mCurrentSelectionService = mSerialSelectionService;
                    DvmStateMachine.Fire(Triggers.StartSerialSelection);

                    EventAggregator.Instance.GetEvent<CharacterSelectionStartedEvent>().Publish(Models.Enums.SelectionMode.Serial);
                }
                else
                {
                    mCurrentSelectionService = mRandomSelectionService;
                    DvmStateMachine.Fire(Triggers.StartRadnomSelection);

                    EventAggregator.Instance.GetEvent<CharacterSelectionStartedEvent>().Publish(Models.Enums.SelectionMode.Random);
                }

                Task selectionServiceTask = mCurrentSelectionService.Start();
                Task dialogGeneratorTask = mDialogGeneratorViewModel.StartDialogGenerator();

                await selectionServiceTask;
                await dialogGeneratorTask;
            }
            catch (Exception ex)
            {
                mcLogger.Error("_startDialog " + ex.Message);
            }

            EventAggregator.Instance.GetEvent<CharacterSelectionStartedEvent>().Publish(Models.Enums.SelectionMode.NoSelection);


            if (DvmStateMachine.CanFire(Triggers.Idle))
                DvmStateMachine.Fire(Triggers.Idle);    
        }


        #endregion

        #region - Properties -


        public StateMachine DvmStateMachine
        {
            get { return mDvmStateMachine; }
            set
            {
                mDvmStateMachine = value;
                OnPropertyChanged("DvmStateMachine");
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

        /// <summary>
        /// Signal strengh received from toys 
        /// </summary>
        public  int[,] HeatMapUpdate
        {
            get { return DialogData.Instance.HeatMapUpdate; }
        }

        /// <summary>
        /// Selected character 1 - CharacterPrefix
        /// </summary>
        public string Character1Prefix
        {
            get { return mCharacter1Prefix; }
            set
            {
                mCharacter1Prefix = value;
                OnPropertyChanged("Character1Prefix");
            }
        }


        /// <summary>
        /// RSSIstable
        /// </summary>
        public bool RSSIstable
        {
            get { return mRSSIstable; }
            set
            {
                mRSSIstable = value;
                OnPropertyChanged("RSSIstable");
            }
        }


        /// <summary>
        /// RSSIsum
        /// </summary>
        public int RSSIsum
        {
            get { return mRSSIsum; }
            set
            {
                mRSSIsum = value;
                OnPropertyChanged("RSSIsum");
            }
        }


        /// <summary>
        /// Selected character 2 - CharacterPrefix
        /// </summary>
        public string Character2Prefix
        {
            get { return mCharacter2Prefix; }
            set
            {
                mCharacter2Prefix = value;
                OnPropertyChanged("Character2Prefix");
            }
        }


        public DialogGeneratorViewModel DialogGeneratorViewModel
        {
            get { return mDialogGeneratorViewModel; }
            set
            {
                mDialogGeneratorViewModel = value;
                OnPropertyChanged("DialogGeneratorViewModel");
            }
        }

        /// <summary>
        /// Reference on View (DialogView.xaml)
        /// </summary>
        public DialogView View
        {
            get { return mView; }
            set { this.mView = value; }
        }

        /// <summary>
        /// Collection of <see cref="Character"/>
        /// Source for characters combobox
        /// </summary>
        public ObservableCollection<Character> CharacterCollection
        {
            get { return DialogData.Instance.CharacterCollection; }
        }

        /// <summary>
        /// Collection of <see cref="ModelDialogInfo"/>
        /// Source for model dialogs combobox
        /// </summary>
        public ObservableCollection<ModelDialogInfo> DialogModelCollection
        {
            get { return DialogData.Instance.DialogModelCollection; }
        }

        /// <summary>
        /// Collection of <see cref="ErrorMessage"/>
        /// Source for GridView in "ErrorMessage" TabItem
        /// </summary>
        public ObservableCollection<ErrorMessage> ErrorMessagesCollection
        {
            get { return DialogData.Instance.ErrorMessagesCollection; }
        }

        /// <summary>
        /// Collection of <see cref="WarningMessage"/>
        /// Source for GridView in "WarningMessage" TabItem
        /// </summary>
        public ObservableCollection<WarningMessage> WarningMessagesCollection
        {
            get { return DialogData.Instance.WarningMessagesCollection; }
        }

        /// <summary>
        /// Collection of <see cref="InfoMessage"/>
        /// Source for GridView in "InfoMessage" TabItem
        /// </summary>
        public ObservableCollection<InfoMessage> InfoMessagesCollection
        {
            get { return DialogData.Instance.InfoMessagesCollection; }
        }


        /// <summary>
        /// Radios states
        /// </summary>
        public static bool[] RadiosState
        {
            get { return mRadiosState; }
        }

        #endregion
    }
}