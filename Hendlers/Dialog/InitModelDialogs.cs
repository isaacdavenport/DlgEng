//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using log4net;
using Newtonsoft.Json;
using DialogEngine.Models.Logger;
using System.Collections.ObjectModel;
using DialogEngine.Models.Enums;
using System.Threading.Tasks;

namespace DialogEngine
{
    /// <summary>
    /// Initialize dialog models
    /// </summary>
    public static class InitModelDialogs 
    {
        #region - Fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public delegate void PrintMethod(object _message);

        public static PrintMethod AddItem;

        #endregion


        #region - Public methods -

        /// <summary>
        /// Loads models dialog
        /// </summary>
        /// <param name="_inObj"><see cref="DialogTracker"/></param>
        /// <param name="_arguments"> If arguments lenght > 0 then we need to reload models dialog because state is changed </param>
        public static async  void SetDefaults(DialogTracker _inObj,params object[] _arguments) //TODO is there a good way to identify orphaned tags? (dialog lines)
        {
            //Dialogs JSON parse here.


            Task task = Task.Run(() =>
            {

                int _index = 0;


                try
                {
                    var _dialogsD = new DirectoryInfo(SessionVariables.DialogsDirectory);

                    AddItem(new InfoMessage("Dialog JSON in: " + SessionVariables.DialogsDirectory));


                    if (SessionVariables.WriteSerialLog)
                        using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                        {
                            _jsonLog.WriteLine("Dialog JSON in: " + SessionVariables.DialogsDirectory);
                        }


                    var _inFiles = _dialogsD.GetFiles("*.json");



                    ObservableCollection<ModelDialogInfo> _modelDialogsState = null;

                    if (_arguments.Length > 0)
                    {
                        _modelDialogsState = (ObservableCollection<ModelDialogInfo>)_arguments[0];
                    }


                    foreach (var _file in _inFiles) //file of type FileInfo for each .json in directory
                    {

                        if (_modelDialogsState != null)
                        {
                            if (_modelDialogsState[_index].State == ModelDialogState.Off)
                            {

                                _index += 1;

                                continue;  // if modelDialog state is off, ignore this file
                            }

                        }



                        AddItem(new InfoMessage(" opening dialog models in " + _file.Name));


                        if (SessionVariables.WriteSerialLog)
                            using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                            {
                                _jsonLog.WriteLine(" opening dialog models in " + _file.Name);
                            }


                        string _inDialog;

                        try
                        {
                            var _fs = _file.OpenRead(); //open a read-only FileStream

                            using (var _reader = new StreamReader(_fs)) //creates new streamerader for fs stream. Could also construct with filename...
                            {
                                try
                                {
                                    _inDialog = _reader.ReadToEnd(); //create string of JSON file

                                    var _dialogsInClass = JsonConvert.DeserializeObject<ModelDialogInput>(_inDialog); //string to Object.


                                    foreach (var _curDialog in _dialogsInClass.InList)
                                    {
                                        //Add to dialog List
                                        _inObj.ModelDialogs.Add(_curDialog);
                                        //population sums
                                        _inObj.DialogModelPopularitySum += _curDialog.Popularity;
                                    }
                                }
                                catch (JsonReaderException _e)
                                {
                                    AddItem(new ErrorMessage("Error reading " + _file.Name));
                                    mcLogger.Error(_e.Message);
                                }
                            }


                            AddItem(new InfoMessage("Completed " + _file.Name));


                            if (SessionVariables.WriteSerialLog)
                                using (var _jsonLog = new StreamWriter(SessionVariables.LogsDirectory + SessionVariables.DialogLogFileName, true))
                                {
                                    _jsonLog.WriteLine(" completed " + _file.Name);
                                }
                        }
                        catch (UnauthorizedAccessException _e)
                        {
                            AddItem(new ErrorMessage("Unauthorized access exception while reading: " + _file.FullName));

                            mcLogger.Error(_e.Message);
                        }
                        catch (DirectoryNotFoundException __e)
                        {
                            AddItem(new ErrorMessage("Directory not found exception while reading: " + _file.FullName));
                            mcLogger.Error(__e.Message);
                        }
                    }

                    _index += 1;

                }
                catch (OutOfMemoryException _e)
                {
                    AddItem(new ErrorMessage("You probably need to restart your computer..."));

                    mcLogger.Error(_e.Message);
                }


                if (_inObj.ModelDialogs.Count < 2)
                    MessageBox.Show("Insufficient dialog models found in " + SessionVariables.DialogsDirectory + " exiting.");


            });
        }


        #endregion
    }
}