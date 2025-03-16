using System.Text.Json.Serialization;

namespace Application.DTOs.Facebook
{
    public class FacebookUserAccessTokenValidator
    {
        [JsonPropertyName("data")]
        public FacebookUserAccessTokenValidatorData Data { get; set; }
    }

    public class FacebookUserAccessTokenValidatorData
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
