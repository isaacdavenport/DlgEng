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

        private static Random msRandom = new Random();
        private  static DialogTracker msDialogTracker=DialogTracker.Instance;


        public const int StrongRssiBufDepth = 12;

        public static readonly TimeSpan MaxLastSeenInterval = new TimeSpan(0, 0, 0, 2, 100);


        public static int BigRssi = 0;
        public static bool RssiStable = false;
        public static int NextCharacter1 = 1, NextCharacter2 = 2;
        public static int[,] HeatMap = new int[SerialComs.NumRadios, SerialComs.NumRadios];
        public static DateTime[] CharactersLastHeatMapUpdateTime = new DateTime[SerialComs.NumRadios];
        static int[,] msStrongRssiCharacterPairBuf = new int[2, StrongRssiBufDepth];

        #endregion


        #region - Private methods -

        


        private static void enqueLatestCharacters(int _ch1, int _ch2)
        {
            RssiStable = true;

            for (int _i = 0; _i < StrongRssiBufDepth - 1; _i++)
            {  
                // scoot data in buffer back by one to make room for next
                msStrongRssiCharacterPairBuf[0, _i] = msStrongRssiCharacterPairBuf[0, _i + 1];
                msStrongRssiCharacterPairBuf[1, _i] = msStrongRssiCharacterPairBuf[1, _i + 1];
            }

            msStrongRssiCharacterPairBuf[0, StrongRssiBufDepth - 1] = _ch1;
            msStrongRssiCharacterPairBuf[1, StrongRssiBufDepth - 1] = _ch2;

            for (int _i = 0; _i < StrongRssiBufDepth - 1; _i++)
            {
                if (msStrongRssiCharacterPairBuf[0, _i] != msStrongRssiCharacterPairBuf[0, _i + 1] ||
                    msStrongRssiCharacterPairBuf[1, _i] != msStrongRssiCharacterPairBuf[1, _i + 1])
                {
                    RssiStable = false;
                    break;
                }
            }
        }

        static void assignNextCharacters(int _tempCh1, int _tempCh2)
        {
            if ((msRandom.NextDouble() > 0.5) && RssiStable)
            {
                NextCharacter1 = _tempCh1;
                NextCharacter2 = _tempCh2;
            }
            else if (RssiStable)
            {
                NextCharacter1 = _tempCh2;
                NextCharacter2 = _tempCh1;
            }
        }


        #endregion


        #region - Public methods -

        public static void FindBiggestRssiPair()
        {
            //  This method takes the RSSI values and combines them so that the RSSI for Ch2 looking at 
            //  Ch1 is added to the RSSI for Ch1 looking at Ch2

            int _tempCh1 = 0, _tempCh2 = 0, _i = 0, _j = 0;

            var _currentTime = DateTime.Now;
            _tempCh1 = NextCharacter1;
            _tempCh2 = NextCharacter2;

            BigRssi = HeatMap[_tempCh1, _tempCh2] + HeatMap[_tempCh2, _tempCh1];  //only pick up new characters if bigRssi greater not =


            for (_i = 0; _i < SerialComs.NumRadios; _i++)
            {  // the sixth radio is the computer's receiver now included for adventures

                for (_j = _i + 1; _j < SerialComs.NumRadios; _j++)
                {   
                    // only need data above the matrix diagonal

                    if (   HeatMap[_i, _j] + HeatMap[_j, _i] > BigRssi 
                        && _currentTime - CharactersLastHeatMapUpdateTime[_i] < MaxLastSeenInterval
                        && _currentTime - CharactersLastHeatMapUpdateTime[_j] < MaxLastSeenInterval)
                    {  
                        // look at both characters view of each other
                        BigRssi = HeatMap[_i, _j] + HeatMap[_j, _i];
                        _tempCh1 = _i;
                        _tempCh2 = _j;
                    }
                }

            }


            if (_tempCh1 <= msDialogTracker.CharacterList.Count && _tempCh2 <= msDialogTracker.CharacterList.Count)
            {
                enqueLatestCharacters(_tempCh1, _tempCh2);
                assignNextCharacters(_tempCh1, _tempCh2);
            }
        }

        public static void OccasionallyChangeToRandNewCharacter()
        {   
            // used for computers with no serial input radio for random, or forceCharacter mode
            // does not include final character the silent schoolhouse, not useful in noSerial mode 
            bool _userHasForcedCharacters = false;

            DateTime _nextCharacterSwapTime = new DateTime();

            _nextCharacterSwapTime = DateTime.Now;

            _nextCharacterSwapTime.AddSeconds(12);


            while (true)
            {
                if (SessionVars.DebugFlag && Console.KeyAvailable)
                {

                    var _userInput = Console.ReadLine();

                    String[] _charactersInitials = _userInput.Split(' ');


                    if (_charactersInitials.Length == 2 && _charactersInitials.Length < 15)
                    {  //two three letter inital sets should be less than7 w space

                        _userHasForcedCharacters = true;

                        foreach (var _character in msDialogTracker.CharacterList)
                        {
                            if (_charactersInitials[0] == _character.CharacterPrefix)
                            {
                                NextCharacter1 = msDialogTracker.CharacterList.IndexOf(_character);
                            }

                            if (_charactersInitials[1] == _character.CharacterPrefix)
                            {
                                NextCharacter2 = msDialogTracker.CharacterList.IndexOf(_character);
                            }
                        }
                    }
                }


                Thread.Sleep(1000);


                if (!_userHasForcedCharacters && _nextCharacterSwapTime.CompareTo(DateTime.Now) < 0)
                {
                    NextCharacter1 = msRandom.Next(0, msDialogTracker.CharacterList.Count); //lower bound inclusive, upper exclusive
                    NextCharacter2 = msRandom.Next(0, msDialogTracker.CharacterList.Count); //lower bound inclusive, upper exclusive
                    _nextCharacterSwapTime = DateTime.Now;
                    _nextCharacterSwapTime = _nextCharacterSwapTime.AddSeconds(8 + msRandom.Next(0, 34));
                }


                while (NextCharacter1 == NextCharacter2)
                {
                    NextCharacter2 = msRandom.Next(0, msDialogTracker.CharacterList.Count);
                }
            }
        }

        #endregion



    }
}