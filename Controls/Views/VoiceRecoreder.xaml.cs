using DialogEngine.Controls.ViewModels;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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

