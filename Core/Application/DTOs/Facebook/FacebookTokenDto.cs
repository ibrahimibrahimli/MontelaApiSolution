using System.Text.Json.Serialization;

namespace Application.DTOs.Facebook
{
    public class FacebookTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
    }
}
