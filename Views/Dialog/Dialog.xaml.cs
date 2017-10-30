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
    public partial class Dialog : PageBase
    {
        public Dialog()
        {
            InitializeComponent();
           
            DataContext = new DialogViewModel(this);

        }

        protected override void OnPageLoaded()
        {
            (this.DataContext as DialogViewModel).TheDialogs=DialogTracker.Instance;
            ;
            (this.DataContext as DialogViewModel).OnViewModelLoaded();
        }

    }
}
