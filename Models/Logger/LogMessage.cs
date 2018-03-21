//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org


namespace DialogEngine.Models.Logger
{
    /// <summary>
    /// Base class for debug messages
    /// </summary>
    public abstract class LogMessage
    {
        /// <summary>
        /// Set message
        /// </summary>
        /// <param name="_message">Message text</param>
        protected LogMessage(string _message)
        {
            Message = _message;
        }

        /// <summary>
        /// Message text
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// File where message generated
        /// </summary>
        public string SourceFile { get; set; }  

        /// <summary>
        /// Line in file where message generated
        /// </summary>
        public int    Line { get; set; } 

    }
}
