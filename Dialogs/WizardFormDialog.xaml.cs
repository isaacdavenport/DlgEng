
using DialogEngine.Models;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;


namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for WizardFormDialog.xaml
    /// </summary>
    public partial class WizardFormDialog : UserControl, INotifyPropertyChanged
    {
        #region - fields -

        private Character mSelectedCharacter;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        public WizardFormDialog()
        {
            DataContext = this;
            InitializeComponent();

            CharactersCbx.SelectedIndex = Characters.Count > 0 ? 0 :-1 ;
            WizardTypesCbx.SelectedIndex = WizardTypes.Count > 0 ? 0 : -1;
        }

        public WizardFormDialog(Character character)
        {
            DataContext = this;
            InitializeComponent();
            SelectedCharacter = character;
            CharactersCbx.SelectedIndex = Characters.ToList().FindIndex(item => item.CharacterPrefix.Equals(SelectedCharacter.CharacterPrefix));
            WizardTypesCbx.SelectedIndex = WizardTypes.Count > 0 ? 0 : -1;
        }

        #endregion

        #region - event handlers -

        private void _start_Click(object sender, RoutedEventArgs e)
        {
            if (WizardTypesCbx.SelectedIndex == -1 || CharactersCbx.SelectedIndex == -1)
                return;


            WizardParameter parameter = new WizardParameter
            {
                Character = Characters[CharactersCbx.SelectedIndex],
                WizardTypeIndex = WizardTypesCbx.SelectedIndex
            };

            DialogHost.CloseDialogCommand.Execute(parameter, sender as Button);
        }

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }

        #endregion

        #region - private functions -

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

        public ObservableCollection<Character> Characters { get { return DialogData.Instance.CharacterCollection; }}

        public ObservableCollection<Wizard> WizardTypes { get { return DialogData.Instance.WizardsCollection; } }

        #endregion
    }
}
