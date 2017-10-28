using System;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using log4net;


namespace DialogEngine
{
    public static class InitModelDialogs    //TODO lets get some graceful failures here. recovery from single file failures.
    {
        #region - Fields -
        private static readonly ILog mcLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public delegate void PrintMethod(string message);
        public static PrintMethod AddDialogItem = new PrintMethod(((MainWindow)Application.Current.MainWindow).CurrentPrintMethod);

        #endregion


        #region - Public methods -

        public static  void SetDefaults(DialogTracker inObj) //TODO is there a good way to identify orphaned tags? (dialog lines)
        {
            //Dialogs JSON parse here.
            try
            {
                DirectoryInfo dialogs_d = new DirectoryInfo(SessionVars.DialogsDirectory);

                AddDialogItem("Dialog JSON in: " + SessionVars.DialogsDirectory);

                if (SessionVars.WriteSerialLog)
                {
                    using (StreamWriter JSONLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                    {
                        JSONLog.WriteLine("Dialog JSON in: " + SessionVars.DialogsDirectory);
                    }
                }

                var inFiles = dialogs_d.GetFiles("*.json");

                foreach (FileInfo file in inFiles) //file of type FileInfo for each .json in directory
                {
                    AddDialogItem(" opening dialog models in " + file.Name);

                    if (SessionVars.WriteSerialLog)
                    {
                        using (StreamWriter JSONLog = new StreamWriter(
                        (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                        {
                            JSONLog.WriteLine(" opening dialog models in " + file.Name);
                        }
                    }

                    string inDialog;

                    try
                    {
                        FileStream fs = file.OpenRead();    //open a read-only FileStream
                        using (StreamReader reader = new StreamReader(fs))   //creates new streamerader for fs stream. Could also construct with filename...
                        {
                            try
                            {
                                inDialog = reader.ReadToEnd();//create string of JSON file

                                ModelDialogInput dialogsInClass = JsonConvert.DeserializeObject<ModelDialogInput>(inDialog);  //string to Object.

                                foreach (ModelDialog curDialog in dialogsInClass.InList)
                                {
                                    //Add to dialog List
                                    inObj.ModelDialogs.Add(curDialog);
                                    //population sums
                                    inObj.DialogModelPopularitySum += curDialog.Popularity;
                                }
                            }
                            catch (Newtonsoft.Json.JsonReaderException e)
                            {
                                Console.WriteLine("Error reading " + file.Name);
                                Console.WriteLine("JSON Parse error at " + e.LineNumber + ", " + e.LinePosition);
                                Console.ReadLine();
                            }
                        }

                        AddDialogItem(" completed " + file.Name);

                        if (SessionVars.WriteSerialLog)
                        {
                            using (StreamWriter JSONLog = new StreamWriter(
                            (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                            {
                                JSONLog.WriteLine(" completed " + file.Name);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Unauthorized access exception while reading: " + file.FullName);
                        Console.WriteLine("Check file and directory permissions");
                        Console.ReadLine();

                    }
                    catch (DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Directory not found exception while reading: " + file.FullName);
                        Console.WriteLine("check the Dialog JSON path in your config file");
                        Console.ReadLine();
                    }
                }
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("You probably need to restart your computer...");
                Console.ReadLine();
            }
            if (inObj.ModelDialogs.Count < 2)
            {
                AddDialogItem("  Insufficient dialog models found in " + SessionVars.DialogsDirectory + " exiting.");

                Environment.Exit(0);
            }
        }

        #endregion


    }
}
