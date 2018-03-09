﻿//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Dialog
{

    public class PhraseEntry
    {
        /// <summary>
        /// Represents content which charachter will say
        /// </summary>
        public string DialogStr { get; set; }

        /// <summary>
        /// When you make an audio recording of dialogue, you'll use this to name the file
        /// </summary>
        public string FileName;

        /// <summary>
        ///  G: General Audiences - PG: Parental Guidance Suggested
        /// </summary>
        public string PhraseRating;

        /// <summary>
        /// "Key" represents phrase type. Phrase type determines what situations your character will say the dialogue in
        /// 
        /// <example> 
        /// your character will use a “Greeting” when they want to say
        /// hello or introduce themselves.
        /// </example>
        /// 
        /// "Value" This determines how often the
        /// character will say this phrase, compared to other phrases of the same type. The bigger
        /// the number, the more often they will say this phrase.
        /// </summary>
        public Dictionary<string, double> PhraseWeights; //to replace PhraseWeights, uses string tags.
    }
}
