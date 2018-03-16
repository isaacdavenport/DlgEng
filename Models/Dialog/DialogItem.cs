//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

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
