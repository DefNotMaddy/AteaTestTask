using Core.Interfaces;
using System.Text.Json.Serialization;
namespace Core.Dtos.Responses
{
    public class JwtTokenDto(string ResponseMessage) : IResponse
    {
        [JsonPropertyName("Token")]
        public string ResponseMessage { get; set; } = ResponseMessage;
    }
}
