using DialogEngine.Core;
using DialogEngine.Models.Dialog;
using DialogEngine.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DialogEngine.Views
{
    /// <summary>
    /// Interaction logic for CreateCharacter.xaml
    /// </summary>
    public partial class WizardView : PageBase
    {

        #region - constructor -
        /// <summary>
        /// Constructor which is invoked when we want to add new character
        /// </summary>
        public WizardView()
        {
            InitializeComponent();

            DataContext = new WizardViewModel(this);
        }

        /// <summary>
        /// Constructor which is invoked when we want to edit character
        /// </summary>
        /// <param name="character">Reference on character which we want to edit</param>
        public WizardView(Character character)
        {
            InitializeComponent();

            DataContext = new WizardViewModel(this, character);
        }

        #endregion

        #region - event handlers -

        private void WizardView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MediaGrid.Width = (LeftGrid.ActualHeight - dialogStrTb.ActualHeight - 40 - 45) * 4 / 3;

            LeftGrid.Margin = new Thickness((WizardMainGrid.ColumnDefinitions[2].ActualWidth - MediaGrid.Width) / 2, 10.0, 0.0, 0.0);
        }

        #endregion

        #region - overriding functions - 

        protected  override void onPageLoaded()
        {
            MediaGrid.Width = (LeftGrid.ActualHeight - dialogStrTb.ActualHeight - 40 - 45) * 4/3;

            LeftGrid.Margin= new Thickness((WizardMainGrid.ColumnDefinitions[2].ActualWidth - MediaGrid.Width)/2,10.0,0.0,0.0);
        }

        protected override void onPageUnloaded()
        {
            (this.DataContext as WizardViewModel).Reset();
        }

        #endregion

    }
}
