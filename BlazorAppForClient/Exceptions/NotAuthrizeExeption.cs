namespace BlazorAppForClient.Exceptions
{
    public class NotAuthrizeExeption : Exception
    {
        public string Error { get; set; }

        public NotAuthrizeExeption() : this(default)
        {
        }

        public NotAuthrizeExeption(string error) : base() =>
            Error = error;
    }
}
