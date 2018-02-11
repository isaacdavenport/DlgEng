//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Threading;
using DialogEngine.Helpers;
using DialogEngine.ViewModels.Dialog;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Reflection;
using System.Timers;
using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Models.Logger;
using log4net;

namespace DialogEngine
{
    public static class SelectNextCharacters
    {
        #region  - Fields -
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private static Random msRandom = new Random();
        private static int[,] msStrongRssiCharacterPairBuf = new int[2, StrongRssiBufDepth];
        

        public const int StrongRssiBufDepth = 12;
        public static readonly TimeSpan MaxLastSeenInterval = new TimeSpan(0, 0, 0, 3, 100);

        public static int BigRssi = 0;
        public static bool RssiStable = false;
        public static int NextCharacter1 = 1;
        public static int NextCharacter2 = 2;
        public static int[,] HeatMap = new int[SerialComs.NumRadios, SerialComs.NumRadios];
        public static DateTime[] CharactersLastHeatMapUpdateTime = new DateTime[SerialComs.NumRadios];

        #endregion

        #region - Private methods -


        private static void _calculateRssiStable(int _ch1, int _ch2)
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

            for (int _i = 0; _i < StrongRssiBufDepth - 2; _i++)
            {
               if((msStrongRssiCharacterPairBuf[0, _i] != msStrongRssiCharacterPairBuf[0, _i + 1] || 
                   msStrongRssiCharacterPairBuf[1, _i] != msStrongRssiCharacterPairBuf[1, _i + 1])
                                                                                                  &&
                  (msStrongRssiCharacterPairBuf[0, _i] != msStrongRssiCharacterPairBuf[1, _i + 1] ||
                   msStrongRssiCharacterPairBuf[1, _i] != msStrongRssiCharacterPairBuf[0, _i+1]))
                {
                    RssiStable = false;
                    break;
                }
            }
        }



        static void _assignNextCharacters(int _tempCh1, int _tempCh2)
        {
            if (!RssiStable)
                return;

            int _nextCharacter1MappedIndex1, _nextCharacter1MappedIndex2;

            if (msRandom.NextDouble() > 0.5)
            {
                 _nextCharacter1MappedIndex1 = _getCharacterMappedIndex(_tempCh1);
                 _nextCharacter1MappedIndex2 = _getCharacterMappedIndex(_tempCh2);
            }
            else
            {
                _nextCharacter1MappedIndex1 = _getCharacterMappedIndex(_tempCh2);
                _nextCharacter1MappedIndex2 = _getCharacterMappedIndex(_tempCh1);
            }

            if (_nextCharacter1MappedIndex1 >= 0 && _nextCharacter1MappedIndex2 >= 0)
            {
                NextCharacter1 = _nextCharacter1MappedIndex1;
                NextCharacter2 = _nextCharacter1MappedIndex2;

                //mcLogger.Debug("start break current dialog");
                 
               if ((NextCharacter1 != DialogTracker.Instance.Character1Num || NextCharacter2 != DialogTracker.Instance.Character2Num) &&
                    (NextCharacter2 != DialogTracker.Instance.Character1Num || NextCharacter1 != DialogTracker.Instance.Character2Num))
                {
                    //break current dialog and restart player
                    //mcLogger.Debug("start StopPlayingCurrentDialogLineEvent ");

                    Application.Current.Dispatcher.BeginInvoke((Action) (() =>
                    {
                        mcLogger.Debug("Finish current line : " + "nc1: " + NextCharacter1 + " nc2: " + NextCharacter2 );

                        EventAggregator.Instance.GetEvent<StopPlayingCurrentDialogLineEvent>().Publish();
                    }));
                    //mcLogger.Debug("start CancellationTokenGenerateDialogSource.Cancel ");
                    DialogViewModel.Instance.CancellationTokenGenerateDialogSource.Cancel();

                    //mcLogger.Debug("start new CancellationTokenSource ");

                    DialogViewModel.Instance.CancellationTokenGenerateDialogSource = new CancellationTokenSource();

                    //mcLogger.Debug("start finish StopPlayingCurrentDialogLineEvent");

                }

                //mcLogger.Debug("end break current dialog");

            }
        }


        private static int _getCharacterMappedIndex(int _radioNum)
        {
            try
            {
                // if we find character return its index , or throw exception if there is no character with 
                //specified radio assigned.  First() - throws exception if no items found
                // FirstOrDefault() - returns first value or default value (for reference type it is null)
                int index = DialogViewModel.Instance.CharacterCollection.Select((c, i) => 
                                new { Character = c, Index = i })
                                .Where(x => x.Character.RadioNum == _radioNum).Select(x => x.Index).First();

                return index;
            }
            catch(Exception)
            {
                MessageBox.Show("No character assigned to radio with number " + _radioNum + " .");

                return -1;
            }
        }


        private static bool _isSelectedCharacterAvailable(int index)
        {
            return DialogViewModel.Instance.CharacterCollection[index].State == Models.Enums.CharacterState.Available;
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Random selection of next available character
        /// </summary>
        /// <param name="_indexToSkip"> Number which must be ignored, so we can avoid the same index of selected characters </param>
        /// <returns> Character index or -1 if there is not available characters </returns>
        public static int GetNextCharacter(params int[] _indexToSkip)
        {
            int index;

            // list with indexes of available characters
            List<int> _allowedIndexes = DialogViewModel.Instance.CharacterCollection.Select(
                                      (c, i) => new { Character = c, Index = i })
                                      .Where(x => x.Character.State == Models.Enums.CharacterState.Available)
                                      .Select(x => x.Index).ToList();


            int result = -1;

            switch (_allowedIndexes.Count)
            {
                case 0:  // no avaialbe characters
                    {
                        MessageBox.Show("No available characters. Please change characters settings.");

                        break;
                    }

                case 1: // 1 available character
                    {
                        // if we don't want duplicate index
                        if (_indexToSkip.Length > 0  && _allowedIndexes[0] == _indexToSkip[0])
                        {
                            break;
                        }
                        else
                        {
                            result = _allowedIndexes[0];
                        }

                        break;
                    }

                default:  // more than 1 available characters 
                    {
                        Random random = new Random();
                        bool _isIndexTheSame;

                        // get random element form list with indexes of available characters
                        do
                        {
                            index = _allowedIndexes[random.Next(0, _allowedIndexes.Count)];

                            _isIndexTheSame = false;

                            if (_indexToSkip.Length > 0)
                            {
                                if (index == _indexToSkip[0])
                                    _isIndexTheSame = true;
                            }
                        }
                        while (_isIndexTheSame);

                        result = index;

                        break;
                    }
            }

            return result ;

        }

        /// <summary>
        /// Finds the closest 2 characters depend on rssi values received from toys
        /// </summary>
        public static void FindBiggestRssiPair()
        {
            //  This method takes the RSSI values and combines them so that the RSSI for Ch2 looking at 
            //  Ch1 is added to the RSSI for Ch1 looking at Ch2
            //mcLogger.Debug("start FindBiggestRssiPair");

            try
            {


            int _tempCh1 = 0, _tempCh2 = 0, i = 0, j = 0;

            var _currentTime = DateTime.Now;
            _tempCh1 = NextCharacter1;
            _tempCh2 = NextCharacter2;

            if (_tempCh1 > SerialComs.NumRadios - 1 || _tempCh2 > SerialComs.NumRadios - 1 || 
                _tempCh1 < 0 || _tempCh2 < 0 || _tempCh1 == _tempCh2)
            {
                _tempCh1 = 0;
                _tempCh2 = 1;
                mcLogger.Debug("NextCharacter Error Ch1 " + _tempCh1 + "  Ch2 " + _tempCh2);
            }

            //only pick up new characters if bigRssi greater not =
            BigRssi = HeatMap[_tempCh1, _tempCh2] + HeatMap[_tempCh2, _tempCh1];  
            
            for ( i = 0; i < SerialComs.NumRadios; i++)
            {  
                // it shouldn't happen often that a character has dissapeared, if so zero him out
                if (_currentTime - CharactersLastHeatMapUpdateTime[i] > MaxLastSeenInterval)
                {
                    for (j = 0; j < SerialComs.NumRadios; j++)
                    {
                        HeatMap[i, j] = 0;
                    }
                }
                for (j = i + 1; j < SerialComs.NumRadios; j++)
                {
                    // only need data above the matrix diagonal
                    if (HeatMap[i, j] + HeatMap[j, i] > BigRssi)
                    {
                        // look at both characters view of each other
                        BigRssi = HeatMap[i, j] + HeatMap[j, i];
                        _tempCh1 = i;
                        _tempCh2 = j;
                    }
                }
            }
                //mcLogger.Debug("finished loops");

                _calculateRssiStable(_tempCh1, _tempCh2);
                //mcLogger.Debug("finished _calculateRssiStable");
                _assignNextCharacters(_tempCh1, _tempCh2);
                //mcLogger.Debug("end FindBiggestRssiPair");
        
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Used for random selection of active characters
        /// </summary>
        /// <param name="_cancellationToken">CancellationToken which is used to detect when we want to break background operation</param>
        /// <returns>Task</returns>
        public async  static Task OccasionallyChangeToRandNewCharacterAsync(CancellationToken _cancellationToken)
        {

            await Task.Run(() =>
            {
                Thread.CurrentThread.Name = "OccasionallyChangeToRandNewCharacterAsyncThread";

                // used for computers with no serial input radio for random, or forceCharacter mode
                // does not include final character the silent schoolhouse, not useful in noSerial mode 

                try
                {
                    bool _userHasForcedCharacters;

                    DateTime _nextCharacterSwapTime = new DateTime();

                    _nextCharacterSwapTime = DateTime.Now;

                    _nextCharacterSwapTime.AddSeconds(12);

                    SerialComs.IsSerialMode = false;


                    if (SessionVariables.UseSerialPort)
                    {
                        string _configPath = System.IO.Path.Combine(System.Environment.CurrentDirectory, "DialogEngine.exe");

                        Configuration _config = ConfigurationManager.OpenExeConfiguration(_configPath);

                        _config.AppSettings.Settings["UseSerialPort"].Value = false.ToString();

                        _config.Save();

                        ConfigurationManager.RefreshSection("appSettings");
                    }


                    while (true)
                    {
                        if (_cancellationToken.IsCancellationRequested)
                        {
                            Thread.CurrentThread.Abort();
                            return;
                        }

                        _userHasForcedCharacters = false;


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
                            int _nextCharacter1Index = GetNextCharacter();
                            int _nextCharacter2Index = GetNextCharacter(_nextCharacter1Index >= 0 ? _nextCharacter1Index : NextCharacter1);

                            NextCharacter1 = _nextCharacter1Index >= 0 ? _nextCharacter1Index : NextCharacter1; //lower bound inclusive, upper exclusive
                            NextCharacter2 = _nextCharacter2Index >= 0 ? _nextCharacter2Index : NextCharacter2; //lower bound inclusive, upper exclusive

                            _nextCharacterSwapTime = DateTime.Now;
                            _nextCharacterSwapTime = _nextCharacterSwapTime.AddSeconds(8 + msRandom.Next(0, 34));
                        }
                    }
                }
                catch (Exception ex)
                {
                    mcLogger.Error("OccasionallyChangeToRandNewCharacterAsync " + ex.Message);

                    DialogViewModel.Instance.AddItem(new ErrorMessage("Error in random selection of characters."));
                }
            });
        }
        #endregion
    }
}