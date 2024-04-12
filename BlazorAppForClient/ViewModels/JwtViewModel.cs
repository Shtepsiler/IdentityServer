namespace BlazorAppForClient.ViewModels
{
    public class JwtViewModel
    {
        // TODO remove this property. It's temporary
        public int id { get; set; }
        public string clientName { get; set; }

        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
