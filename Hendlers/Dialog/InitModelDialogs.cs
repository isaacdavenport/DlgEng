﻿//  Confidential Source Code Property Toys2Life LLC Colorado 2017
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
using System.Collections.Generic;
using DialogEngine.ViewModels.Dialog;
using System.Linq;

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
        /// Loads models dialogs
        /// </summary>
        /// <param name="_inObj"><see cref="DialogTracker"/></param>
        public static async  Task SetDefaultsAsync(DialogTracker _inObj) //TODO is there a good way to identify orphaned tags? (dialog lines)
        {

        //Dialogs JSON parse here.
        List<ModelDialogInfo> _modelDialogsInfo = new List<ModelDialogInfo>();

        List<ModelDialog> _modelDialogs = new List<ModelDialog>();

        double _dialogModelPopularitySum = 0.0;

        await Task.Run(() =>
            {

                try
                {
                    var _dialogsD = new DirectoryInfo(SessionVariables.DialogsDirectory);

                    AddItem(new InfoMessage("Dialog JSON in: " + SessionVariables.DialogsDirectory));


                    if (SessionVariables.WriteSerialLog)
                        LoggerHelper.Info("LogDialog","Dialog JSON in: " + SessionVariables.DialogsDirectory);



                    var _inFiles = _dialogsD.GetFiles("*.json");


                    int _filesLenght = _inFiles.Count();

                    for (int i = 0; i < _filesLenght; i++) //file of type FileInfo for each .json in directory
                    {

                        AddItem(new InfoMessage("Opening dialog models in " + _inFiles[i].Name));


                        if (SessionVariables.WriteSerialLog)
                            LoggerHelper.Info("LogDialog","Opening dialog models in " + _inFiles[i].Name);



                        string _inDialog;

                        try
                        {
                            var fs = _inFiles[i].OpenRead(); //open a read-only FileStream

                            //creates new streamerader for fs stream. Could also construct with filename...
                            using (var _reader = new StreamReader(fs))
                            {
                                try
                                {
                                    _inDialog = _reader.ReadToEnd(); //create string of JSON file

                                    string _fileName = Path.GetFileNameWithoutExtension(_inFiles[i].Name);

                                    var _dialogsInClass = JsonConvert.DeserializeObject<ModelDialogInfo>(_inDialog); //string to Object.

                                    _dialogsInClass.FileName = _fileName;

                                    _dialogModelPopularitySum += _dialogsInClass.InList.Sum(_modelDialogItem => _modelDialogItem.Popularity);

                                    _modelDialogsInfo.Add(_dialogsInClass);

                                    // add dialog models to DialogTracker.cs
                                    foreach(ModelDialog _dialog in _dialogsInClass.InList)
                                    {
                                        _dialog.FileName = _fileName;
                                        _modelDialogs.Add(_dialog);
                                    }

                                }
                                catch (JsonReaderException e)
                                {
                                    AddItem(new ErrorMessage("Error reading " + _inFiles[i].Name));
                                    mcLogger.Error(e.Message);
                                }
                            }


                            AddItem(new InfoMessage("Completed " + _inFiles[i].Name));


                            if (SessionVariables.WriteSerialLog)
                                LoggerHelper.Info("LogDialog","Completed " + _inFiles[i].Name);


                        }
                        catch (UnauthorizedAccessException e)
                        {
                            AddItem(new ErrorMessage("Unauthorized access exception while reading: " + _inFiles[i].FullName));

                            mcLogger.Error(e.Message);
                        }
                        catch (DirectoryNotFoundException e)
                        {
                            AddItem(new ErrorMessage("Directory not found exception while reading: " + _inFiles[i].FullName));
                            mcLogger.Error(e.Message);
                        }
                    }


                }
                catch (OutOfMemoryException e)
                {
                    AddItem(new ErrorMessage("You probably need to restart your computer..."));

                    mcLogger.Error(e.Message);

                    MessageBox.Show("You probably need to restart your computer...");
                }

                DialogViewModel.Instance.DialogModelCollection = new  ObservableCollection<ModelDialogInfo>(_modelDialogsInfo);

                _inObj.DialogModelPopularitySum = _dialogModelPopularitySum;

                _inObj.ModelDialogs = _modelDialogs;


                if (_inObj.ModelDialogs.Count < 2)
                    MessageBox.Show("Insufficient dialog models found in " + SessionVariables.DialogsDirectory + " exiting.");


            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dialogTracker"></param>
        /// <returns></returns>
        public static async Task RefreshDialogModelsAsync(DialogTracker _dialogTracker)
        {
            await Task.Run(() =>
            {

                int _historicalDialogsSize = DialogTracker.Instance.HistoricalDialogs.Count;

                List <string> _selectedDialogFiles = DialogViewModel.Instance.DialogModelCollection.Where(dialog => dialog.State == ModelDialogState.On)
                                                                                                   .Select(dialog => dialog.FileName)
                                                                                                   .ToList();


                for(int i = _historicalDialogsSize-1; i >= 0; i--)
                {
                    string _dialogFileName = DialogTracker.Instance.ModelDialogs[DialogTracker.Instance.HistoricalDialogs[i].DialogIndex].FileName;

                    if (!_selectedDialogFiles.Contains(_dialogFileName))
                    {
                        DialogTracker.Instance.HistoricalDialogs.RemoveAt(i);
                    }
                }


                List<ModelDialog> _modelDialogsList = new List<ModelDialog>();


                foreach(ModelDialogInfo _dialogInfo in DialogViewModel.Instance.DialogModelCollection)
                {
                    if(_dialogInfo.State == ModelDialogState.On)
                    {
                        foreach(ModelDialog _dialog in _dialogInfo.InList)
                        {
                            // query will return null if no data found
                            HistoricalDialog _historicalDialog = DialogTracker.Instance.HistoricalDialogs.Where(historicalDialog => historicalDialog.DialogName.Equals(_dialog.Name))
                                                                                                         .FirstOrDefault();
                            if(_historicalDialog != null)
                            {
                                _historicalDialog.DialogIndex = _modelDialogsList.Count;
                            }

                            _modelDialogsList.Add(_dialog);
                        }
                    }
                }


                _dialogTracker.ModelDialogs = _modelDialogsList;

                _dialogTracker.DialogModelPopularitySum = _modelDialogsList.Sum(_modelDialogItem => _modelDialogItem.Popularity);

            });
        } 

        #endregion
    }
}