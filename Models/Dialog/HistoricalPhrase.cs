﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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