//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Dialog
{
    /// <summary>
    /// Class represents one dialog line
    /// </summary>
    public class DialogItem
    {
        // Speaker character
        public Character Character { get; set; }

        // Current spekaing phrase
        public PhraseEntry PhraseEntry { get; set; }

    }
}
