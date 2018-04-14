//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using DialogEngine.Services;
using System;

namespace DialogEngine.Models.Dialog
{
    public class ReceivedMessage
    {
        public string CharacterPrefix = "XX";
        public DateTime ReceivedTime = DateTime.MinValue;
        public int[] Rssi = new int[SerialSelectionService.NumRadios];
        public int SequenceNum = -1;
    }
}
