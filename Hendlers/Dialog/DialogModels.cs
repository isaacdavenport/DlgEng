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


        public delegate void PrintMethod(string _message);
        public static PrintMethod AddDialogItem = new PrintMethod(((MainWindow)Application.Current.MainWindow).CurrentPrintMethod);

        #endregion


        #region - Public methods -

        public static  void SetDefaults(DialogTracker _inObj) //TODO is there a good way to identify orphaned tags? (dialog lines)
        {
            //Dialogs JSON parse here.
            try
            {
                DirectoryInfo _dialogsD = new DirectoryInfo(SessionVars.DialogsDirectory);

                AddDialogItem("Dialog JSON in: " + SessionVars.DialogsDirectory);

                if (SessionVars.WriteSerialLog)
                {
                    using (StreamWriter _jsonLog = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                    {
                        _jsonLog.WriteLine("Dialog JSON in: " + SessionVars.DialogsDirectory);
                    }
                }

                var _inFiles = _dialogsD.GetFiles("*.json");

                foreach (FileInfo _file in _inFiles) //file of type FileInfo for each .json in directory
                {
                    AddDialogItem(" opening dialog models in " + _file.Name);

                    if (SessionVars.WriteSerialLog)
                    {
                        using (StreamWriter _jsonLog = new StreamWriter(
                        (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                        {
                            _jsonLog.WriteLine(" opening dialog models in " + _file.Name);
                        }
                    }

                    string _inDialog;

                    try
                    {
                        FileStream _fs = _file.OpenRead();    //open a read-only FileStream
                        using (StreamReader _reader = new StreamReader(_fs))   //creates new streamerader for fs stream. Could also construct with filename...
                        {
                            try
                            {
                                _inDialog = _reader.ReadToEnd();//create string of JSON file

                                ModelDialogInput _dialogsInClass = JsonConvert.DeserializeObject<ModelDialogInput>(_inDialog);  //string to Object.

                                foreach (ModelDialog _curDialog in _dialogsInClass.InList)
                                {
                                    //Add to dialog List
                                    _inObj.ModelDialogs.Add(_curDialog);
                                    //population sums
                                    _inObj.DialogModelPopularitySum += _curDialog.Popularity;
                                }
                            }
                            catch (Newtonsoft.Json.JsonReaderException _e)
                            {
                                Console.WriteLine("Error reading " + _file.Name);
                                Console.WriteLine("JSON Parse error at " + _e.LineNumber + ", " + _e.LinePosition);
                                Console.ReadLine();
                            }
                        }

                        AddDialogItem(" completed " + _file.Name);

                        if (SessionVars.WriteSerialLog)
                        {
                            using (StreamWriter _jsonLog = new StreamWriter(
                            (SessionVars.LogsDirectory + SessionVars.DialogLogFileName), true))
                            {
                                _jsonLog.WriteLine(" completed " + _file.Name);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException _e)
                    {
                        Console.WriteLine(_e.Message);
                        Console.WriteLine("Unauthorized access exception while reading: " + _file.FullName);
                        Console.WriteLine("Check file and directory permissions");
                        Console.ReadLine();

                    }
                    catch (DirectoryNotFoundException _e)
                    {
                        Console.WriteLine(_e.Message);
                        Console.WriteLine("Directory not found exception while reading: " + _file.FullName);
                        Console.WriteLine("check the Dialog JSON path in your config file");
                        Console.ReadLine();
                    }
                }
            }
            catch (OutOfMemoryException _e)
            {
                Console.WriteLine(_e.Message);
                Console.WriteLine("You probably need to restart your computer...");
                Console.ReadLine();
            }
            if (_inObj.ModelDialogs.Count < 2)
            {
                AddDialogItem("  Insufficient dialog models found in " + SessionVars.DialogsDirectory + " exiting.");

                Environment.Exit(0);
            }
        }

        #endregion


    }
}
