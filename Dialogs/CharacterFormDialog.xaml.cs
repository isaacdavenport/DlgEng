using DialogEngine.Models.Dialog;
using DialogEngine.Models.Shared;
using DialogEngine.Models.Wizard;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for NewCharacterDialogControl.xaml
    /// </summary>
    public partial class CharacterFormDialog : UserControl
    {
        #region - fields -

        private Character mCharacter;

        #endregion

        #region - constructor -

        public CharacterFormDialog()
        {
            InitializeComponent();

            this.groupBox.HeaderTemplate = (DataTemplate)FindResource("newCharacterHeader");
            NumbersList = new List<int>(Enumerable.Range(1, 100));
            NumbersCbx.ItemsSource = NumbersList;
            WizardTypeCb.ItemsSource = DialogData.Instance.WizardTypesCollection;
        }


        public CharacterFormDialog(Character character)
        {
            InitializeComponent();

            this.groupBox.HeaderTemplate =(DataTemplate)FindResource("editCharacterHeader");
            mCharacter = character;
            NumbersList = new List<int>(Enumerable.Range(1, 100));
            NumbersCbx.ItemsSource = NumbersList;
            WizardTypeCb.ItemsSource = DialogData.Instance.WizardTypesCollection;
            _populateForm(character);
        }

        #endregion

        #region - event handlers -

        private void _save_Click(object sender, RoutedEventArgs e)
        {
            if(mCharacter == null)
            {
                mCharacter = new Character
                {
                    CharacterAge = NumbersList[NumbersCbx.SelectedIndex],
                    CharacterGender = (GenderCb.SelectedValue).ToString().Substring(0, 1),
                    CharacterName = CharacterNameTb.Text,
                    CharacterPrefix = CharacterPrefixTb.Text
                };
            }
            else
            {
                mCharacter.CharacterAge = NumbersList[NumbersCbx.SelectedIndex];
                mCharacter.CharacterGender = (GenderCb.SelectedValue).ToString().Substring(0, 1);
                mCharacter.CharacterName = CharacterNameTb.Text;
                mCharacter.CharacterPrefix = CharacterPrefixTb.Text;
            }

            DialogHost.CloseDialogCommand.Execute(new WizardParameter() { Character = mCharacter, WizardTypeIndex = WizardTypeCb.SelectedIndex }, sender as Button);
        }

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("mainFrame") as Frame).NavigationService.GoBack();
        }


        #endregion

        #region - private functions -

        private void _populateForm(Character character)
        {
            this.CharacterNameTb.Text = character.CharacterName;
            this.CharacterPrefixTb.Text = character.CharacterPrefix;
            this.NumbersCbx.SelectedValue = character.CharacterAge;
            this.GenderCb.SelectedIndex = character.CharacterGender.Equals("M") ? 0 : 1 ;         
        }

        #endregion

        #region - properties -

        public IList<int> NumbersList { get; }



        #endregion


    }
}
