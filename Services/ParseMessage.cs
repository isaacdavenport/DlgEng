//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.ViewModels;
using log4net;
using System.Reflection;
using DialogEngine.Hendlers;

namespace DialogEngine
{

    public static class ParseMessage
    {
        #region - Fields -
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static List<ReceivedMessage> ReceivedMessages = new List<ReceivedMessage>();

        #endregion

        #region - Private methods -

        private static void _addMessageToReceivedBuffer(int _characterRowNum, int[] _rw, DateTime _timeStamp)
        {
            try
            {
                if (_characterRowNum > DialogViewModel.Instance.CharacterCollection.Count - 1)  //was omiting character 5 from log when it was Count - 2
                {
                    return;
                }

                ReceivedMessages.Add(new ReceivedMessage()
                {
                    ReceivedTime = _timeStamp,
                    SequenceNum = _rw[SerialSelection.NumRadios],
                    CharacterPrefix = DialogViewModel.Instance.CharacterCollection[_characterRowNum].CharacterPrefix
                });

                //TODO add a lock around this
                for (int _i = 0; _i < SerialSelection.NumRadios; _i++)
                {
                    ReceivedMessages.Last().Rssi[_i] = _rw[_i];
                }

                string _debugString = ReceivedMessages[ReceivedMessages.Count - 1].CharacterPrefix + "  ";

                for (var j = 0; j < SerialSelection.NumRadios; j++)
                {
                    _debugString += ReceivedMessages[ReceivedMessages.Count - 1].Rssi[j].ToString("D3");
                    _debugString += " ";
                }

                _debugString += ReceivedMessages[ReceivedMessages.Count - 1].SequenceNum.ToString("D3");

                LoggerHelper.Info(SessionVariables.DecimalLogFileName, _debugString);

                if (ReceivedMessages.Count > 30000)
                {
                    ReceivedMessages.RemoveRange(0, 100);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region - Public functions -


        public static void ProcessMessage(int _rowNum, int[] _newRow)
        {
            try
            {
                for (int _k = 0; _k < SerialSelection.NumRadios; _k++)
                {
                    if (DialogViewModel.Instance.RadiosState[_rowNum])
                    {
                        SerialSelection.HeatMap[_rowNum, _k] = _newRow[_k];
                    }
                    else
                    {
                        SerialSelection.HeatMap[_rowNum, _k] = 0;
                    }
                }

                var _currentDateTime = DateTime.Now;

                SerialSelection.CharactersLastHeatMapUpdateTime[_rowNum] = _currentDateTime;

                _addMessageToReceivedBuffer(_rowNum, _newRow, _currentDateTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static int Parse(string _message,ref int[] _rssiRow)
        {
            try
            {
                // rssiRow also has seqNum from FW at end
                int _rowNumber = -1;

                if (_message.StartsWith("ff") && _message.Contains("a5") && _message.Length == 19)
                {
                    for (int _i = 0; _i < SerialSelection.NumRadios; _i++)
                    {
                        string _subMessage = _message.Substring(_i * 2 + 2, 2);
                        _rssiRow[_i] = int.Parse(_subMessage, System.Globalization.NumberStyles.HexNumber);
                        if (_rssiRow[_i] == 0xFF) _rowNumber = _i;
                    }

                    // The final int after the receiver for the PC, skipping "a5" key value is sequence number
                    _rssiRow[SerialSelection.NumRadios] = int.Parse(_message.Substring(SerialSelection.NumRadios * 2 + 4, 2), System.Globalization.NumberStyles.HexNumber);

                }
                if (_rowNumber == -1 && SessionVariables.MonitorMessageParseFails) mcLogger.Error("Failed to parse message.");

                return _rowNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}

