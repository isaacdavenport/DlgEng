//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Core;
using System.Windows.Controls;
using DialogEngine.ViewModels;
using System;
using DialogEngine.Services;
using DialogEngine.Helpers;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace DialogEngine.Views
{
    /// <summary>
    /// Implementation of <see cref="PageBase"/>
    /// It can be  set as content of <see cref="Frame"/>
    /// </summary>
    public partial class DialogView : PageBase
    {

        /// <summary>
        /// Creates instance of DialogView
        /// </summary>
        public  DialogView(DateTime _dateTime)
        {
            InitializeComponent();
           
            DataContext = DialogViewModel.Instance;
        }


        protected async override void onPageLoaded()
        {
            (this.DataContext as DialogViewModel).View = this;

            await DialogDataService.LoadDialogDataAsync(SessionVariables.WizardDirectory);
        }

        private void PopupBox_Opened(object sender, System.Windows.RoutedEventArgs e)
        {
           ComboBox child = (sender as PopupBox).PopupContent as ComboBox;            
           child.IsDropDownOpen = true;
           FocusManager.SetFocusedElement(child.Parent,child);
        }
    }
}
