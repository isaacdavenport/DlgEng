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
using System.IO;


namespace DialogEngine
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        string versionTimeStr = "Dialog Engine ver 0.67 " + DateTime.Now;
        public MainWindow()
        {
            InitializeComponent();
        }
        void WriteStartupInfo()
        {
            if (SessionVars.WriteSerialLog)
            {
                //string versionTimeStr = "Dialog Engine ver 0.67 " + DateTime.Now;
                //Console.WriteLine("");
                //Console.WriteLine(versionTimeStr);
                //Console.WriteLine("");

                using (StreamWriter serialLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.HexLogFileName), true))
                {
                    serialLog.WriteLine("");
                    serialLog.WriteLine("");
                    serialLog.WriteLine(versionTimeStr);
                    serialLog.Close();
                }
                using (StreamWriter serialLogDec = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DecimalLogFileName), true))
                {
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine("");
                    serialLogDec.WriteLine(versionTimeStr);
                    serialLogDec.Close();
                }
                using (StreamWriter serialLogDialog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                {
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine("");
                    serialLogDialog.WriteLine(versionTimeStr);
                    serialLogDialog.Close();
                }
            }
        }

        private void PlayButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WriteStartupInfo();
            TestOutput.Text = versionTimeStr;
        }
    }
}
