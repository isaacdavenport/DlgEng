using DialogEngine.Core;
using System.Windows;

namespace DialogEngine.Views
{
    /// <summary>
    /// Interaction logic for CreateCharacter.xaml
    /// </summary>
    public partial class WizardView : PageBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WizardView()
        {
            InitializeComponent();
        }

        protected async override void onPageLoaded()
        {
            MediaGrid.Width = (LeftGrid.ActualHeight - TagTb.ActualHeight - 40 - 45) * 4/3;

            LeftGrid.Margin= new Thickness((WizardMainGrid.ColumnDefinitions[2].ActualWidth - MediaGrid.Width)/2,10.0,0.0,0.0);
        }

        private void WizardView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MediaGrid.Width = (LeftGrid.ActualHeight - TagTb.ActualHeight - 40 - 45) * 4/3;

            LeftGrid.Margin = new Thickness((WizardMainGrid.ColumnDefinitions[2].ActualWidth - MediaGrid.Width) / 2, 10.0, 0.0, 0.0);
        }
    }
}
