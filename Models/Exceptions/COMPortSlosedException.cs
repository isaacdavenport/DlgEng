

using System;

namespace DialogEngine.Models.Exceptions
{
    public class COMPortSlosedException: Exception
    {
        public COMPortSlosedException() { }

        public COMPortSlosedException(string message)
            : base(message)
        {

        }
    }
}
