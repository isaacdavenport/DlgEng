//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Linq;
using System.Windows;
using DialogEngine.Helpers;
using DialogEngine.Models.Dialog;
using DialogEngine.ViewModels.Dialog;

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

#pragma warning disable 1591
        public static void PrintHeatMap()
#pragma warning restore 1591
        {

            DialogViewModel.Instance.HeatMap = SelectNextCharacters.HeatMap;

            DialogViewModel.Instance.Character1Prefix = DialogViewModel.Instance.CharacterCollection[SelectNextCharacters.NextCharacter1].CharacterPrefix;

            DialogViewModel.Instance.Character2Prefix = DialogViewModel.Instance.CharacterCollection[SelectNextCharacters.NextCharacter2].CharacterPrefix;

            DialogViewModel.Instance.RSSIsum = SelectNextCharacters.BigRssi;

            DialogViewModel.Instance.RSSIstable = SelectNextCharacters.RssiStable;

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
