//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;


namespace DialogEngine.Models.Dialog
{
    public class HistoricalPhrase
    {
        public int CharacterIndex;
        public string CharacterPrefix = "";
        public string PhraseFile = "";
        public int PhraseIndex;
        public DateTime StartedTime = DateTime.MinValue;
    }
}
