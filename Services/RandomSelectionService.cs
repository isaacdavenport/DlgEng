

using DialogEngine.Events;
using DialogEngine.Events.DialogEvents;
using DialogEngine.Events.EventArgs;
using DialogEngine.Helpers;
using DialogEngine.Models.Logger;
using DialogEngine.Models.Shared;
using DialogEngine.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DialogEngine.Services
{
    public class RandomSelectionService : ICharacterSelection
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Random msRandom = new Random();
        public static int NextCharacter1 = 1;
        public static int NextCharacter2 = 2;
        private CancellationTokenSource mCancellationTokenSource;

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
            int result = -1;


            // list with indexes of available characters
            List<int> _allowedIndexes = DialogData.Instance.CharacterCollection.Select(
                                      (c, i) => new { Character = c, Index = i })
                                      .Where(x => x.Character.State == Models.Enums.CharacterState.Available)
                                      .Select(x => x.Index).ToList();


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
                        if (_indexToSkip.Length > 0 && _allowedIndexes[0] == _indexToSkip[0])
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

            return result;
        }


        public  Task Start()
        {
            mCancellationTokenSource = new CancellationTokenSource();

            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "OccasionallyChangeToRandNewCharacterAsyncThread";

                // used for computers with no serial input radio for random, or forceCharacter mode
                // does not include final character the silent schoolhouse, not useful in noSerial mode 

                try
                {
                    bool _isNewCharacterSelected;
                    DateTime _nextCharacterSwapTime = DateTime.Now.AddSeconds(12);
                    
                    while (true)
                    {
                        mCancellationTokenSource.Token.ThrowIfCancellationRequested();

                        _isNewCharacterSelected = false;

                        Thread.Sleep(1000);

                        if(_nextCharacterSwapTime.CompareTo(DateTime.Now) < 0)
                        {
                            switch (DialogViewModel.SelectedCharactersOn)
                            {
                                case 0:
                                    {
                                        int _nextCharacter1Index = GetNextCharacter();
                                        int _nextCharacter2Index = GetNextCharacter(_nextCharacter1Index >= 0 ? _nextCharacter1Index : NextCharacter1);

                                        NextCharacter1 = _nextCharacter1Index >= 0 ? _nextCharacter1Index : NextCharacter1; //lower bound inclusive, upper exclusive
                                        NextCharacter2 = _nextCharacter2Index >= 0 ? _nextCharacter2Index : NextCharacter2; //lower bound inclusive, upper exclusive

                                        _nextCharacterSwapTime = DateTime.Now.AddSeconds(8 + msRandom.Next(0, 14));

                                        _isNewCharacterSelected = true;

                                        break;
                                    }
                                case 1:
                                    {
                                        NextCharacter1 = DialogViewModel.SelectedIndex1;
                                        int _nextCharacter2Index = GetNextCharacter(NextCharacter1);

                                        NextCharacter2 = _nextCharacter2Index >= 0 ? _nextCharacter2Index : NextCharacter2;

                                        _nextCharacterSwapTime = DateTime.Now.AddSeconds(8 + msRandom.Next(0, 14));

                                        _isNewCharacterSelected = true;

                                        break;
                                    }
                                case 2:
                                    {
                                        NextCharacter1 = DialogViewModel.SelectedIndex1;
                                        NextCharacter2 = DialogViewModel.SelectedIndex2;

                                        _nextCharacterSwapTime = DateTime.Now.AddSeconds(8 + msRandom.Next(0, 14));

                                        _isNewCharacterSelected = true;

                                        break;
                                    }
                            }
                        }

                        if (_isNewCharacterSelected)
                        {
                            Application.Current.Dispatcher.BeginInvoke(()=>
                            {
                                EventAggregator.Instance.GetEvent<SelectedCharactersPairChangedEvent>()
                                    .Publish(new SelectedCharactersPairEventArgs() { Character1Index = NextCharacter1, Character2Index = NextCharacter2 });

                            },DispatcherPriority.Send);

                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    mcLogger.Debug("OccasionallyChangeToRandNewCharacterAsync " + ex.Message);
                }
                catch (Exception ex)
                {
                    mcLogger.Error("OccasionallyChangeToRandNewCharacterAsync " + ex.Message);
                    DialogDataHelper.AddMessage(new ErrorMessage("Error in random selection of characters."));
                }
            });
        }

        public void Stop()
        {
            mCancellationTokenSource.Cancel();
        }

        #endregion
    }
}
