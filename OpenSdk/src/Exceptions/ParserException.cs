using System;

namespace OpenSdk.Exceptions;

public class ParserException : Exception
{
    public ParserException(string message) : base(message)
    {
    }
}