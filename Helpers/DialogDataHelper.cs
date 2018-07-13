
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
using System.Windows;
using System.Linq;

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



        private static void _processJSONData(JSONObjectsTypesList _jsonObjectsTypesList,string _fileName)
        {
            try
            {
                int j = 0;
                if (_jsonObjectsTypesList.Characters != null)
                {
                    foreach (var _character in _jsonObjectsTypesList.Characters)
                    {
                        _character.FileName = _fileName;
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
                        _wizard.FileName = _fileName;
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
                        _dialogModel.FileName = _fileName;
                        _dialogModel.JsonArrayIndex = j;
                        j++;
                        DialogData.Instance.DialogModelCollection.Add(_dialogModel);
                    }
                }
            }
            catch (Exception ex)
            {
                mcLogger.Error(ex.Message);
            }
        }

        private static JSONObjectsTypesList _findDataForFile(string _fileName)
        {
            try
            {
                ObservableCollection<Wizard> wizards = new ObservableCollection<Wizard>(DialogData.Instance.WizardsCollection.Where(w => w.FileName.Equals(_fileName))
                                                             .Select(w => w)
                                                             .OrderBy(w => w.JsonArrayIndex)
                                                             .ToList());

                ObservableCollection<ModelDialogInfo> _dialogModels = new ObservableCollection<ModelDialogInfo>(DialogData.Instance.DialogModelCollection.Where(dm => dm.FileName.Equals(_fileName))
                                                                         .Select(dm => dm)
                                                                         .OrderBy(dm => dm.JsonArrayIndex)
                                                                         .ToList());

                ObservableCollection<Character> characters = new ObservableCollection<Character>(DialogData.Instance.CharacterCollection.Where(c => c.FileName.Equals(_fileName))
                                                                     .Select(c => c)
                                                                     .OrderBy(c => c.JsonArrayIndex)
                                                                     .ToList());

                JSONObjectsTypesList _jsonObjectsTypesList = new JSONObjectsTypesList
                {
                    Wizards = wizards,
                    DialogModels = _dialogModels,
                    Characters = characters
                };

                return _jsonObjectsTypesList;
            }
            catch(Exception ex)
            {
                mcLogger.Error("_findDataForFile " + ex.Message);
                return null;
            }
        }

        #endregion

        #region - public functions -

        public static void ProcessJSONFile(FileInfo _fileInfo)
        {
            var _fileSteam = _fileInfo.OpenRead(); //open a read-only FileStream
            using (var reader = new StreamReader(_fileSteam)) //creates new streamerader for fs stream. Could also construct with filename...
            {
                try
                {
                    string _jsonString = reader.ReadToEnd();
                    //json string to Object.
                    JSONObjectsTypesList _jsonObjectsTypesList = JsonConvert.DeserializeObject
                        <JSONObjectsTypesList>(_jsonString);

                    if (_jsonObjectsTypesList != null)
                    {
                        //This assumes all characters/wizards in array read and are formatted correctly or the entire json read fails
                        // otherwise array member numbers will be off by one if we decide not to read one due to a parse error
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _processJSONData(_jsonObjectsTypesList, _fileInfo.Name);
                        });
                    }
                }
                catch (JsonReaderException e)
                {
                    string _errorReadingMessage = "Error reading " + _fileInfo.Name;
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


        public static  Task LoadDialogDataAsync(string path)
        {
            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "LoadDialogDataAsync";

                try
                {
                    var _directoryInfo = new DirectoryInfo(path);
                    FileInfo[] _fileInfo = _directoryInfo.GetFiles("*.json");

                    DialogData.Instance.CharacterCollection = new ObservableCollection<Character>();
                    DialogData.Instance.DialogModelCollection = new ObservableCollection<ModelDialogInfo>();
                    DialogData.Instance.WizardsCollection = new ObservableCollection<Wizard>();

                    for (int i=0;i< _fileInfo.Length; i++)
                    {
                        ProcessJSONFile(_fileInfo[i]);
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

        public static Task SerializeCharacterToFile(Character character)
        {
            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "SerializeCharacterToFile";

                try
                {
                    string _fileName = character.CharacterName.Replace(" ", string.Empty) + ".json";

                    character.FileName = _fileName;

                    JSONObjectsTypesList _jsonObjectsTypesList = _findDataForFile(_fileName);

                    var settings = new JsonSerializerSettings
                    {
                        Error = (sender, args) =>
                        {
                            mcLogger.Error(args.ErrorContext.Error.Message);
                        },
                        Formatting = Formatting.Indented
                    };

                    string _jsonLocal = JsonConvert.SerializeObject(_jsonObjectsTypesList, settings);

                    if (File.Exists(Path.Combine(SessionHelper.WizardDirectory,_fileName)))
                    {
                        File.WriteAllText(Path.Combine(SessionHelper.WizardDirectory, _fileName), _jsonLocal);
                    }
                    else
                    {
                        File.Create(Path.Combine(SessionHelper.WizardDirectory, _fileName)).Close();
                        File.WriteAllText(Path.Combine(SessionHelper.WizardDirectory, _fileName), _jsonLocal);
                    }
                }
                catch (Exception ex)
                {
                    mcLogger.Error("SerializeCharacterToFile " + ex.Message);
                }
            });
        }

        public static  Task SerializeDataToFile(string path)
        {   //TODO run this after wizard completes not on application close, only run for changed character
            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "SerializeDataToFile";

                var settings = new JsonSerializerSettings
                {
                    Error = (sender, args) =>
                    {
                        mcLogger.Error(args.ErrorContext.Error.Message);
                    },
                    Formatting = Formatting.Indented
                };

                var _directoryInfo = new DirectoryInfo(path);
                FileInfo[] _fileInfo = _directoryInfo.GetFiles("*.json");

                foreach (FileInfo file in _fileInfo)
                {
                    try
                    {
                        JSONObjectsTypesList _jsonObjectsTypesList = _findDataForFile(file.Name);

                        string _jsonLocal = JsonConvert.SerializeObject(_jsonObjectsTypesList, settings);

                        File.WriteAllText(Path.Combine(SessionHelper.WizardDirectory, file.Name), _jsonLocal);
                    }
                    catch (JsonSerializationException ex)
                    {
                        mcLogger.Error("Error during serialize data to file: " + file);
                        mcLogger.Error(ex.Message);
                    }
                }
            });
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
