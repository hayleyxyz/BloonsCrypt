using System;

namespace BloonsCrypt
{
    internal class MalformedSaveException : Exception
    {
        public MalformedSaveException(string message) : base(message)
        {
        }
    }
}
