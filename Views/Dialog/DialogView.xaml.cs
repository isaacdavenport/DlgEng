//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Core;
using System.Windows.Controls;
using DialogEngine.ViewModels.Dialog;

namespace DialogEngine.Views.Dialog
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
        public DialogView()
        {
            InitializeComponent();
           
            DataContext = DialogViewModel.Instance;

        }


        protected override void onPageLoaded()
        {
            
            DialogTracker.GetInstance(DialogViewModel.Instance).AddItem = new DialogTracker.PrintMethod((this.DataContext as DialogViewModel).AddItem);

            InitModelDialogs.AddItem = new InitModelDialogs.PrintMethod(((this.DataContext as DialogViewModel).AddItem));

            (this.DataContext as DialogViewModel).View = this;

            (this.DataContext as DialogViewModel).InitDialogData();
            
        }

    }
}
