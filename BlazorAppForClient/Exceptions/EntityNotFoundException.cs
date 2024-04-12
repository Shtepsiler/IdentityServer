using System;

namespace BlazorAppForClient.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string Error { get; set; }

        public EntityNotFoundException() : this(default)
        {
        }

        public EntityNotFoundException(string error) : base() =>
            Error = error;
    }
}
