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
    /// Model for error message
    /// </summary>
    public class ErrorMessage : LogMessage
    {
        public ErrorMessage(string _message, [CallerFilePath] string _file = "", [CallerLineNumber] int _line = 0) : base(_message)
        {
            SourceFile = Path.GetFileName(_file);
            Line = _line;
        }

    }
}
