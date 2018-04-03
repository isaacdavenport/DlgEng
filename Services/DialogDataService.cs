
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

namespace DialogEngine.Services
{
    public static class DialogDataService
    {
        #region - fields-

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region - public functions -

        public static async  Task LoadDialogDataAsync(string path)
        {
            ObservableCollection<Character> _characterList = new ObservableCollection<Character>();

            await Task.Run(() =>
            {
                Thread.CurrentThread.Name = "LoadDialogDataAsync";

                var _directoryInfo = new DirectoryInfo(path);
                FileInfo _fileInfo = _directoryInfo.GetFiles("*.json")[0];
                string _jsonString;

                try
                {
                    var _fileSteam = _fileInfo.OpenRead(); //open a read-only FileStream
                    using (var reader = new StreamReader(_fileSteam)) //creates new streamerader for fs stream. Could also construct with filename...
                    {
                        try
                        {
                            _jsonString = reader.ReadToEnd();
                            WizardsList _wizardsList = JsonConvert.DeserializeObject<WizardsList>(_jsonString); //json string to Object.

                            DialogData.Instance.CharacterCollection = new ObservableCollection<Character>(_wizardsList.Characters);
                            DialogData.Instance.DialogModelCollection = new ObservableCollection<ModelDialogInfo>(_wizardsList.DialogModels);
                            DialogData.Instance.WizardTypesCollection = new ObservableCollection<WizardType>(_wizardsList.Wizards);

                            EventAggregator.Instance.GetEvent<DialogDataLoadedEvent>().Publish();
                        }
                        catch (JsonReaderException e)
                        {
                            string _errorReadingMessage = "Error reading " + _fileInfo.Name;
                            DialogDataService.AddMessage(new ErrorMessage(_errorReadingMessage));

                            string _jsonParseErrorMessage = "JSON Parse error at " + e.LineNumber + ", " + e.LinePosition;
                            DialogDataService.AddMessage(new ErrorMessage(_jsonParseErrorMessage));
                        }
                        catch (Exception ex)
                        {
                            mcLogger.Error("Error during parsing json file " + ex.Message);
                        }
                    }
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


        public static void AddMessage(LogMessage message)
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
    }
}
