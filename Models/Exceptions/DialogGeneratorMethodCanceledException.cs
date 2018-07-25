

using System;

namespace DialogEngine.Models.Exceptions
{
    public class DialogGeneratorMethodCanceledException : Exception
    {
        public DialogGeneratorMethodCanceledException() : base() { }

        public DialogGeneratorMethodCanceledException(string message)
            : base(message)
        {

        }
    }
}
