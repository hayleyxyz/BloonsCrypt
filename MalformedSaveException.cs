using System;

namespace btd5crypt
{
    internal class MalformedSaveException : Exception
    {
        public MalformedSaveException(string message) : base(message)
        {
        }
    }
}
