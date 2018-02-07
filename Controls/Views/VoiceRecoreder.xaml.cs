using DialogEngine.Controls.ViewModels;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.ViewModels.Dialog;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace DialogEngine.Controls.Views
{
    /// <summary>
    /// Interaction logic for VoiceRecoreder.xaml
    /// </summary>
    public partial class VoiceRecoreder : UserControl
    {

        public VoiceRecoreder()
        {
            InitializeComponent();

            this.DataContext = new VoiceRecorderViewModel(this);
        }

      
    }
}

