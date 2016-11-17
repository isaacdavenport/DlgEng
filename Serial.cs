//using System.IO.Ports;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace DialogEngine
{
    public class ReceivedMessage
    {
        public string characterPrefix = "XX";
        public DateTime receivedTime = DateTime.MinValue;
        public int[] rssi = new int[SerialComs.numRadios];
        public int sequenceNum = -1;
    }

    public static class SerialComs
    {
        public static List<ReceivedMessage> receivedMessages = new List<ReceivedMessage>();
        public const int numRadios = 6;
        private static bool rssiStable = false;
        private static int bigRssi = 0;
        public const int STRONG_RSSI_BUF_DEPTH = 12;
        public static int nextCharacter1 = 1, nextCharacter2 = 2; 
        //static SerialPort _serialPort;
        public static bool _continue = true;
        static int[,] heatMap = new int[numRadios, numRadios];
        static DateTime[] lastHeatMapUpdates = new DateTime[numRadios];
        public static readonly TimeSpan maxLastSeenInterval = new TimeSpan(0, 0, 0, 2, 100);
        static int[,] strongRssiCharacterPairBuf = new int[2, STRONG_RSSI_BUF_DEPTH];

        // public static void InitSerial() {
        //     if (!SessionVars.NoSerialPort) {
        //         _serialPort = new SerialPort();
        //         Thread readThread = new Thread(ReadAndParse);
        //         _serialPort.PortName = "COM4";
        //         _serialPort.BaudRate = 460800;
        //         _serialPort.ReadTimeout = 500;
        //         _serialPort.Open();
        //         _serialPort.DiscardInBuffer();
        //         readThread.Start();
        //     }
        //     else {
        //         Thread dontReadThread = new Thread(DontReadAndParse);
        //         dontReadThread.Start();
        //     }
        //     //worry about stopping cleanly later TODO
        // }

        public static void PrintHeatMap() {
            int i, l, m;

            for (i = 0; i < numRadios - 1; i++) {
                Console.Write(lastHeatMapUpdates[i].ToString("mm.ss.fff") + " ");
            }
            Console.WriteLine();
            for (l = 0; l < numRadios - 1; l++) {
                for (m = 0; m < numRadios - 1; m++) {
                    Console.Write("{0:D3}", heatMap[l,m]);
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  Character1-2Num " + Program.theDialogs.CharacterList[Program.theDialogs.Character1Num].CharacterPrefix 
                + " " + Program.theDialogs.CharacterList[Program.theDialogs.Character2Num].CharacterPrefix
                + " RSSIsum " + "{0:D3}", bigRssi + ", rssiStable = " + rssiStable);
            Console.WriteLine("");
        }

        public static void PrintHeatMapSums()
        {
            int i, l, m;

            for (i = 0; i < numRadios - 1; i++)
            {
                Console.Write(lastHeatMapUpdates[i].ToString("mm.ss.fff") + " ");
            }
            Console.WriteLine();
            for (l = 0; l < numRadios - 2; l++)
            {
                for (m = 1; m < numRadios - 1; m++)
                {
                    if (m > l) {
                        Console.Write("{0:D3}", (heatMap[l, m] + heatMap[m, l]));
                    }
                    else {
                        Console.Write("{0:D3}", (0));  // only show diagonal top of matrix when summed, symetrical
                    }
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  nextCharacter1-2 " + Program.theDialogs.CharacterList[nextCharacter1].CharacterPrefix
                + " " + Program.theDialogs.CharacterList[nextCharacter2].CharacterPrefix
                + " RSSIsum " + "{0:D3}", bigRssi + ", rssiStable = " + rssiStable);
            Console.WriteLine("");
        }


        public static void CheckStuckTransmissions() {
            // step through recent messages to ensure all characters are still transmitting 
            // and transmitting unique messages (for debug of no-new-data FW bug)
            // each character is "OK " STUCK "STK" or "MIA" missing in action
            // this could be changed to order N rather than numCharacters*N but not worth it now
            const int numMsgToChk = 5;
            if (receivedMessages.Count < 20) {
                return;
            }
            var currentTime = DateTime.Now;
            foreach (var chr in Program.theDialogs.CharacterList) {
                Console.Write(chr.CharacterPrefix + " ");
                ReceivedMessage[] lastFiveMsg = new ReceivedMessage[numMsgToChk];
                int i, j = 0;
                for ( i = receivedMessages.Count - 1; i > 2; i--) {
                    if (receivedMessages[i].characterPrefix == chr.CharacterPrefix) {
                        if (j == 0 && currentTime - receivedMessages[i].receivedTime > maxLastSeenInterval) {
                            Console.Write("MIA ");  //haven't seen this character in over maxLastSeenInterval seconds
                            break;
                        }
                        if (j < numMsgToChk) {
                            lastFiveMsg[j] = receivedMessages[i];
                            j++;
                        }
                        else {
                            bool messagesUnique = false;
                            for (var k = 1; k < numMsgToChk; k++) {
                                if (lastFiveMsg[k].sequenceNum != lastFiveMsg[k - 1].sequenceNum) {
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

        // private static string ReadSerialInLine() {
        //     string message = null;
        //     try {
        //         if (_serialPort.BytesToRead > 18) {
        //             message = _serialPort.ReadLine();
        //             if (_serialPort.BytesToRead > 1000) {
        //                 // got behind for some reason
        //                 _serialPort.DiscardInBuffer();
        //                 Console.WriteLine("serial buffer over run.");
        //             }
        //             if (SessionVars.WriteSerialLog) {
        //                 using (StreamWriter serialLog = new StreamWriter(
        //                     @"c:\Isaac\Toys2LifeResources\CapturesAndAnalysis\SerialLog.txt", true)) {
        //                     serialLog.Write(DateTime.Now.ToString("mm.ss.fff") + "  ");
        //                     serialLog.Write(message);
        //                     serialLog.Close();
        //                 }
        //             }
        //         }
        //     }
        //     catch (TimeoutException) {
        //         Console.WriteLine("serial timeout.");
        //     }
        //     return message;
        // }

        private static int Parse(string message, int[] rssiRow) {  // rssiRow also has seqNum from FW at end
            int rowNumber = -1;
            
            if (message.StartsWith("ff") && message.Contains("a5") && message.Length == 19) {
                for (int i = 0; i < numRadios; i++) {
                    string subMessage = message.Substring(i*2 + 2, 2);
                    rssiRow[i] = int.Parse(subMessage, System.Globalization.NumberStyles.HexNumber);
                    if (rssiRow[i] == 0xFF) rowNumber = i;
                }
                // The final int after the receiver for the PC, skipping "a5" key value is sequence number
                rssiRow[numRadios] = int.Parse(message.Substring(numRadios * 2 + 4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (rowNumber == -1 && SessionVars.MonitorMessageParseFails) Console.WriteLine("Failed to parse message.");

            return rowNumber;
        }

        static void EnqueLatestCharacters(int ch1, int ch2) {
            rssiStable = true;
            for (int i = 0; i < STRONG_RSSI_BUF_DEPTH-1; i++) {  // scoot data in buffer back by one to make room for next
                strongRssiCharacterPairBuf[0, i] = strongRssiCharacterPairBuf[0, i + 1];
                strongRssiCharacterPairBuf[1, i] = strongRssiCharacterPairBuf[1, i + 1];
            }
            strongRssiCharacterPairBuf[0, STRONG_RSSI_BUF_DEPTH - 1] = ch1;
            strongRssiCharacterPairBuf[1, STRONG_RSSI_BUF_DEPTH - 1] = ch2;

            for (int i = 0; i < STRONG_RSSI_BUF_DEPTH-1; i++)
            {
                if (strongRssiCharacterPairBuf[0, i] != strongRssiCharacterPairBuf[0, i + 1] || 
                    strongRssiCharacterPairBuf[1, i] != strongRssiCharacterPairBuf[1, i + 1]) {
                    rssiStable = false;
                    break;
                }
            }
        }

        static void FindBiggestRssiPair() {
            //  This method takes the RSSI values and combines them so that the RSSI for Ch2 looking at 
            //  Ch1 is added to the RSSI for Ch1 looking at Ch2
            int tempCh1 = 0, tempCh2 = 0, i = 0, j = 0;
            var currentTime = DateTime.Now;
            tempCh1 = nextCharacter1;
            tempCh2 = nextCharacter2;
            bigRssi = heatMap[tempCh1, tempCh2] + heatMap[tempCh2, tempCh1];  //only pick up new characters if bigRssi greater not =
            for (i = 0; i < numRadios - 1; i++) {  // the sixth radio is the computer's receiver
                for (j = i + 1; j < numRadios - 1; j++) {  // only need data above the matrix diagonal
                    if (heatMap[i, j] + heatMap[j, i] > bigRssi && currentTime - lastHeatMapUpdates[i] < maxLastSeenInterval
                                && currentTime - lastHeatMapUpdates[j] < maxLastSeenInterval) {  // look at both characters view of each other
                        bigRssi = heatMap[i, j] + heatMap[j, i];
                        tempCh1 = i;
                        tempCh2 = j;
                    }
                }
            }
            EnqueLatestCharacters(tempCh1, tempCh2);
            if ((bigRssi | 0x00000001) != 0 && rssiStable) {  //quasi random selection of which character is first
                nextCharacter1 = tempCh1;
                nextCharacter2 = tempCh2;
            } else if (rssiStable) {
                nextCharacter1 = tempCh2;
                nextCharacter2 = tempCh1;
            }
        }

        static void AddMessageToReceivedBuffer(int characterRowNum, int[] rw, DateTime timeStamp) {
            receivedMessages.Add(new ReceivedMessage() {
            receivedTime = timeStamp,               
            sequenceNum = rw[numRadios],
            characterPrefix = Program.theDialogs.CharacterList[characterRowNum].CharacterPrefix
            });
            //TODO add a lock around this
            for (int i = 0; i < numRadios; i++) {
                receivedMessages.Last().rssi[i] = rw[i];
            }

            if (SessionVars.WriteSerialLog)
            {
                using (StreamWriter serialLogDecimal = new StreamWriter(
                    @"c:\Isaac\Toys2LifeResources\CapturesAndAnalysis\SerialLogDecimal.txt", true))
                {
                    serialLogDecimal.Write(receivedMessages[receivedMessages.Count - 1].characterPrefix + "  ");
                    serialLogDecimal.Write(receivedMessages[receivedMessages.Count - 1].receivedTime.ToString("mm.ss.fff") + "  ");
                    for (var j = 0; j < numRadios; j++)
                    {
                        serialLogDecimal.Write("{0:D3}", receivedMessages[receivedMessages.Count - 1].rssi[j]);
                        serialLogDecimal.Write(" ");
                    }
                    serialLogDecimal.Write("{0:D3}", receivedMessages[receivedMessages.Count - 1].sequenceNum);
                    serialLogDecimal.WriteLine();
                    serialLogDecimal.Close();
                }
            }

            if (receivedMessages.Count > 10000) {
                receivedMessages.RemoveRange(0,100);
            }
        }

        // public static void ReadAndParse() {
        //     int[] newRow = new int[numRadios + 1];
        //     int cycleCount = 0;

        //     while (_continue) {
        //         var processCurrentMessage = true;
        //         var rowNum = -1;
        //         var message = ReadSerialInLine();
        //         if (message != null) {
        //             rowNum = Parse(message, newRow);
        //         } else {
        //             processCurrentMessage = false;  //we are in here a great deal
        //         }
        //         if (rowNum > -1 && rowNum < numRadios - 1 && processCurrentMessage) {
        //             cycleCount++;
        //             for (int k = 0; k < numRadios; k++) {
        //                 heatMap[rowNum, k] = newRow[k];
        //             }
        //             var currentDateTime = DateTime.Now;
        //             lastHeatMapUpdates[rowNum] = currentDateTime;
        //             AddMessageToReceivedBuffer(rowNum, newRow, currentDateTime);
        //             FindBiggestRssiPair();
        //         }

        //         if (cycleCount > 50) {
        //             TimeSpan lenOfBuffer;
        //             if (SessionVars.HeatMapSumsMode && !SessionVars.HeatMapOnlyMode)
        //                 PrintHeatMapSums();
        //             if (SessionVars.HeatMapFullMatrixDispMode && !SessionVars.HeatMapOnlyMode)
        //                 PrintHeatMap();
        //             if (SessionVars.CheckStuckTransmissions && !SessionVars.HeatMapOnlyMode)
        //                 CheckStuckTransmissions();
        //             if (SessionVars.MonitorReceiveBufferSize && !SessionVars.HeatMapOnlyMode) {
        //                 lenOfBuffer = receivedMessages.Last().receivedTime - receivedMessages[0].receivedTime;
        //                 Console.WriteLine("RecBuffCnt = " + receivedMessages.Count +
        //                                   " SecsOfBuff = " + lenOfBuffer.ToString(@"mm\.ss\.fff"));
        //             }
        //             cycleCount = 0;
        //         }
        //      }
        // }

        public static void DontReadAndParse()
        {
            while (_continue)
            {
                nextCharacter1 = RandomNumbers.Gen.Next(0, numRadios - 1); //lower bound inclusing upper exclusive
                while (nextCharacter1 == nextCharacter2) {
                    nextCharacter2 = RandomNumbers.Gen.Next(0, numRadios - 1);  
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