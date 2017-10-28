//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Timers;
using System.Threading;
using DialogEngine.Helpers;

namespace DialogEngine
{
    public static class SelectNextCharacters
    {
        #region  - Fields -

        private static Random _random = new Random();
        private  static DialogTracker _dialogTracker=DialogTracker.Instance;


        public const int STRONG_RSSI_BUF_DEPTH = 12;

        public static readonly TimeSpan MaxLastSeenInterval = new TimeSpan(0, 0, 0, 2, 100);


        public static int BigRssi = 0;
        public static bool RssiStable = false;
        public static int NextCharacter1 = 1, NextCharacter2 = 2;
        public static int[,] HeatMap = new int[SerialComs.NUM_RADIOS, SerialComs.NUM_RADIOS];
        public static DateTime[] CharactersLastHeatMapUpdateTime = new DateTime[SerialComs.NUM_RADIOS];
        static int[,] _strongRssiCharacterPairBuf = new int[2, STRONG_RSSI_BUF_DEPTH];

        #endregion


        #region - Private methods -

        


        static void EnqueLatestCharacters(int ch1, int ch2)
        {
            RssiStable = true;

            for (int i = 0; i < STRONG_RSSI_BUF_DEPTH - 1; i++)
            {  
                // scoot data in buffer back by one to make room for next
                _strongRssiCharacterPairBuf[0, i] = _strongRssiCharacterPairBuf[0, i + 1];
                _strongRssiCharacterPairBuf[1, i] = _strongRssiCharacterPairBuf[1, i + 1];
            }

            _strongRssiCharacterPairBuf[0, STRONG_RSSI_BUF_DEPTH - 1] = ch1;
            _strongRssiCharacterPairBuf[1, STRONG_RSSI_BUF_DEPTH - 1] = ch2;

            for (int i = 0; i < STRONG_RSSI_BUF_DEPTH - 1; i++)
            {
                if (_strongRssiCharacterPairBuf[0, i] != _strongRssiCharacterPairBuf[0, i + 1] ||
                    _strongRssiCharacterPairBuf[1, i] != _strongRssiCharacterPairBuf[1, i + 1])
                {
                    RssiStable = false;
                    break;
                }
            }
        }

        static void AssignNextCharacters(int tempCh1, int tempCh2)
        {
            if ((_random.NextDouble() > 0.5) && RssiStable)
            {
                NextCharacter1 = tempCh1;
                NextCharacter2 = tempCh2;
            }
            else if (RssiStable)
            {
                NextCharacter1 = tempCh2;
                NextCharacter2 = tempCh1;
            }
        }


        #endregion


        #region - Public methods -

        public static void FindBiggestRssiPair()
        {
            //  This method takes the RSSI values and combines them so that the RSSI for Ch2 looking at 
            //  Ch1 is added to the RSSI for Ch1 looking at Ch2
            int tempCh1 = 0, tempCh2 = 0, i = 0, j = 0;

            var currentTime = DateTime.Now;
            tempCh1 = NextCharacter1;
            tempCh2 = NextCharacter2;

            BigRssi = HeatMap[tempCh1, tempCh2] + HeatMap[tempCh2, tempCh1];  //only pick up new characters if bigRssi greater not =


            for (i = 0; i < SerialComs.NUM_RADIOS; i++)
            {  // the sixth radio is the computer's receiver now included for adventures

                for (j = i + 1; j < SerialComs.NUM_RADIOS; j++)
                {  // only need data above the matrix diagonal
                    if (HeatMap[i, j] + HeatMap[j, i] > BigRssi && currentTime - CharactersLastHeatMapUpdateTime[i] < MaxLastSeenInterval
                        && currentTime - CharactersLastHeatMapUpdateTime[j] < MaxLastSeenInterval)
                    {  // look at both characters view of each other
                        BigRssi = HeatMap[i, j] + HeatMap[j, i];
                        tempCh1 = i;
                        tempCh2 = j;
                    }
                }

            }


            if (tempCh1 <= _dialogTracker.CharacterList.Count && tempCh2 <= _dialogTracker.CharacterList.Count)
            {
                EnqueLatestCharacters(tempCh1, tempCh2);
                AssignNextCharacters(tempCh1, tempCh2);
            }
        }

        public static void OccasionallyChangeToRandNewCharacter()
        {   
            // used for computers with no serial input radio for random, or forceCharacter mode
            // does not include final character the silent schoolhouse, not useful in noSerial mode 
            bool userHasForcedCharacters = false;

            DateTime nextCharacterSwapTime = new DateTime();

            nextCharacterSwapTime = DateTime.Now;

            nextCharacterSwapTime.AddSeconds(12);


            while (true)
            {
                if (SessionVars.DebugFlag && Console.KeyAvailable)
                {

                    var userInput = Console.ReadLine();

                    String[] CharactersInitials = userInput.Split(' ');


                    if (CharactersInitials.Length == 2 && CharactersInitials.Length < 15)
                    {  //two three letter inital sets should be less than7 w space

                        userHasForcedCharacters = true;

                        foreach (var character in _dialogTracker.CharacterList)
                        {
                            if (CharactersInitials[0] == character.CharacterPrefix)
                            {
                                NextCharacter1 = _dialogTracker.CharacterList.IndexOf(character);
                            }
                            if (CharactersInitials[1] == character.CharacterPrefix)
                            {
                                NextCharacter2 = _dialogTracker.CharacterList.IndexOf(character);
                            }
                        }
                    }
                }


                Thread.Sleep(1000);


                if (!userHasForcedCharacters && nextCharacterSwapTime.CompareTo(DateTime.Now) < 0)
                {
                    NextCharacter1 = _random.Next(0, _dialogTracker.CharacterList.Count); //lower bound inclusive, upper exclusive
                    NextCharacter2 = _random.Next(0, _dialogTracker.CharacterList.Count); //lower bound inclusive, upper exclusive
                    nextCharacterSwapTime = DateTime.Now;
                    nextCharacterSwapTime = nextCharacterSwapTime.AddSeconds(8 + _random.Next(0, 34));
                }


                while (NextCharacter1 == NextCharacter2)
                {
                    NextCharacter2 = _random.Next(0, _dialogTracker.CharacterList.Count);
                }
            }
        }

        #endregion



    }
}