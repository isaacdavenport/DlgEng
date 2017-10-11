using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
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

    public static class TextBoxUtilities
    {
        public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached("AlwaysScrollToEnd", typeof(bool), typeof(TextBoxUtilities), new PropertyMetadata(false, AlwaysScrollToEndChanged));

        private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                bool alwaysScrollToEnd = (e.NewValue != null) && (bool)e.NewValue;
                if (alwaysScrollToEnd)
                {
                    tb.ScrollToEnd();
                    tb.TextChanged += TextChanged;
                }
                else
                {
                    tb.TextChanged -= TextChanged;
                }
            }
            else
            {
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to TextBox instances.");
            }
        }

        public static bool GetAlwaysScrollToEnd(TextBox textBox)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }

            return (bool)textBox.GetValue(AlwaysScrollToEndProperty);
        }

        public static void SetAlwaysScrollToEnd(TextBox textBox, bool alwaysScrollToEnd)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }

            textBox.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);
        }

        private static void TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).ScrollToEnd();
        }
    }
    public partial class MainWindow : Window
    {

        // vb : object references to class Dialog Tracker

        public static DialogTracker TheDialogs = new DialogTracker();
        public string keyboardInput;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            //vb : to test if input is retrived properly
            ((MainWindow)Application.Current.MainWindow).TestOutput.Text += ((MainWindow)Application.Current.MainWindow).TestInput.Text;

            //vb : store input string in global variable - is this a good practice ??
            keyboardInput = ((MainWindow)Application.Current.MainWindow).TestInput.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxUtilities.SetAlwaysScrollToEnd(TestOutput, true);
            //MessageBoxResult result = MessageBox.Show(this, "Hello MessageBox"); 
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
                    ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "   you may enter two characters initials to make them talk" + Environment.NewLine; // vb : is this a good practice
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

            //vb: following part taken out of while(true) to check in input works and can be parsed

            if (SessionVars.ForceCharactersAndDialogModel)
            {
                //string[] keyboardInput = Console.ReadLine().Split(' ');

                //  vb: take user input fromtyext box instead of console.readline above
                
                string[] parsekeyboardInput = keyboardInput.Split();

                //vb : for testing what is the parsed output
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += parsekeyboardInput[0] + Environment.NewLine;
               ((MainWindow)Application.Current.MainWindow).TestOutput.Text += parsekeyboardInput[1] + Environment.NewLine;
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += parsekeyboardInput[2] + Environment.NewLine;

                //if keyboard input has three numbers for debug mode to force dialog model and characters

            }
            //((MainWindow)Application.Current.MainWindow).TestOutput.Text += "Hi Vihanga" + Environment.NewLine;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync(400);
           
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {






                    if (SessionVars.ForceCharactersAndDialogModel)
                    {
                            //string[] keyboardInput = Console.ReadLine().Split(' ');
                            string[] parsekeyboardInput = keyboardInput.Split(); //vb:can add a hard code a console.readline()

                            //if keyboard input has three numbers for debug mode to force dialog model and characters
                            if (parsekeyboardInput.Length == 3)
                        {
                            int j = 0;
                            int[] modelAndCharacters = new int[3];
                            foreach (string asciiInt in parsekeyboardInput)
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
                                TheDialogs.GenerateADialog();
                                // wrong number of user input select rand dialog and characters
                            }
                    }
                    else
                    {
                        if (!SessionVars.HeatMapOnlyMode)
                        {
                            TheDialogs.GenerateADialog();  //normal operation
                                Thread.Sleep(1100); //vb:commented out for debugging as code stops here
                                Thread.Sleep(RandomNumbers.Gen.Next(0, 2000)); //vb:commented out for debugging as code stops here
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
                            Thread.Sleep(400); //vb:commented out for debugging as code stops here
                            }
                    }



            }
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(this, "While loop completed");
        }

    }
}
