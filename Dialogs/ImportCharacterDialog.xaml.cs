
using DialogEngine.Helpers;
using log4net;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for ImportCharacterDialog.xaml
    /// </summary>
    public partial class ImportCharacterDialog : UserControl,INotifyPropertyChanged
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private Visibility mIsWorking = Visibility.Hidden;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        public ImportCharacterDialog()
        {
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region - event handlers -

        private async void _import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog _openFileDialog = new System.Windows.Forms.OpenFileDialog();
                _openFileDialog.Filter = "Zip file(*.zip)|*.zip";

                if (_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    IsWorking = Visibility.Visible;

                    await Task.Run(() =>
                    {
                        Thread.CurrentThread.Name = "_import_Click ZipFile";

                        ZipFile.ExtractToDirectory(_openFileDialog.FileName, SessionHelper.TempDirectory);

                        _processExtractedFiles();

                        // clear data from Temp directory
                        DirectoryInfo _directoryInfo = new DirectoryInfo(SessionHelper.TempDirectory);

                        foreach (FileInfo file in _directoryInfo.GetFiles())
                        {
                            file.Delete();
                        }
                    });

                    IsWorking = Visibility.Hidden;
                }
            }
            catch (System.Exception ex)
            {
                mcLogger.Error("_import_Click " + ex.Message);
            }

            DialogHost.CloseDialogCommand.Execute(null, sender as Button);

        }

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }

        #endregion

        #region - private functions -

        private void _processExtractedFiles()
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(SessionHelper.TempDirectory);

                // process .json files
                FileInfo[] _jsonFiles = directoryInfo.GetFiles("*.json");

                foreach(FileInfo _jsonFile in _jsonFiles)
                {
                    DialogDataHelper.ProcessJSONFile(_jsonFile);
                    File.Copy(_jsonFile.FullName, SessionHelper.WizardDirectory,true);
                }

                // process .mp3 files

                FileInfo[] _mp3Files = directoryInfo.GetFiles("*.mp3");

                foreach (FileInfo _mp3File in _mp3Files)
                {
                    File.Copy(_mp3File.FullName, SessionHelper.WizardAudioDirectory,true);
                }
            }
            catch (System.Exception ex)
            {
                mcLogger.Error("_processExtractedFiles " + ex.Message);
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

        public Visibility IsWorking
        {
            get { return mIsWorking; }
            set
            {
                mIsWorking = value;
                OnPropertyChanged("IsWorking");
            }
        }
        #endregion
    }
}
