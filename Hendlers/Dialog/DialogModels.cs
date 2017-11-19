using System;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using log4net;
using System.Windows.Media;

namespace DialogEngine
{

    public static class
        InitModelDialogs //TODO lets get some graceful failures here. recovery from single file failures.
    {
        #region - Fields -

        private static readonly ILog mcLogger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public delegate void PrintMethod(string _message);

        public static PrintMethod AddDialogItem;

        #endregion


        #region - Public methods -


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_inObj"></param>
        public static void
            SetDefaults(DialogTracker _inObj) //TODO is there a good way to identify orphaned tags? (dialog lines)
        {
            //Dialogs JSON parse here.
            try
            {
                DirectoryInfo _dialogsD = new DirectoryInfo(SessionVariables.DialogsDirectory);

                WriteStatusBarInfo("Dialog JSON in: " + SessionVariables.DialogsDirectory,Brushes.Black);


                if (SessionVariables.WriteSerialLog)
                {
                    using (StreamWriter _jsonLog =
                        new StreamWriter((SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName), true))
                    {
                        _jsonLog.WriteLine("Dialog JSON in: " + SessionVariables.DialogsDirectory);
                    }
                }



                var _inFiles = _dialogsD.GetFiles("*.json");

                foreach (FileInfo _file in _inFiles) //file of type FileInfo for each .json in directory
                {
                    WriteStatusBarInfo(" opening dialog models in " + _file.Name,Brushes.Black);



                    if (SessionVariables.WriteSerialLog)
                    {
                        using (StreamWriter _jsonLog =
                            new StreamWriter((SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName),
                                true))
                        {
                            _jsonLog.WriteLine(" opening dialog models in " + _file.Name);
                        }
                    }




                    string _inDialog;

                    try
                    {
                        FileStream _fs = _file.OpenRead(); //open a read-only FileStream

                        using (StreamReader _reader = new StreamReader(_fs)
                        ) //creates new streamerader for fs stream. Could also construct with filename...
                        {
                            try
                            {
                                _inDialog = _reader.ReadToEnd(); //create string of JSON file

                                ModelDialogInput _dialogsInClass =
                                    JsonConvert.DeserializeObject<ModelDialogInput>(_inDialog); //string to Object.



                                foreach (ModelDialog _curDialog in _dialogsInClass.InList)
                                {
                                    //Add to dialog List
                                    _inObj.ModelDialogs.Add(_curDialog);
                                    //population sums
                                    _inObj.DialogModelPopularitySum += _curDialog.Popularity;
                                }

                            }
                            catch (Newtonsoft.Json.JsonReaderException __e)
                            {
                                WriteStatusBarInfo("Error reading " + _file.Name,Brushes.Red);
                                mcLogger.Error(__e.Message);

                            }
                        }



                        WriteStatusBarInfo("Completed " + _file.Name,Brushes.Black);


                        if (SessionVariables.WriteSerialLog)
                        {
                            using (StreamWriter _jsonLog =
                                new StreamWriter((SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName),
                                    true))
                            {
                                _jsonLog.WriteLine(" completed " + _file.Name);
                            }
                        }


                    }
                    catch (UnauthorizedAccessException _e)
                    {
                        WriteStatusBarInfo("Unauthorized access exception while reading: " + _file.FullName,Brushes.Red);
                        mcLogger.Error(_e.Message);
                    }
                    catch (DirectoryNotFoundException __e)
                    {

                        WriteStatusBarInfo("Directory not found exception while reading: " + _file.FullName,Brushes.Red);
                        mcLogger.Error(__e.Message);
                    }
                }


            }
            catch (OutOfMemoryException _e)
            {
                WriteStatusBarInfo("You probably need to restart your computer...",Brushes.Red);
                mcLogger.Error(_e.Message);
            }




            if (_inObj.ModelDialogs.Count < 2)
            {

                MessageBox.Show("Insufficient dialog models found in " + SessionVariables.DialogsDirectory + " exiting.");

            }


        }


        public static void WriteStatusBarInfo(string _infoMessage, Brush _infoColor)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                try
                {
                    (Application.Current.MainWindow as MainWindow).WriteStatusInfo(_infoMessage, _infoColor);

                }
                catch (Exception e)
                {
                    mcLogger.Error(e.Message);
                }
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action) (() =>
                {
                    try
                    {
                        (Application.Current.MainWindow as MainWindow).WriteStatusInfo(_infoMessage, _infoColor);

                    }
                    catch (Exception e)
                    {
                        mcLogger.Error(e.Message);
                    }
                }));
            }
        }

        #endregion



    }
}

