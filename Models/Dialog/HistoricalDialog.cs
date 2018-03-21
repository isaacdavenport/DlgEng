//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;

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
