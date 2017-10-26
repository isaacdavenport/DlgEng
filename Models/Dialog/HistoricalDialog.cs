using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Dialog
{
    public class HistoricalDialog
    {
        public int Character1;
        public int Character2;
        public bool Completed;
        public int DialogIndex;
        public string DialogName = "";
        public DateTime StartedTime = DateTime.MinValue;
    }
}
