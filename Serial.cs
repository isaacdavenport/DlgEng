using System.IO.Ports;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace DialogEngine
{
    public class ReceivedMessage
    {
        public string CharacterPrefix = "XX";
        public DateTime ReceivedTime = DateTime.MinValue;
        public int[] Rssi = new int[SerialComs.NUM_RADIOS];
        public int SequenceNum = -1;
    }

    public static class SerialComs
    {
        public static List<ReceivedMessage> ReceivedMessages = new List<ReceivedMessage>();
        public const int NUM_RADIOS = 6;  //includes dongle
        private static bool _rssiStable = false;
        private static int _bigRssi = 0;
        public const int STRONG_RSSI_BUF_DEPTH = 12;
        public static int NextCharacter1 = 1, NextCharacter2 = 2; 
        static SerialPort _serialPort;
        public static bool Continue = true;
        static int[,] _heatMap = new int[NUM_RADIOS, NUM_RADIOS];
        static DateTime[] _charactersLastHeatMapUpdateTime = new DateTime[NUM_RADIOS];
        public static readonly TimeSpan MaxLastSeenInterval = new TimeSpan(0, 0, 0, 2, 100);
        static int[,] _strongRssiCharacterPairBuf = new int[2, STRONG_RSSI_BUF_DEPTH];

        public static void InitSerial() {
            if (!SessionVars.NoSerialPort) {
                _serialPort = new SerialPort();
                Thread readThread = new Thread(ReadAndParse);
                _serialPort.PortName = "COM4";
                _serialPort.BaudRate = 460800;
                _serialPort.ReadTimeout = 500;
                _serialPort.Open();
                _serialPort.DiscardInBuffer();
                readThread.Start();
            }
            else {
                Thread dontReadThread = new Thread(DontReadAndParse);
                dontReadThread.Start();
            }
            //worry about stopping cleanly later TODO
        }

        public static void PrintHeatMap() {
            int i, l, m;

            for (i = 0; i < NUM_RADIOS; i++) {
                Console.Write(_charactersLastHeatMapUpdateTime[i].ToString("mm.ss.fff") + " ");
            }
            Console.WriteLine();
            for (l = 0; l < NUM_RADIOS; l++) {
                for (m = 0; m < NUM_RADIOS; m++) {
                    Console.Write("{0:D3}", _heatMap[l,m]);
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  Character1-2Num " + Program.TheDialogs.CharacterList[Program.TheDialogs.Character1Num].CharacterPrefix 
                + " " + Program.TheDialogs.CharacterList[Program.TheDialogs.Character2Num].CharacterPrefix
                + " RSSIsum " + "{0:D3}", _bigRssi + ", rssiStable = " + _rssiStable);
            Console.WriteLine("");
        }

        public static void PrintHeatMapSums()
        {
            int i, l, m;

            for (i = 0; i < NUM_RADIOS; i++)
            {
                Console.Write(_charactersLastHeatMapUpdateTime[i].ToString("mm.ss.fff") + " ");
            }
            Console.WriteLine();
            for (l = 0; l < NUM_RADIOS; l++)
            {
                for (m = 1; m < NUM_RADIOS; m++)
                {
                    if (m > l) {
                        Console.Write("{0:D3}", (_heatMap[l, m] + _heatMap[m, l]));
                    }
                    else {
                        Console.Write("{0:D3}", (0));  // only show diagonal top of matrix when summed, symetrical
                    }
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  nextCharacter1-2 " + Program.TheDialogs.CharacterList[NextCharacter1].CharacterPrefix
                + " " + Program.TheDialogs.CharacterList[NextCharacter2].CharacterPrefix
                + " RSSIsum " + "{0:D3}", _bigRssi + ", rssiStable = " + _rssiStable);
            Console.WriteLine("");
        }


        public static void CheckStuckTransmissions() {
            // step through recent messages to ensure all characters are still transmitting 
            // and transmitting unique messages (for debug of no-new-data FW bug)
            // each character is "OK " STUCK "STK" or "MIA" missing in action
            // this could be changed to order N rather than numCharacters*N but not worth it now
            const int numMsgToChk = 5;
            if (ReceivedMessages.Count < 20) {
                return;
            }
            var currentTime = DateTime.Now;
            foreach (var chr in Program.TheDialogs.CharacterList) {
                Console.Write(chr.CharacterPrefix + " ");
                ReceivedMessage[] lastFiveMsg = new ReceivedMessage[numMsgToChk];
                int i, j = 0;
                for ( i = ReceivedMessages.Count - 1; i > 2; i--) {
                    if (ReceivedMessages[i].CharacterPrefix == chr.CharacterPrefix) {
                        if (j == 0 && currentTime - ReceivedMessages[i].ReceivedTime > MaxLastSeenInterval) {
                            Console.Write("MIA ");  //haven't seen this character in over maxLastSeenInterval seconds
                            break;
                        }
                        if (j < numMsgToChk) {
                            lastFiveMsg[j] = ReceivedMessages[i];
                            j++;
                        }
                        else {
                            bool messagesUnique = false;
                            for (var k = 1; k < numMsgToChk; k++) {
                                if (lastFiveMsg[k].SequenceNum != lastFiveMsg[k - 1].SequenceNum) {
                                    Console.Write("OK  ");  //some of the recent messages are unique
                                    messagesUnique = true;
                                    break;
                                }
                            }
                            if (!messagesUnique) { 
                                Console.Write("STK ");  //last five messages all had same seq number so we are stuck 
                            }
                            break;
                        }
                    }
                }
                if (i < 3)  // if we counted all the way down we didn't find our five messages from this character he is MIA
                {
                    Console.Write("MIA ");
                }
            }
            Console.WriteLine();
        }

        private static string ReadSerialInLine() {
            string message = null;
            try {
                if (_serialPort.BytesToRead > 18) {
                    message = _serialPort.ReadLine();
                    if (_serialPort.BytesToRead > 1000) {
                        // got behind for some reason
                        _serialPort.DiscardInBuffer();
                        Console.WriteLine("serial buffer over run.");
                    }
                    if (SessionVars.WriteSerialLog) {
                        using (StreamWriter serialLog = new StreamWriter(
                            SessionVars.LogsDirectory + SessionVars.HexSerialLogFileName, true)) {
                            serialLog.Write(DateTime.Now.ToString("mm.ss.fff") + "  ");
                            serialLog.Write(message);
                            serialLog.Close();
                        }
                    }
                }
            }
            catch (TimeoutException) {
                Console.WriteLine("serial timeout.");
            }
            return message;
        }

        private static int Parse(string message, int[] rssiRow) {  // rssiRow also has seqNum from FW at end
            int rowNumber = -1;
            
            if (message.StartsWith("ff") && message.Contains("a5") && message.Length == 19) {
                for (int i = 0; i < NUM_RADIOS; i++) {
                    string subMessage = message.Substring(i*2 + 2, 2);
                    rssiRow[i] = int.Parse(subMessage, System.Globalization.NumberStyles.HexNumber);
                    if (rssiRow[i] == 0xFF) rowNumber = i;
                }
                // The final int after the receiver for the PC, skipping "a5" key value is sequence number
                rssiRow[NUM_RADIOS] = int.Parse(message.Substring(NUM_RADIOS * 2 + 4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (rowNumber == -1 && SessionVars.MonitorMessageParseFails) Console.WriteLine("Failed to parse message.");

            return rowNumber;
        }

        static void EnqueLatestCharacters(int ch1, int ch2) {
            _rssiStable = true;
            for (int i = 0; i < STRONG_RSSI_BUF_DEPTH-1; i++) {  // scoot data in buffer back by one to make room for next
                _strongRssiCharacterPairBuf[0, i] = _strongRssiCharacterPairBuf[0, i + 1];
                _strongRssiCharacterPairBuf[1, i] = _strongRssiCharacterPairBuf[1, i + 1];
            }
            _strongRssiCharacterPairBuf[0, STRONG_RSSI_BUF_DEPTH - 1] = ch1;
            _strongRssiCharacterPairBuf[1, STRONG_RSSI_BUF_DEPTH - 1] = ch2;

            for (int i = 0; i < STRONG_RSSI_BUF_DEPTH-1; i++)
            {
                if (_strongRssiCharacterPairBuf[0, i] != _strongRssiCharacterPairBuf[0, i + 1] || 
                    _strongRssiCharacterPairBuf[1, i] != _strongRssiCharacterPairBuf[1, i + 1]) {
                    _rssiStable = false;
                    break;
                }
            }
        }

        static void AssignNextCharacters(int tempCh1, int tempCh2) {
            if ((RandomNumbers.Gen.NextDouble() > 0.5) && _rssiStable)
            {  
                NextCharacter1 = tempCh1;
                NextCharacter2 = tempCh2;
            }
            else if (_rssiStable)
            {
                NextCharacter1 = tempCh2;
                NextCharacter2 = tempCh1;
            }
        }

        static void FindBiggestRssiPair() {
            //  This method takes the RSSI values and combines them so that the RSSI for Ch2 looking at 
            //  Ch1 is added to the RSSI for Ch1 looking at Ch2
            int tempCh1 = 0, tempCh2 = 0, i = 0, j = 0;
            var currentTime = DateTime.Now;
            tempCh1 = NextCharacter1;
            tempCh2 = NextCharacter2;
            _bigRssi = _heatMap[tempCh1, tempCh2] + _heatMap[tempCh2, tempCh1];  //only pick up new characters if bigRssi greater not =
            for (i = 0; i < NUM_RADIOS; i++) {  // the sixth radio is the computer's receiver now included for adventures
                for (j = i + 1; j < NUM_RADIOS; j++) {  // only need data above the matrix diagonal
                    if (_heatMap[i, j] + _heatMap[j, i] > _bigRssi && currentTime - _charactersLastHeatMapUpdateTime[i] < MaxLastSeenInterval
                                && currentTime - _charactersLastHeatMapUpdateTime[j] < MaxLastSeenInterval) {  // look at both characters view of each other
                        _bigRssi = _heatMap[i, j] + _heatMap[j, i];
                        tempCh1 = i;
                        tempCh2 = j;
                    }
                }
            }
            EnqueLatestCharacters(tempCh1, tempCh2);
            AssignNextCharacters(tempCh1, tempCh2);
        }

        static void AddMessageToReceivedBuffer(int characterRowNum, int[] rw, DateTime timeStamp) {
            ReceivedMessages.Add(new ReceivedMessage() {
            ReceivedTime = timeStamp,               
            SequenceNum = rw[NUM_RADIOS],
            CharacterPrefix = Program.TheDialogs.CharacterList[characterRowNum].CharacterPrefix
            });
            //TODO add a lock around this
            for (int i = 0; i < NUM_RADIOS; i++) {
                ReceivedMessages.Last().Rssi[i] = rw[i];
            }

            if (SessionVars.WriteSerialLog)
            {
                using (StreamWriter serialLogDecimal = new StreamWriter(
                    (SessionVars.LogsDirectory + SessionVars.DecimalSerialLogFileName), true))
                {
                    serialLogDecimal.Write(ReceivedMessages[ReceivedMessages.Count - 1].CharacterPrefix + "  ");
                    serialLogDecimal.Write(ReceivedMessages[ReceivedMessages.Count - 1].ReceivedTime.ToString("mm.ss.fff") + "  ");
                    for (var j = 0; j < NUM_RADIOS; j++)
                    {
                        serialLogDecimal.Write("{0:D3}", ReceivedMessages[ReceivedMessages.Count - 1].Rssi[j]);
                        serialLogDecimal.Write(" ");
                    }
                    serialLogDecimal.Write("{0:D3}", ReceivedMessages[ReceivedMessages.Count - 1].SequenceNum);
                    serialLogDecimal.WriteLine();
                    serialLogDecimal.Close();
                }
            }

            if (ReceivedMessages.Count > 30000) {
                ReceivedMessages.RemoveRange(0,100);
            }
        }

        static void ProcessDebugFlags() {
            TimeSpan lenOfBuffer;
            if (SessionVars.HeatMapSumsMode && !SessionVars.HeatMapOnlyMode)
                PrintHeatMapSums();
            if (SessionVars.HeatMapFullMatrixDispMode && !SessionVars.HeatMapOnlyMode)
                PrintHeatMap();
            if (SessionVars.CheckStuckTransmissions && !SessionVars.HeatMapOnlyMode)
                CheckStuckTransmissions();
            if (SessionVars.MonitorReceiveBufferSize && !SessionVars.HeatMapOnlyMode)
            {
                lenOfBuffer = ReceivedMessages.Last().ReceivedTime - ReceivedMessages[0].ReceivedTime;
                Console.WriteLine("RecBuffCnt = " + ReceivedMessages.Count +
                                  " SecsOfBuff = " + lenOfBuffer.ToString(@"mm\.ss\.fff"));
            }
        }

        public static void ReadAndParse() {
            int[] newRow = new int[NUM_RADIOS + 1];
            int cycleCount = 0;

            while (Continue) {
                var processCurrentMessage = true;
                var rowNum = -1;
                var message = ReadSerialInLine();
                if (message != null) {
                    rowNum = Parse(message, newRow);
                } else {
                    processCurrentMessage = false;  //we are in here a great deal
                }
                if (rowNum > -1 && rowNum < NUM_RADIOS && processCurrentMessage) {
                    cycleCount++;
                    for (int k = 0; k < NUM_RADIOS; k++) {
                        _heatMap[rowNum, k] = newRow[k];
                    }
                    var currentDateTime = DateTime.Now;
                    _charactersLastHeatMapUpdateTime[rowNum] = currentDateTime;
                    AddMessageToReceivedBuffer(rowNum, newRow, currentDateTime);
                    FindBiggestRssiPair();
                }
                if (cycleCount > 70) {
                    ProcessDebugFlags();
                    cycleCount = 0;
                }
             }
        }

        public static void DontReadAndParse()
        {  // used for computers with no serial input radio for random, or forceCharacter mode
            // does not include final character the silent schoolhouse, not useful in noSerial mode
            while (Continue) {
                NextCharacter1 = RandomNumbers.Gen.Next(0, NUM_RADIOS - 1); //lower bound inclusive upper exclusive
                while (NextCharacter1 == NextCharacter2) {
                    NextCharacter2 = RandomNumbers.Gen.Next(0, NUM_RADIOS - 1);  
                }
                Thread.Sleep(8000 + RandomNumbers.Gen.Next(0, 34000));
            }
        }
    }
}


/* sample input strings from embedded radios
  First FF is start character 
  A5 is the end character
  between FF and A5 are the one byte RSSI in hex
  after A5 is the sequence number that updates each time a radio puts new data into its output buffer to send
  each message should have an FF between the start FF and the end A5 in the slot corresponding to the number
  of which radio was transmitting.  If I am radio 3, I put FF in slot three of my output message which is
  like me, radio 3, seeing my own RSSI as FF.  Radio 5 is always RSSI of 00 since we only have 
  See radio firmware for details.

  message from radio 0, radio 0 recently saw radio 2 with RSSI of CD
ffffe2cdd0ca00a5c4
  message from radio 3
ffcad3d3ffdc00a5a7
ffcad0d0dcff00a59d
*/