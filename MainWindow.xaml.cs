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
using System.Threading;


namespace DialogEngine
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        
        public static DialogTracker TheDialogs = new DialogTracker();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void PlayButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Program.WriteStartupInfo();
            TheDialogs.intakeCharacters();
            InitModelDialogs.SetDefaults(TheDialogs);

            if (SessionVars.TagUsageCheck)
            {
                Program.checkTagsUsed();
            }

            if (SessionVars.DebugFlag)
            {
                Program.CheckForMissingPhrases();
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "  press enter to continue" + Environment.NewLine;
                //Console.WriteLine("  press enter to continue");

                //Console.ReadLine();

                if (!SessionVars.ForceCharactersAndDialogModel)
                {
                    ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine; // vb : is this a good practice
                    //Console.WriteLine("   you may enter two characters initials to make them talk"); 
                }
            }

            //Select Debug Output
            if (SessionVars.ForceCharactersAndDialogModel)
            {
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "   enter three numbers to set the next: DialogModel, Char1, Char2" + Environment.NewLine; // vb : is this a good practice       
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += Environment.NewLine;
                //Console.WriteLine("   enter three numbers to set the next: DialogModel, Char1, Char2");
                //Console.WriteLine();
            }

            /*while (true)
            {
                if (SessionVars.ForceCharactersAndDialogModel)
                {
                    //string[] keyboardInput = Console.ReadLine().Split(' ');
                    string[] keyboardInput = { "FS"," ","PP"}; //vb:added this to hard code a console.readline()

                    //if keyboard input has three numbers for debug mode to force dialog model and characters
                    if (keyboardInput.Length == 3)
                    {
                        int j = 0;
                        int[] modelAndCharacters = new int[3];
                        foreach (string asciiInt in keyboardInput)
                        {
                            modelAndCharacters[j] = Int32.Parse(asciiInt);
                            j++;
                        }

                        TheDialogs.GenerateADialog(modelAndCharacters);
                    }
                    else
                    {
                        ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "Incorrect input, generating random dialog." + Environment.NewLine;
                        //Console.WriteLine("Incorrect input, generating random dialog.");
                        TheDialogs.GenerateADialog(); // wrong number of user input select rand dialog and characters
                    }
                }
                else
                {
                    if (!SessionVars.HeatMapOnlyMode)
                    {
                        TheDialogs.GenerateADialog();  //normal operation
                        //Thread.Sleep(1100); //vb:commented out for debugging as code stops here
                        //Thread.Sleep(RandomNumbers.Gen.Next(0, 2000)); //vb:commented out for debugging as code stops here
                    }
                    else
                    {
                        //Console.Clear();
                        if (SessionVars.HeatMapFullMatrixDispMode)
                        {
                            FirmwareDebuggingTools.PrintHeatMap();
                        }
                        if (SessionVars.HeatMapSumsMode)
                        {
                            FirmwareDebuggingTools.PrintHeatMapSums();
                        }
                        //Thread.Sleep(400); //vb:commented out for debugging as code stops here
                    }
                }
            }

        */
        }
    }
}
