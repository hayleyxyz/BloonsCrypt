using System;
using System.Collections.Generic;
using System.Text;

namespace btd5crypt
{
    internal class SaveMalformedException : Exception
    {
        public SaveMalformedException(string message) : base(message)
        {
        }
    }
}
