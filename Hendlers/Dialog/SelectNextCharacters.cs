//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Timers;
using System.Threading;
using DialogEngine.Helpers;
using DialogEngine.ViewModels.Dialog;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;

namespace DialogEngine
{
    public static class SelectNextCharacters
    {
        #region  - Fields -

        private static Random msRandom = new Random();
        private static int[,] msStrongRssiCharacterPairBuf = new int[2, StrongRssiBufDepth];

        public const int StrongRssiBufDepth = 12;
        public static readonly TimeSpan MaxLastSeenInterval = new TimeSpan(0, 0, 0, 2, 100);

        public static int BigRssi = 0;
        public static bool RssiStable = false;
        public static int NextCharacter1 = 1;
        public static int NextCharacter2 = 2;
        public static int[,] HeatMap = new int[SerialComs.NumRadios, SerialComs.NumRadios];
        public static DateTime[] CharactersLastHeatMapUpdateTime = new DateTime[SerialComs.NumRadios];

        #endregion


        #region - Private methods -



        private static void _enqueLatestCharacters(int _ch1, int _ch2)
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


        static void _assignNextCharacters(int _tempCh1, int _tempCh2)
        {
            if ((msRandom.NextDouble() > 0.5) && RssiStable)
            {
                NextCharacter1 = _getCharacterMappedIndex(_tempCh1);
                NextCharacter2 = _getCharacterMappedIndex(_tempCh2);
            }
            else if (RssiStable)
            {
                NextCharacter1 = _getCharacterMappedIndex(_tempCh2);
                NextCharacter2 = _getCharacterMappedIndex(_tempCh1);
            }

            EventAggregator.Instance.GetEvent<StopPlayingCurrentDialogLineEvent>().Publish();
        }


        private static int _getCharacterMappedIndex(int _radioNum)
        {
            int length = DialogViewModel.Instance.CharacterCollection.Count;

            for(int i=0;i<length; i++)
            {
                if(DialogViewModel.Instance.CharacterCollection[i].RadioNum == _radioNum)
                {
                    return i;
                }
            }

            return 0;
        }


        public static int GetNextCharacter(params int[] _indexToSkip)
        {
            int index;
            bool _isIndexTheSame;

            Random random = new Random(); 
            do
            {
                index = random.Next(0, DialogViewModel.Instance.CharacterCollection.Count);
                _isIndexTheSame = false;

                if( _indexToSkip.Length > 0 )
                {
                    if (index == _indexToSkip[0])
                        _isIndexTheSame = true;
                }
            }
            while (!_isSelectedCharacterAvailable(index) || _isIndexTheSame);

            return index;
        }


        private static bool _isSelectedCharacterAvailable(int index)
        {

            return DialogViewModel.Instance.CharacterCollection[index].State == Models.Enums.CharacterState.Available;
        }

        #endregion


        #region - Public methods -

        public static void FindBiggestRssiPair()
        {
            //  This method takes the RSSI values and combines them so that the RSSI for Ch2 looking at 
            //  Ch1 is added to the RSSI for Ch1 looking at Ch2

            int _tempCh1 = 0, _tempCh2 = 0, i = 0, j = 0;

            var _currentTime = DateTime.Now;
            _tempCh1 = NextCharacter1;
            _tempCh2 = NextCharacter2;

            BigRssi = HeatMap[_tempCh1, _tempCh2] + HeatMap[_tempCh2, _tempCh1];  //only pick up new characters if bigRssi greater not =


            for ( i = 0; i < SerialComs.NumRadios; i++)
            {  // the sixth radio is the computer's receiver now included for adventures

                for ( j = i + 1; j < SerialComs.NumRadios; j++)
                {   
                    // only need data above the matrix diagonal

                    if (   HeatMap[i, j] + HeatMap[j, i] > BigRssi 
                        && _currentTime - CharactersLastHeatMapUpdateTime[i] < MaxLastSeenInterval
                        && _currentTime - CharactersLastHeatMapUpdateTime[j] < MaxLastSeenInterval)
                    {  
                        // look at both characters view of each other
                        BigRssi = HeatMap[i, j] + HeatMap[j, i];
                        _tempCh1 = i;
                        _tempCh2 = j;
                    }
                }

            }


            if (_tempCh1 <= DialogViewModel.Instance.CharacterCollection.Count && _tempCh2 <= DialogViewModel.Instance.CharacterCollection.Count)
            {
                _enqueLatestCharacters(_tempCh1, _tempCh2);
                _assignNextCharacters(_tempCh1, _tempCh2);
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
                if (SessionVariables.DebugFlag)
                {


                    if (DialogViewModel.SelectedCharactersOn == 2)
                    {  //two three letter inital sets should be less than7 w space

                        _userHasForcedCharacters = true;


                        NextCharacter1 = DialogViewModel.Instance.SelectedIndex1;

                        NextCharacter2 = DialogViewModel.Instance.SelectedIndex2;

                    }
                }


                Thread.Sleep(1000);


                if (!_userHasForcedCharacters && _nextCharacterSwapTime.CompareTo(DateTime.Now) < 0)
                {

                    NextCharacter1 = GetNextCharacter(); //lower bound inclusive, upper exclusive
                    NextCharacter2 = GetNextCharacter(NextCharacter1); //lower bound inclusive, upper exclusive
                    _nextCharacterSwapTime = DateTime.Now;
                    _nextCharacterSwapTime = _nextCharacterSwapTime.AddSeconds(8 + msRandom.Next(0, 34));
                }



            }
        }

        #endregion



    }
}