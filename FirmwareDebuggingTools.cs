//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Linq;
using System.Windows;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;

//TODO pull stuff out of here that is not serial and create a CharacterSelection.cs with pieces from DialogTracker

namespace DialogEngine
{
    public static class FirmwareDebuggingTools
    {
        #region -Fields-

        private static DialogTracker msDialogTracker=DialogTracker.Instance;

        public delegate void PrintMethod(string _message);

        private static PrintMethod mcAddItem;

        #endregion

        #region - Properties -

        public static PrintMethod AddItem { get; set; }


        #endregion

        #region - Static methods -

        public static void PrintHeatMap()
        {
            ;

            int i, l, k;

            for (i = 0; i < SerialComs.NumRadios; i++)
            {
                AddItem(SelectNextCharacters.CharactersLastHeatMapUpdateTime[i].ToString("mm.ss.fff") + " ");

            }


            for (l = 0; l < SerialComs.NumRadios; l++)
            {
                for (k = 0; k < SerialComs.NumRadios; k++)
                {
                    AddItem("{0:D3}" + SelectNextCharacters.HeatMap[l, k]);

                }
                
            }

            AddItem("Character1-2Num " + msDialogTracker.CharacterList[msDialogTracker.Character1Num].CharacterPrefix
                              + " " + msDialogTracker.CharacterList[msDialogTracker.Character2Num].CharacterPrefix
                              + " RSSIsum " + "{0:D3}" + SelectNextCharacters.BigRssi + ", rssiStable = " + SelectNextCharacters.RssiStable);

        }


        public static void PrintHeatMapSums()
        {
            int i, l, k;

            for (i = 0; i < SerialComs.NumRadios; i++)
            {
                //((MainWindow)Application.Current.MainWindow).TestOutput.Text += SelectNextCharacters.CharactersLastHeatMapUpdateTime[i].ToString("mm.ss.fff") + Environment.NewLine;
                //Console.Write(SelectNextCharacters.CharactersLastHeatMapUpdateTime[i].ToString("mm.ss.fff") + " ");
            }
            //((MainWindow)Application.Current.MainWindow).TestOutput.Text += Environment.NewLine;
            //Console.WriteLine();
            for (l = 0; l < SerialComs.NumRadios; l++)
            {
                for (k = 1; k < SerialComs.NumRadios; k++)
                {
                    if (k > l)
                    {
                        //((MainWindow)Application.Current.MainWindow).TestOutput.Text += "{0:D3}" + (SelectNextCharacters.HeatMap[l, m] + SelectNextCharacters.HeatMap[m, l]) + Environment.NewLine;
                        //Console.Write("{0:D3}", (SelectNextCharacters.HeatMap[l, m] + SelectNextCharacters.HeatMap[m, l]));
                    }
                    else
                    {
                        //((MainWindow)Application.Current.MainWindow).TestOutput.Text += "{0:D3}" + (0) + Environment.NewLine; //vb : is this format correct?
                        //Console.Write("{0:D3}", (0));  // only show diagonal top of matrix when summed, symetrical
                    }
                    //((MainWindow)Application.Current.MainWindow).TestOutput.Text += " " + Environment.NewLine;
                    //Console.Write(" ");
                }
                //((MainWindow)Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine;
                //Console.WriteLine("");
            }
            //((MainWindow)Application.Current.MainWindow).TestOutput.Text += "  nextCharacter1-2 " + Program.TheDialogs.CharacterList[SelectNextCharacters.NextCharacter1].CharacterPrefix
                              //+ " " + Program.TheDialogs.CharacterList[SelectNextCharacters.NextCharacter2].CharacterPrefix
                              //+ " RSSIsum " + "{0:D3}" + SelectNextCharacters.BigRssi + ", rssiStable = " + SelectNextCharacters.RssiStable + Environment.NewLine;

            /*Console.WriteLine("  nextCharacter1-2 " + Program.TheDialogs.CharacterList[SelectNextCharacters.NextCharacter1].CharacterPrefix
                              + " " + Program.TheDialogs.CharacterList[SelectNextCharacters.NextCharacter2].CharacterPrefix
                              + " RSSIsum " + "{0:D3}", SelectNextCharacters.BigRssi + ", rssiStable = " + SelectNextCharacters.RssiStable); */
            //((MainWindow)Application.Current.MainWindow).TestOutput.Text += "" + Environment.NewLine;
            //Console.WriteLine("");
        }

        public static void CheckStuckTransmissions()
        {
            // step through recent messages to ensure all characters are still transmitting 
            // and transmitting unique messages (for debug of no-new-data FW bug)
            // each character is "OK " STUCK "STK" or "MIA" missing in action
            // this could be changed to order N rather than numCharacters*N but not worth it now


            const int numMsgToChk = 5;
            if (ParseMessage.ReceivedMessages.Count < 20)
            {
                return;
            }

            var _currentTime = DateTime.Now;
            foreach (var _chr in msDialogTracker.CharacterList)
            {
                Console.Write(_chr.CharacterPrefix + " ");
                ReceivedMessage[] _lastFiveMsg = new ReceivedMessage[numMsgToChk];
                int i, j = 0;
                for (i = ParseMessage.ReceivedMessages.Count - 1; i > 2; i--)
                {
                    if (ParseMessage.ReceivedMessages[i].CharacterPrefix == _chr.CharacterPrefix)
                    {
                        if (j == 0 && _currentTime - ParseMessage.ReceivedMessages[i].ReceivedTime > SelectNextCharacters.MaxLastSeenInterval)
                        {
                            Console.Write("MIA ");  //haven't seen this character in over maxLastSeenInterval seconds
                            break;
                        }
                        if (j < numMsgToChk)
                        {
                            _lastFiveMsg[j] = ParseMessage.ReceivedMessages[i];
                            j++;
                        }
                        else
                        {
                            bool _messagesUnique = false;
                            for (var k = 1; k < numMsgToChk; k++)
                            {
                                if (_lastFiveMsg[k].SequenceNum != _lastFiveMsg[k - 1].SequenceNum)
                                {
                                    Console.Write("OK  ");  //some of the recent messages are unique
                                    _messagesUnique = true;
                                    break;
                                }
                            }
                            if (!_messagesUnique)
                            {
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
        }

        public static void ProcessDebugFlags()
        {
            TimeSpan _lenOfBuffer;

            if (SessionVariables.HeatMapSumsMode && !SessionVariables.HeatMapOnlyMode)
            {
                PrintHeatMapSums();
            }
            if (SessionVariables.HeatMapFullMatrixDispMode && !SessionVariables.HeatMapOnlyMode)
            {
                PrintHeatMap(); 
            }
            if (SessionVariables.CheckStuckTransmissions && !SessionVariables.HeatMapOnlyMode)
            {
                CheckStuckTransmissions();
            }
            if (SessionVariables.MonitorReceiveBufferSize && !SessionVariables.HeatMapOnlyMode)
            {
                _lenOfBuffer = ParseMessage.ReceivedMessages.Last().ReceivedTime - ParseMessage.ReceivedMessages[0].ReceivedTime;
                Console.WriteLine("RecBuffCnt = " + ParseMessage.ReceivedMessages.Count +
                                  " SecsOfBuff = " + _lenOfBuffer.ToString(@"mm\.ss\.fff"));
            }
        }

        #endregion

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
