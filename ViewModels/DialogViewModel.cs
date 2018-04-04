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
        // counter for characters in On state
        private static int msSelectedCharactersOn;
        private static DialogViewModel msInstance = null;
        private static readonly object mcPadlock = new object();
        // reference on view
        private DialogView mView; 
        private readonly Random mRandom = new Random();
        // start position of drag operation from characters listbox to radio textbox
        private Point mStartPosition;
        // detect is dialog model changed, true value force application to reload dialog model
        private bool mIsModelsDialogChanged;
        private int mSelectedIndex1;
        private int mSelectedIndex2;
        // variables for debuging selection of characters in serial mode

        private string mCharacter1Prefix = "--";
        private string mCharacter2Prefix = "--";
        private bool mRSSIstable;
        private int mRSSIsum;
        private int[,] mHeatMap = new int[SerialComs.NumRadios, SerialComs.NumRadios];

        // indicate when dialog is active or no
        private bool mIsDialogStopped = true;
        // selected dialog model .json file
        private ModelDialogInfo mSelectedDialogModel;
        private ObservableCollection<Character> mCharacterCollection;
        private ObservableCollection<ModelDialogInfo> mDialogModelCollection;

        // Collections of debug messages

        private ObservableCollection<InfoMessage> mInfoMessagesCollection;
        private ObservableCollection<WarningMessage> mWarningMessagesCollection;
        private ObservableCollection<ErrorMessage> mErrorMessagesCollection;

        private bool[] mRadiosState = new bool[6];

        #endregion

        #region - Singleton - 

        /// <summary>
        /// Implementation of singleton pattern for DialogTracker class
        /// </summary>
        public static DialogViewModel Instance
        {
            get
            {
                lock (mcPadlock)
                {
                    if (msInstance == null)
                    {
                        msInstance = new DialogViewModel();
                    }
                    return msInstance;
                }
            }
        }

        #endregion

        #region - Constructor -

        /// <summary>
        /// Creates instance of DialogViewModel.cs
        /// </summary>
        public DialogViewModel()
        {
            DialogData.Instance.PropertyChanged += _dialogData_PropertyChanged;
            EventAggregator.Instance.GetEvent<ChangedCharactersStateEvent>().Subscribe(_onChangedCharacterState);
            EventAggregator.Instance.GetEvent<ChangedModelDialogStateEvent>().Subscribe(_onChangedModelDialogState);
            EventAggregator.Instance.GetEvent<DialogDataLoadedEvent>().Subscribe(_onDialogDataLoaded);

            _bindCommands();
        }

        #endregion

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

        #endregion

        #region - Private methods -

        // refresh SelectedCharactersOn when character state is changed
        private void _onChangedCharacterState()
        {
            try
            {
                int result = 0;
                SelectedCharactersOn = 0;
                SelectedIndex1 = -1;
                SelectedIndex2 = -1;
                int index = 0;

                // iterate over characters and try to find characters in ON state
                // then assign indexes to mSelectedIndex 
                foreach (Character characterInfo in CharacterCollection)
                {
                    if (characterInfo.State == Models.Enums.CharacterState.On)
                    {
                        string fieldName = "mSelectedIndex" + (result + 1);
                        // get field using reflection
                        var field = this.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                        field.SetValue(this, index);
                        result += 1;
    
                        if (result == 2)
                            break;
                    }
                    index++;
                }

                SelectedCharactersOn = result;

                OnPropertyChanged("SelectedCharactersOn");

                // when state of character changed, we want to cancel current dialog and reset MP3 player
                EventAggregator.Instance.GetEvent<StopPlayingCurrentDialogLineEvent>().Publish();
            }
            catch (Exception ex)
            {
                mcLogger.Error("Character state changed. "+ ex.Message);
            }
        }


        // change indicator if state of dialog models changed
        private void _onChangedModelDialogState()
        {
            mIsModelsDialogChanged = true;
        }


        private async void _setCharacterToRadioBidnings()
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

            for (int i = 0; i < SerialComs.NumRadios && i < mCharacterCollection.Count; i++)
            {
                mCharacterCollection[i].RadioNum = i;

                string textBoxName = "Radio_" + i.ToString();
                (mView.FindName(textBoxName) as TextBox).Text = mCharacterCollection[i].CharacterName;
                (mView.FindName(textBoxName) as TextBox).Tag = mCharacterCollection[i];
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

            EditCharacterCommand = new Core.RelayCommand(x => _editCharacter((Character)x));

            // MVVM light commands where we can pass event object

            PreviewMouseLeftButtonDownCommand = new RelayCommand<MouseButtonEventArgs>(_previewMouseLeftButtonCommand);

            PreviewMouseMoveCommand = new RelayCommand<MouseEventArgs>(_previewMouseMoveCommand);

            DragEnterCommand = new RelayCommand<DragEventArgs>(_dragEnterCommand);

            DropCommand = new RelayCommand<DragEventArgs>(_dropCommand);

            DragOverCommand = new RelayCommand<DragEventArgs>(_dragOverCommand);

            DialogModelSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(_dialogModelSelectionChanged);

            RefreshTabItem = new RelayCommand<SelectionChangedEventArgs>(_refreshTabItem);
        }


        private async void _editCharacter(Character character)
        {
            await Dispatcher.BeginInvoke((Action) (() => {
                (Application.Current.MainWindow.FindName("mainFrame") as Frame).NavigationService.Navigate(new WizardView(character));
            }));
        }


        private async void _onDialogDataLoaded()
        {
            Task _checkTagsUsedTask = _checkTagsUsedAsync();
            Task _checkForMissingPhrasesTask = _checkForMissingPhrasesAsync();

            await _checkTagsUsedTask;
            await _checkForMissingPhrasesTask;

            _setCharacterToRadioBidnings();
        }


        private void _dialogModelSelectionChanged(SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                ComboBox source = e.Source as ComboBox;

                SelectedDialogModel = source.Tag as ModelDialogInfo;

                SelectedDialogModel.SelectedModelDialogIndex = source.SelectedIndex;
            }
            else
            {
                SelectedDialogModel = null;
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
                    Character character = e.Data.GetData("characterFormat") as Character;
                    TextBox tb = e.Source as TextBox;

                    // prevent dropping of the same character
                    if (tb.Tag != null)
                    {
                        if ((tb.Tag as Character).CharacterName.Equals(character.CharacterName))
                        {
                            e.Handled = true;
                            return;
                        }
                    }

                    // TextBox has name in form of "Radio_x"  x - radio number
                    string[] _nameRadioNum = tb.Name.Split('_');
                    int _numRadio = int.Parse(_nameRadioNum[1]);

                    // if radioNum == -1 then character is already assigned
                    if (character.RadioNum < 0)
                    {
                        character.RadioNum = _numRadio;

                        if (tb.Tag != null)
                        {
                            (tb.Tag as Character).RadioNum = -1;
                        }

                        tb.Text = character.CharacterName;
                        tb.Tag = character;
                    }
                    else
                    {
                        // if character already assigned we get textbox where is character dropped
                        TextBox _tbClear = mView.FindName("Radio_" + character.RadioNum) as TextBox;
                        // assign new radio number for dropping character
                        character.RadioNum = _numRadio;
                        // clear former textbox
                        _tbClear.Text = "";
                        _tbClear.Tag = null;
                        // assign new character
                        tb.Text = character.CharacterName;

                        if (tb.Tag != null)
                        {
                            (tb.Tag as Character).RadioNum = -1;
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

                if ((e.LeftButton == MouseButtonState.Pressed) &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
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

                if (!SessionVariables.AudioDialogsOn)
                    return;

                foreach (var _character in CharacterCollection)
                {
                    foreach (var _phrase in _character.Phrases)
                    {
                        if (!File.Exists(SessionVariables.WizardAudioDirectory
                            + _character.CharacterPrefix + "_"
                            + _phrase.FileName + ".mp3")) //Char name and prefix are being left blank...
                        {
                            var _debugMessage = "Missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr;

                            _addMessage(new WarningMessage(_debugMessage));

                            LoggerHelper.Info(SessionVariables.DialogLogFileName, "missing " + _character.CharacterPrefix + "_" + _phrase.FileName + ".mp3 " + _phrase.DialogStr);
                        }
                    }
                }
            });
            //TODO check that all dialog models have unique names
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
                                LoggerHelper.Info(SessionVariables.DialogLogFileName, " " + _phraseTag + " is not used.");
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
                                LoggerHelper.Info(SessionVariables.DialogLogFileName, " " + _dialogTag + " not used in " + dialog.Name);
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
            //try
            //{
            //    IsDialogStopped = true;

            //    EventAggregator.Instance.GetEvent<StopImmediatelyPlayingCurrentDialogLIne>().Publish();

            //    DialogLinesCollection.Clear();

            //    OnPropertyChanged("DialogLinesCollection");

            //    mCancellationTokenDialogWorkerSource.Cancel();
            //    mCancellationTokenGenerateDialogSource.Cancel();

            //    mCancellationTokenDialogWorkerSource = new CancellationTokenSource();
            //    mCancellationTokenGenerateDialogSource = new CancellationTokenSource();
            //}
            //catch (Exception ex)
            //{
            //    mcLogger.Error("Stop dialog. " + ex.Message);
            //}
        }


        // starts new dialog
        private async  void _startDialog()
        {
            //try
            //{
            //    try
            //    {
            //        // .First() will throw exception if data not found
            //        var _dialogModelInfo = DialogModelCollection.Where(x => x.State == Models.Enums.ModelDialogState.On)
            //                                                    .First();
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No allowed dialog model files. Please change settings for dialog models.");
            //        return;
            //    }

            //    IsDialogStopped = false;
            //}
            //catch (Exception ex)
            //{
            //    mcLogger.Error("Start dialog exception. "+ ex.Message);
            //}         
        }


        #endregion

        #region - Properties -

        /// <summary>
        /// Contains index of selected character 1 in ON state
        /// </summary>
        public int SelectedIndex1 { get => mSelectedIndex1; set => mSelectedIndex1 = value; }

        /// <summary>
        /// Contains index of selected character 2 in ON state
        /// </summary>
        public int SelectedIndex2 { get => mSelectedIndex2; set => mSelectedIndex2 = value; }


        /// <summary>
        /// Signal strengh received from toys 
        /// </summary>
        public int[,] HeatMapUpdate
        {
            get { return mHeatMap; }
            set
            {
                mHeatMap = value;
                OnPropertyChanged("HeatMapUpdate");
            }
        }


        /// <summary>
        /// Represents dialog started state
        /// </summary>
        public bool IsDialogStopped
        {
            get { return mIsDialogStopped; }
            set
            {
                mIsDialogStopped = value;
                OnPropertyChanged("IsDialogStopped");
            }
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


        /// <summary>
        /// Reference on View (DialogView.xaml)
        /// </summary>
        public DialogView View
        {
            get { return mView; }
            set { this.mView = value; }
        }

        /// <summary>
        /// Selected dialog model .json file
        /// </summary>
        public ModelDialogInfo SelectedDialogModel
        {
            get { return mSelectedDialogModel; }
            set
            {
                this.mSelectedDialogModel = value;
                OnPropertyChanged("SelectedDialogModel");
            }
        }

        /// <summary>
        /// Index of selected dialog model from selected dialog .json file
        /// </summary>
        public int SelectedDialogModelIndex
        {
            get
            {
                //if (SelectedDialogModel == null)
                //{
                //    return -1;
                //}

                //int result = 0;

                //// iterate over dialog model files
                //for (int i = 0; i < DialogModelCollection.Count; i++)
                //{
                //    // if we found selected file, then get its selected dialog model 
                //    if (DialogModelCollection[i].FileName.Equals(SelectedDialogModel.FileName))
                //    {
                //        return result + SelectedDialogModel.SelectedModelDialogIndex;
                //    }
                //    else  // add number of its dialog models
                //    {
                //        if (DialogModelCollection[i].State == Models.Enums.ModelDialogState.On)
                //        {
                //            result += DialogModelCollection[i].InList.Count;
                //        }
                //    }
                //}

                return -1;
            }
        }

        /// <summary>
        /// Counter for characters in "On" state
        /// </summary>
        public static int SelectedCharactersOn
        {
            get { return msSelectedCharactersOn;  }
            set { msSelectedCharactersOn = value; }
        }

        /// <summary>
        /// Collection of <see cref="Character"/>
        /// Source for characters combobox
        /// </summary>
        public ObservableCollection<Character> CharacterCollection
        {
            get
            {
                mCharacterCollection = DialogData.Instance.CharacterCollection;
                return mCharacterCollection;
            }
        }

        /// <summary>
        /// Collection of <see cref="ModelDialogInfo"/>
        /// Source for model dialogs combobox
        /// </summary>
        public ObservableCollection<ModelDialogInfo> DialogModelCollection
        {
            get
            {
                mDialogModelCollection = DialogData.Instance.DialogModelCollection;
                return mDialogModelCollection;
            }
        }

        /// <summary>
        /// Collection of <see cref="ErrorMessage"/>
        /// Source for GridView in "ErrorMessage" TabItem
        /// </summary>
        public ObservableCollection<ErrorMessage> ErrorMessagesCollection
        {
            get
            {
                if (mErrorMessagesCollection == null)
                {
                    mErrorMessagesCollection = new ObservableCollection<ErrorMessage>();
                }
                return mErrorMessagesCollection;
            }
            set
            {
                mErrorMessagesCollection = value;
                OnPropertyChanged("ErrorMessagesCollection");
            }
        }

        /// <summary>
        /// Collection of <see cref="WarningMessage"/>
        /// Source for GridView in "WarningMessage" TabItem
        /// </summary>
        public ObservableCollection<WarningMessage> WarningMessagesCollection
        {
            get
            {
                if (mWarningMessagesCollection == null)
                {
                    mWarningMessagesCollection = new ObservableCollection<WarningMessage>();
                }
                return mWarningMessagesCollection;
            }
            set
            {
                mWarningMessagesCollection = value;
                OnPropertyChanged("WarningMessagesCollection");
            }
        }

        /// <summary>
        /// Collection of <see cref="InfoMessage"/>
        /// Source for GridView in "InfoMessage" TabItem
        /// </summary>
        public ObservableCollection<InfoMessage> InfoMessagesCollection
        {
            get
            {
                if (mInfoMessagesCollection == null)
                {
                    mInfoMessagesCollection = new ObservableCollection<InfoMessage>();
                }
                return mInfoMessagesCollection;
            }
            set
            {
                mInfoMessagesCollection = value;
                OnPropertyChanged("InfoMessagesCollection");
            }
        }


        /// <summary>
        /// Radios states
        /// </summary>
        public bool[] RadiosState
        {
            get { return mRadiosState; }
        }

        #endregion
    }
}