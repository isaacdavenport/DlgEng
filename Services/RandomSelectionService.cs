﻿

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

namespace DialogEngine.Services
{
    public class RandomSelectionService
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Random msRandom = new Random();
        public static int NextCharacter1 = 1;
        public static int NextCharacter2 = 2;

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


        public async static Task OccasionallyChangeToRandNewCharacterAsync(CancellationToken _cancellationToken)
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


                    while (true)
                    {
                        _cancellationToken.ThrowIfCancellationRequested();

                        _userHasForcedCharacters = false;

                        if (SessionHelper.DebugFlag)
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
                catch (OperationCanceledException ex)
                {
                    mcLogger.Error("OccasionallyChangeToRandNewCharacterAsync " + ex.Message);
                }
                catch (Exception ex)
                {
                    mcLogger.Error("OccasionallyChangeToRandNewCharacterAsync " + ex.Message);
                    DialogDataHelper.AddMessage(new ErrorMessage("Error in random selection of characters."));
                }
            });
        }

        #endregion
    }
}
