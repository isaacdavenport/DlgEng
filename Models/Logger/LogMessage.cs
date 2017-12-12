//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DialogEngine.Models.Logger
{
    /// <summary>
    /// Base class for debug messages
    /// </summary>
    public abstract class LogMessage
    {
        protected LogMessage(string _message)
        {
            Message = _message;
        }

        public string Message { get; set; }  // message text

        public string SourceFile { get; set; }  // file where message generated

        public int    Line { get; set; } // line in file where message generated

    }
}
