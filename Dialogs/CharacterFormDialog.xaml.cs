using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using DialogEngine.Models.Wizard;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for NewCharacterDialogControl.xaml
    /// </summary>
    public partial class CharacterFormDialog : UserControl, INotifyPropertyChanged
    {
        #region - fields -

        private Character mCharacter;
        private List<int> mNumbersList;
        private ObservableCollection<WizardType> mWizardsList;
        private List<string> mGenderList;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region - constructor -

        public CharacterFormDialog()
        {
            DataContext = this;
            InitializeComponent();

            this.groupBox.HeaderTemplate = (DataTemplate)FindResource("newCharacterHeader");
            Character = new Character();
            _initData();
        }


        public CharacterFormDialog(Character character)
        {
            DataContext = this;
            InitializeComponent();

            this.groupBox.HeaderTemplate =(DataTemplate)FindResource("editCharacterHeader");
            Character = character;
            _initData();

            CharacterPrefixTb.IsEnabled = false;
        }

        #endregion

        #region - event handlers -

        private void _save_Click(object sender, RoutedEventArgs e)
        {
            NumbersCbx.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
            CharacterNameTb.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            CharacterPrefixTb.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if ( Validation.GetHasError(NumbersCbx)
              || Validation.GetHasError(CharacterNameTb)
              || Validation.GetHasError(CharacterPrefixTb))
                return;

            WizardParameter parameter = new WizardParameter
            {
                Character = mCharacter,
                WizardTypeIndex = WizardTypeCb.SelectedIndex
            };

            DialogHost.CloseDialogCommand.Execute(parameter, sender as Button);
        }

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }


        #endregion

        #region - private functions -

        private void _initData()
        {
            List<string> _genderList = new List<string>();
            _genderList.Add("Male");
            _genderList.Add("Female");

            NumbersList = new List<int>(Enumerable.Range(1, 100));
            WizardsList = DialogData.Instance.WizardTypesCollection;
            GenderList = _genderList;
        }

        #endregion

        #region - public functions -

        /// <summary>
        /// Notifies of property changed.
        /// </summary>
        /// <param name="_propertyName">Name of changed property.</param>
        public virtual void OnPropertyChanged(string _propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }

        #endregion

        #region - properties -


        public ObservableCollection<WizardType> WizardsList
        {
            get { return mWizardsList; }
            set
            {
                mWizardsList = value;
                OnPropertyChanged("WizardsList");
            }
        }


        public List<string> GenderList
        {
            get { return mGenderList; }
            set
            {
                mGenderList = value;
                OnPropertyChanged("GenderList");
            }
        }


        public List<int> NumbersList
        {
            get { return mNumbersList; }
            set
            {
                mNumbersList = value;
                OnPropertyChanged("NumbersList");
            }
        }


        public Character Character
        {
            get { return mCharacter; }
            set
            {
                mCharacter = value;
                OnPropertyChanged("Character");
            }
        }

        #endregion
    }
}
