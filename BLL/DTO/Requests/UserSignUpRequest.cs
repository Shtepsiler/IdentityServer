using System.Text.Json.Serialization;

namespace BLL.DTO.Requests
{
    public class UserSignUpRequest
    {
        [JsonIgnore]
        public string? refererUrl { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }




    }
}
