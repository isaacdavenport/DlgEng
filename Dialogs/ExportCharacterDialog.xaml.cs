using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using log4net;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for ExportCharacterDialog.xaml
    /// </summary>
    public partial class ExportCharacterDialog : UserControl, INotifyPropertyChanged
    {
        #region - fields -
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Character mSelectedCharacter;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        public ExportCharacterDialog()
        {
            DataContext = this;
            InitializeComponent();

            CharactersCbx.SelectedIndex = Characters.Count > 0 ? 0 : -1;

        }

        public ExportCharacterDialog(Character character)
        {
            DataContext = this;
            InitializeComponent();
            SelectedCharacter = character;
            CharactersCbx.SelectedIndex = Characters.ToList()
                                                    .FindIndex(item => item.CharacterPrefix.Equals(SelectedCharacter.CharacterPrefix));
        }

        #endregion

        #region - event handlers -

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            _generateZIPFile(SelectedCharacter);

            // clear temp directory
            DirectoryInfo _directoryInfo = new DirectoryInfo(SessionHelper.TempDirectory);

            foreach (FileInfo file in _directoryInfo.GetFiles())
            {
                file.Delete();
            }

            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }

        private void _charactersCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                SelectedCharacter = e.AddedItems[0] as Character;
            }
        }

        #endregion

        #region - private functions -

        private void _generateZIPFile(Character _selectedCharacter)
        {
            try
            {
                string _fileName = _selectedCharacter.FileName;
                string _fileAbsolutePath = Path.Combine(SessionHelper.WizardDirectory, _fileName);

                // copy file to Temp directory
                File.Copy(_fileAbsolutePath,Path.Combine(SessionHelper.TempDirectory,_fileName),true);

                foreach(PhraseEntry phrase in _selectedCharacter.Phrases)
                {
                    string _phraseFileName = SelectedCharacter.CharacterPrefix + "_" + phrase.FileName + ".mp3";
                    string _phraseFileAbsolutePath = Path.Combine(SessionHelper.WizardAudioDirectory, _phraseFileName);

                    if (File.Exists(_phraseFileAbsolutePath))
                    {
                        File.Copy(_phraseFileAbsolutePath,Path.Combine(SessionHelper.TempDirectory, _phraseFileName),true);
                    }
                }

                System.Windows.Forms.SaveFileDialog _saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                _saveFileDialog.Filter = "Zip file(*.zip)|*.zip";
                _saveFileDialog.FileName = Path.GetFileNameWithoutExtension(SelectedCharacter.FileName);

                if(_saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ZipFile.CreateFromDirectory(SessionHelper.TempDirectory, _saveFileDialog.FileName);
                }
            }
            catch (System.Exception ex)
            {
                mcLogger.Error("_generateZIPFile " + ex.Message);
            }
        }

        #endregion

        #region - public functions -

        public virtual void OnPropertyChanged(string _propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }

        #endregion

        #region - properties -

        public Character SelectedCharacter
        {
            get { return mSelectedCharacter; }
            set
            {
                mSelectedCharacter = value;
                OnPropertyChanged("SelectedCharacter");
            }
        }

        public ObservableCollection<Character> Characters
        {
            get { return DialogData.Instance.CharacterCollection; }
        }

        #endregion


    }
}
