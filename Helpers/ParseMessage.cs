//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DialogEngine
{
    public static class ParseMessage
    {
        public static List<ReceivedMessage> ReceivedMessages = new List<ReceivedMessage>();

        public static void ProcessMessage(int rowNum, int[] newRow) {
            for (int k = 0; k < SerialComs.NUM_RADIOS; k++)
            {
                SelectNextCharacters.HeatMap[rowNum, k] = newRow[k];
            }
            var currentDateTime = DateTime.Now;
            SelectNextCharacters.CharactersLastHeatMapUpdateTime[rowNum] = currentDateTime;
            AddMessageToReceivedBuffer(rowNum, newRow, currentDateTime);
        }

        public static int Parse(string message, int[] rssiRow)
        {  // rssiRow also has seqNum from FW at end
            int rowNumber = -1;

            if (message.StartsWith("ff") && message.Contains("a5") && message.Length == 19)
            {
                for (int i = 0; i < SerialComs.NUM_RADIOS; i++)
                {
                    string subMessage = message.Substring(i * 2 + 2, 2);
                    rssiRow[i] = int.Parse(subMessage, System.Globalization.NumberStyles.HexNumber);
                    if (rssiRow[i] == 0xFF) rowNumber = i;
                }
                // The final int after the receiver for the PC, skipping "a5" key value is sequence number
                rssiRow[SerialComs.NUM_RADIOS] = int.Parse(message.Substring(SerialComs.NUM_RADIOS * 2 + 4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (rowNumber == -1 && SessionVars.MonitorMessageParseFails) Console.WriteLine("Failed to parse message.");

            return rowNumber;
        }

        static void AddMessageToReceivedBuffer(int characterRowNum, int[] rw, DateTime timeStamp)
        {
            if (characterRowNum > Program.TheDialogs.CharacterList.Count - 1)  //was omiting character 5 from log when it was Count - 2
            {
                return;
            }
            ReceivedMessages.Add(new ReceivedMessage()
            {
                ReceivedTime = timeStamp,
                SequenceNum = rw[SerialComs.NUM_RADIOS],
                CharacterPrefix = Program.TheDialogs.CharacterList[characterRowNum].CharacterPrefix
            });
            //TODO add a lock around this
            for (int i = 0; i < SerialComs.NUM_RADIOS; i++)
            {
                ReceivedMessages.Last().Rssi[i] = rw[i];
            }

            if (SessionVars.WriteSerialLog)
            {
                using (StreamWriter serialLogDecimal = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DecimalLogFileName), true))
                {
                    serialLogDecimal.Write(ReceivedMessages[ReceivedMessages.Count - 1].CharacterPrefix + "  ");
                    serialLogDecimal.Write(ReceivedMessages[ReceivedMessages.Count - 1].ReceivedTime.ToString("mm.ss.fff") + "  ");
                    for (var j = 0; j < SerialComs.NUM_RADIOS; j++)
                    {
                        serialLogDecimal.Write("{0:D3}", ReceivedMessages[ReceivedMessages.Count - 1].Rssi[j]);
                        serialLogDecimal.Write(" ");
                    }
                    serialLogDecimal.Write("{0:D3}", ReceivedMessages[ReceivedMessages.Count - 1].SequenceNum);
                    serialLogDecimal.WriteLine();
                    serialLogDecimal.Close();
                }
            }

            if (ReceivedMessages.Count > 30000)
            {
                ReceivedMessages.RemoveRange(0, 100);
            }
        }
    }
}

