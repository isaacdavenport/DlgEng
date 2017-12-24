//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;

namespace DialogEngine
{

    public static class ParseMessage
    {

        #region - Fields -

        public  static  DialogTracker DialogTracker=DialogTracker.Instance;

        public static List<ReceivedMessage> ReceivedMessages = new List<ReceivedMessage>();


        #endregion

        #region - Private methods -

        static void addMessageToReceivedBuffer(int _characterRowNum, int[] _rw, DateTime _timeStamp)
        {

            if (_characterRowNum > DialogTracker.CharacterList.Count - 1)  //was omiting character 5 from log when it was Count - 2
            {
                return;
            }

            ReceivedMessages.Add(new ReceivedMessage()
            {
                ReceivedTime = _timeStamp,
                SequenceNum = _rw[SerialComs.NumRadios],
                CharacterPrefix = DialogTracker.CharacterList[_characterRowNum].CharacterPrefix
            });


            //TODO add a lock around this
            for (int _i = 0; _i < SerialComs.NumRadios; _i++)
            {
                ReceivedMessages.Last().Rssi[_i] = _rw[_i];
            }

            if (SessionVariables.WriteSerialLog)
            {
                using (StreamWriter _serialLogDecimal = new StreamWriter((SessionVariables.LogsDirectory + SessionVariables.DecimalLogFileName), true))
                {
                    _serialLogDecimal.Write(ReceivedMessages[ReceivedMessages.Count - 1].CharacterPrefix + "  ");
                    _serialLogDecimal.Write(ReceivedMessages[ReceivedMessages.Count - 1].ReceivedTime.ToString("mm.ss.fff") + "  ");

                    for (var _j = 0; _j < SerialComs.NumRadios; _j++)
                    {
                        _serialLogDecimal.Write("{0:D3}", ReceivedMessages[ReceivedMessages.Count - 1].Rssi[_j]);
                        _serialLogDecimal.Write(" ");
                    }

                    _serialLogDecimal.Write("{0:D3}", ReceivedMessages[ReceivedMessages.Count - 1].SequenceNum);
                    _serialLogDecimal.WriteLine();
                    _serialLogDecimal.Close();
                }
            }

            if (ReceivedMessages.Count > 30000)
            {
                ReceivedMessages.RemoveRange(0, 100);
            }
        }

        #endregion

        #region - Public functions -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_rowNum"></param>
        /// <param name="_newRow"></param>
        public static void ProcessMessage(int _rowNum, int[] _newRow)
        {

            for (int _k = 0; _k < SerialComs.NumRadios; _k++)
            {
                SelectNextCharacters.HeatMap[_rowNum, _k] = _newRow[_k];
            }

            var _currentDateTime = DateTime.Now;

            SelectNextCharacters.CharactersLastHeatMapUpdateTime[_rowNum] = _currentDateTime;

            addMessageToReceivedBuffer(_rowNum, _newRow, _currentDateTime);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_message"></param>
        /// <param name="_rssiRow"></param>
        /// <returns></returns>
        public static int Parse(string _message, int[] _rssiRow)
        {
            // rssiRow also has seqNum from FW at end
            int _rowNumber = -1;

            if (_message.StartsWith("ff") && _message.Contains("a5") && _message.Length == 19)
            {
                for (int _i = 0; _i < SerialComs.NumRadios; _i++)
                {
                    string _subMessage = _message.Substring(_i * 2 + 2, 2);
                    _rssiRow[_i] = int.Parse(_subMessage, System.Globalization.NumberStyles.HexNumber);
                    if (_rssiRow[_i] == 0xFF) _rowNumber = _i;
                }

                // The final int after the receiver for the PC, skipping "a5" key value is sequence number
                _rssiRow[SerialComs.NumRadios] = int.Parse(_message.Substring(SerialComs.NumRadios * 2 + 4, 2), System.Globalization.NumberStyles.HexNumber);

            }
            if (_rowNumber == -1 && SessionVariables.MonitorMessageParseFails) Console.WriteLine("Failed to parse message.");

            return _rowNumber;
        }


        #endregion


    }
}

