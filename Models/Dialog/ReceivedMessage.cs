using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Dialog
{
    public class ReceivedMessage
    {
        public string CharacterPrefix = "XX";
        public DateTime ReceivedTime = DateTime.MinValue;
        public int[] Rssi = new int[SerialComs.NUM_RADIOS];
        public int SequenceNum = -1;
    }
}
