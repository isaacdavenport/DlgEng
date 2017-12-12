//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DialogEngine.ViewModels.Dialog;

namespace DialogEngine.Views.Dialog
{
    /// <summary>
    /// Implementation of <see cref="PageBase"/>
    /// It can be  set as content of <see cref="Frame"/>
    /// </summary>
    public partial class DialogView : PageBase
    {

        public DialogView()
        {
            InitializeComponent();
           
            DataContext = new DialogViewModel(this);

        }

        protected override void OnPageLoaded()
        {
            //DialogTracker.Instance.AddItem = new DialogTracker.PrintMethod((this.DataContext as DialogViewModel).AddItem);

            //InitModelDialogs.AddItem = new InitModelDialogs.PrintMethod(((this.DataContext as DialogViewModel).AddItem));

            FirmwareDebuggingTools.AddItem = new FirmwareDebuggingTools.PrintMethod(((this.DataContext as DialogViewModel).AddItem));
            
        }

    }
}
