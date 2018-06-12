
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Dialog;
using DialogEngine.Models.Logger;
using DialogEngine.Models.Shared;
using DialogEngine.Models.Wizard;
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
            ObservableCollection<Character> _characterList = new ObservableCollection<Character>();

            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "LoadDialogDataAsync";

                try
                {
                    var _directoryInfo = new DirectoryInfo(path);
                    FileInfo[] _fileInfo = _directoryInfo.GetFiles("*.json");
                    string _jsonString;

                    for (int i=0;i< _fileInfo.Length; i++)
                    {
                        var _fileSteam = _fileInfo[i].OpenRead(); //open a read-only FileStream
                        using (var reader = new StreamReader(_fileSteam)) //creates new streamerader for fs stream. Could also construct with filename...
                        {
                            try
                            {
                                _jsonString = reader.ReadToEnd();

                                // read wizard .json file
                                if (_fileInfo[i].Name.Contains("Wizard"))
                                {
                                    WizardsList _wizardsList = JsonConvert.DeserializeObject<WizardsList>(_jsonString); //json string to Object.

                                    DialogData.Instance.DialogModelCollection = _wizardsList.DialogModels;
                                    DialogData.Instance.WizardTypesCollection = _wizardsList.Wizards;

                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        foreach (var character in _wizardsList.Characters)
                                            DialogData.Instance.CharacterCollection.Add(character);
                                    });
                                }
                                else
                                {
                                    // read characters in separated files
                                    Character character = JsonConvert.DeserializeObject<Character>(_jsonString);
                                    character.FileName = _fileInfo[i].Name;

                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        DialogData.Instance.CharacterCollection.Add(character);
                                    });
                                }

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


        public static  Task SerializeDataToFile(string path)
        {
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
                        if (!string.IsNullOrEmpty(DialogData.Instance.CharacterCollection[i].FileName))
                        {
                            string _jsonLocal = JsonConvert.SerializeObject(DialogData.Instance.CharacterCollection[i], settings);

                            File.WriteAllText(Path.Combine(SessionHelper.WizardDirectory, DialogData.Instance.CharacterCollection[i].FileName), _jsonLocal);

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                DialogData.Instance.CharacterCollection.RemoveAt(i);
                            });
                        }
                    }

                    WizardsList _wizardsList = new WizardsList
                    {

                        Wizards = DialogData.Instance.WizardTypesCollection,
                        Characters = DialogData.Instance.CharacterCollection,
                        DialogModels = DialogData.Instance.DialogModelCollection
                    };

                    string json = JsonConvert.SerializeObject(_wizardsList, settings);

                    File.WriteAllText(path, json);

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
