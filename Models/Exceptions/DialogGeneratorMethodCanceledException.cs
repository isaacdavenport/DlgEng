

using System;

namespace DialogEngine.Models.Exceptions
{
    public class DialogGeneratorMethodCanceledException : Exception
    {
        public DialogGeneratorMethodCanceledException() { }

        public DialogGeneratorMethodCanceledException(string message)
            : base(message)
        {

        }
    }
}
