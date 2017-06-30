﻿using System;
using Newtonsoft.Json;
using System.IO;
using System.Windows;


namespace DialogEngine
{
    public static class InitModelDialogs    //TODO lets get some graceful failures here. recovery from single file failures.
    {
        public static void SetDefaults(DialogTracker inObj) //TODO is there a good way to identify orphaned tags? (dialog lines)
        {
            //Dialogs JSON parse here.
            try
            {
                DirectoryInfo dialogs_d = new DirectoryInfo(SessionVars.DialogsDirectory);
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "Dialog JSON in: " + SessionVars.DialogsDirectory + Environment.NewLine;
                //Console.WriteLine("Dialog JSON in: " + SessionVars.DialogsDirectory);
                if (SessionVars.WriteSerialLog)
                {
                    using (StreamWriter JSONLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                    {
                        JSONLog.WriteLine("Dialog JSON in: " + SessionVars.DialogsDirectory);
                    }
                }
                var _inFiles = dialogs_d.GetFiles("*.json"); 
                foreach (FileInfo file in _inFiles) //file of type FileInfo for each .json in directory
                {
                    ((MainWindow)Application.Current.MainWindow).TestOutput.Text += " opening dialog models in " + file.Name + Environment.NewLine;
                    //Console.WriteLine(" opening dialog models in " + file.Name);
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
                                foreach (ModelDialog curDialog in dialogsInClass.inList)
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
                        ((MainWindow)Application.Current.MainWindow).TestOutput.Text += " completed " + file.Name + Environment.NewLine;
                        //Console.WriteLine(" completed " + file.Name);
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
                ((MainWindow)Application.Current.MainWindow).TestOutput.Text += "  Insufficient dialog models found in " + SessionVars.DialogsDirectory + " exiting.";
                //Console.WriteLine("  Insufficient dialog models found in " + SessionVars.DialogsDirectory + " exiting.");
                //Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
