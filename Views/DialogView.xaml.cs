//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Core;
using System.Windows.Controls;
using DialogEngine.ViewModels;
using System;

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
        public  DialogView(DateTime dateTime)
        {
            InitializeComponent();
           
            DataContext = DialogViewModel.Instance;
        }


        protected async override void onPageLoaded()
        {
                DialogTracker.GetInstance(DialogViewModel.Instance).AddItem = new DialogTracker.PrintMethod((this.DataContext as DialogViewModel).AddItem);

                InitModelDialogs.AddItem = new InitModelDialogs.PrintMethod(((this.DataContext as DialogViewModel).AddItem));

                (this.DataContext as DialogViewModel).View = this;

                await (this.DataContext as DialogViewModel).InitDialogDataAsync();
        }

    }
}
