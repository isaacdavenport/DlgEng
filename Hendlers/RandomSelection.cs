

using DialogEngine.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DialogEngine.Hendlers
{
    public class RandomSelection
    {
        #region - fields -

        #endregion

        #region - constructor -

        public RandomSelection()
        {

        }

        #endregion

        #region - private functions -

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

        #endregion

        #region - properties -
        #endregion
    }
}
