
using System.Windows.Controls;


namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for YesNoDialog.xaml
    /// </summary>
    public partial class YesNoDialog : UserControl
    {
        #region - constructor -

        public YesNoDialog(string _dialogName,string _questionMessage)
        {
            InitializeComponent();

            this.groupBox.Header = _dialogName;
            this.genericTextTb.Text = _questionMessage;
        }

        public YesNoDialog(string _dialogName, string _questionMessage, string _yesBtnContent, string _noBtnContent)
        {
            InitializeComponent();

            this.groupBox.Header = _dialogName;
            this.genericTextTb.Text = _questionMessage;
            this.noBtn.Content = _noBtnContent;
            this.yesBtn.Content = _yesBtnContent;
        }

        #endregion
    }
}
