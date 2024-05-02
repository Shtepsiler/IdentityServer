namespace BLL.DTO.Responses
{
    public class JwtResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string Token { get; set; }
    }
}
