using Core.Interfaces;
using System.Text.Json;

namespace Core.Dtos.Responses
{
    public class NotAuthorized : IResponse
    {
        public string ResponseMessage { get; set; }

        public override string? ToString() => JsonSerializer.Serialize(this);
    }
}
