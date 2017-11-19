using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialogEngine.xaml
    /// </summary>
    public partial class AboutDialogEngine : Window
    {
        public AboutDialogEngine()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object _sender, RequestNavigateEventArgs _e)
        {
            Process.Start(_e.Uri.ToString());
        }
    }
}
