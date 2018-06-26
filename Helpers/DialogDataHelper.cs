
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Logger;
using DialogEngine.Models.Shared;
using DialogEngine.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace DialogEngine.Helpers
{
    public static class DialogDataHelper
    {
        #region - fields-

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region - private functions -

        private static void _processMessage(LogMessage message)
        {
            try
            {
                if (message is InfoMessage)
                {
                    DialogData.Instance.InfoMessagesCollection.Insert(0, (InfoMessage)message);
                    int _length = DialogData.Instance.InfoMessagesCollection.Count;

                    if (_length > 300 && _length > 0)
                    {
                        DialogData.Instance.InfoMessagesCollection.RemoveAt(_length - 1);
                    }
                }
                else if (message is WarningMessage)
                {
                    DialogData.Instance.WarningMessagesCollection.Insert(0, (WarningMessage)message);
                    int _length = DialogData.Instance.WarningMessagesCollection.Count;

                    if (_length > 300 && _length > 0)
                    {
                        DialogData.Instance.WarningMessagesCollection.RemoveAt(_length - 1);
                    }
                }
                else
                {
                    DialogData.Instance.ErrorMessagesCollection.Insert(0, (ErrorMessage)message);
                    int _length = DialogData.Instance.ErrorMessagesCollection.Count;

                    if (_length > 300 && _length > 0)
                    {
                        DialogData.Instance.ErrorMessagesCollection.RemoveAt(_length - 1);
                    }
                }
            }
            catch (Exception e)
            {
                mcLogger.Error("AddMessage " + e.Message);
            }
        }

        #endregion

        #region - public functions -

        public static  Task LoadDialogDataAsync(string path)
        {
            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "LoadDialogDataAsync";

                try
                {
                    var _directoryInfo = new DirectoryInfo(path);
                    FileInfo[] _fileInfo = _directoryInfo.GetFiles("*.json");
                    string _jsonString;

                    DialogData.Instance.CharacterCollection = new ObservableCollection<Character>();
                    DialogData.Instance.DialogModelCollection = new ObservableCollection<ModelDialogInfo>();
                    DialogData.Instance.WizardsCollection = new ObservableCollection<Wizard>();

                    for (int i=0;i< _fileInfo.Length; i++)
                    {
                        var _fileSteam = _fileInfo[i].OpenRead(); //open a read-only FileStream
                        using (var reader = new StreamReader(_fileSteam)) //creates new streamerader for fs stream. Could also construct with filename...
                        {
                            try
                            {
                                _jsonString = reader.ReadToEnd();
                                //json string to Object.
                                JSONObjectsTypesList _jsonObjectsTypesList = JsonConvert.DeserializeObject
                                    <JSONObjectsTypesList>(_jsonString);

                                //This assumes all characters/wizards in array read and are formatted correctly or the entire json read fails
                                // otherwise array member numbers will be off by one if we decide not to read one due to a parse error
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    int j = 0;
                                    if (_jsonObjectsTypesList.Characters != null)
                                    {
                                        foreach (var _character in _jsonObjectsTypesList.Characters)
                                        {
                                            _character.FileName = _fileInfo[i].Name;
                                            _character.JsonArrayIndex = j;
                                            j++;
                                            DialogData.Instance.CharacterCollection.Add(_character);
                                        }
                                    }
                                    
                                    j = 0;
                                    if (_jsonObjectsTypesList.Wizards != null)
                                    {
                                        foreach (var _wizard in _jsonObjectsTypesList.Wizards)
                                        {
                                            _wizard.FileName = _fileInfo[i].Name;
                                            _wizard.JsonArrayIndex = j;
                                            j++;
                                            DialogData.Instance.WizardsCollection.Add(_wizard);
                                        }
                                    }

                                    j = 0;
                                    if (_jsonObjectsTypesList.DialogModels != null)
                                    {
                                        foreach (var _dialogModel in _jsonObjectsTypesList.DialogModels)
                                        {
                                            _dialogModel.FileName = _fileInfo[i].Name;
                                            _dialogModel.JsonArrayIndex = j;
                                            j++;
                                            DialogData.Instance.DialogModelCollection.Add(_dialogModel);
                                        }
                                    }
                                });
                            }
                            catch (JsonReaderException e)
                            {
                                string _errorReadingMessage = "Error reading " + _fileInfo[i].Name;
                                DialogDataHelper.AddMessage(new ErrorMessage(_errorReadingMessage));

                                string _jsonParseErrorMessage = "JSON Parse error at " + e.LineNumber + ", " + e.LinePosition;
                                DialogDataHelper.AddMessage(new ErrorMessage(_jsonParseErrorMessage));
                            }
                            catch (Exception ex)
                            {
                                mcLogger.Error("Error during parsing json file " + ex.Message);
                            }
                        }
                    }
                    EventAggregator.Instance.GetEvent<DialogDataLoadedEvent>().Publish();
                }
                catch (UnauthorizedAccessException e)
                {
                    mcLogger.Error(e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    mcLogger.Error(e.Message);
                }
                catch (OutOfMemoryException e)
                {
                    mcLogger.Error(e.Message);
                }
            });
        }

        public static  Task SerializeDataToFile()
        {   //TODO run this after wizard completes not on application close, only run for changed character
            return Task.Run(() =>
            {
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        Error = (sender, args) =>
                        {
                            mcLogger.Error(args.ErrorContext.Error.Message);
                        },
                        Formatting = Formatting.Indented
                    };

                    for (int i = DialogData.Instance.CharacterCollection.Count - 1; i >= 0; i--)
                    {
                        // The .FileName should never be empty now, all characters, wizards, modelDialogs must be read
                          // from some file.
                        if (!string.IsNullOrEmpty(DialogData.Instance.CharacterCollection[i].FileName))
                        {
                            string _jsonLocal = JsonConvert.SerializeObject(DialogData.Instance.CharacterCollection[i], settings);
                            //TODO we are rewriting everything, even if it didn't change, we should write on wizard completion only
                              // not write everything at application close
                              // this may not be the only character in the file's character array, don't want to wipe out those
                              // other characters or any wizards or modelDialogs
                            File.WriteAllText(Path.Combine(SessionHelper.WizardDirectory, 
                                DialogData.Instance.CharacterCollection[i].FileName), _jsonLocal);

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                DialogData.Instance.CharacterCollection.RemoveAt(i);
                            });
                        }
                        mcLogger.Error("Character " + DialogData.Instance.CharacterCollection[i].CharacterName +
                             " has no associated filename from which it was read for saving back any changes.");
                    }
                }
                catch (JsonSerializationException ex)
                {
                    mcLogger.Error(ex.Message);
                }
            });
        }

        private static void Serializer_Error(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            mcLogger.Error(e.ErrorContext.Error.Message);
        }

        private static void  error(object sender, ErrorEventArgs args)
        {

        }

        public static  void AddMessage(LogMessage message)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                _processMessage(message);
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action) (() =>
                {
                    _processMessage(message);
                }));
            }
        }


        #endregion
    }
}
