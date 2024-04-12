using System;
using System.Collections.Generic;

namespace BlazorAppForClient.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; }

        public ValidationException() : this(default)
        {
        }

        public ValidationException(Dictionary<string, List<string>> errors) : base() =>
            Errors = errors;
    }
}
