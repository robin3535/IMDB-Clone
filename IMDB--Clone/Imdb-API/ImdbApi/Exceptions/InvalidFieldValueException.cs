using System;

namespace ImdbApi.Exceptions
{
    public class InvalidFieldValueException : Exception
    {
        
            public InvalidFieldValueException() : base() { }
            public InvalidFieldValueException(string message) : base(message) { }
            public InvalidFieldValueException(string message, Exception innerException) : base(message, innerException) { }
        
    }
}
