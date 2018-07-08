using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using log4net;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for EditWithJSONEditorDialog.xaml
    /// </summary>
    public partial class EditWithJSONEditorDialog : UserControl,INotifyPropertyChanged
    {
        #region - fields -
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string mcJsonEditorExeName = "JSONedit.exe";
        private Character mSelectedCharacter;
        private Visibility mIsWorking = Visibility.Hidden;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        public EditWithJSONEditorDialog()
        {
            DataContext = this;
            InitializeComponent();

            CharactersCbx.SelectedIndex = Characters.Count > 0 ? 0 : -1;
        }


        public EditWithJSONEditorDialog(Character character)
        {
            DataContext = this;
            InitializeComponent();
            SelectedCharacter = character;
            CharactersCbx.SelectedIndex = Characters.ToList()
                                        .FindIndex(item => item.CharacterPrefix.Equals(SelectedCharacter.CharacterPrefix));
        }

        #endregion

        #region - event handlers -

        private async void _editWithJSONEditor_Click(object sender, RoutedEventArgs e)
        {
            IsWorking = Visibility.Visible;

            await Task.Run(() =>
            {
                Process.Start(Path.Combine(SessionHelper.TutorialDirectory, mcJsonEditorExeName),
                    Path.Combine(SessionHelper.WizardDirectory, SelectedCharacter.FileName));
            });

            IsWorking = Visibility.Hidden;
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }


        private void _charactersCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedCharacter = e.AddedItems[0] as Character;
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
