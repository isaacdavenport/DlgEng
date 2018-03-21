//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;

namespace DialogEngine.Models.Dialog
{
    public class ReceivedMessage
    {
        public string CharacterPrefix = "XX";
        public DateTime ReceivedTime = DateTime.MinValue;
        public int[] Rssi = new int[SerialComs.NumRadios];
        public int SequenceNum = -1;
    }
}
