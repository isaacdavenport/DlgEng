//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Core;
using System.Windows.Controls;
using DialogEngine.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using System.Windows;

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
        public  DialogView()
        {
            DataContext = new DialogViewModel(this);

            InitializeComponent();
        }


        protected  override void onPageLoaded()
        {
            (this.DataContext as DialogViewModel).View = this;
        }

        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {
           ComboBox child = (sender as PopupBox).PopupContent as ComboBox;            
           child.IsDropDownOpen = true;
           FocusManager.SetFocusedElement(child.Parent,child);
        }
    }
}
