using Core.Interfaces;
using MediatR;
using System.Text.Json;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJstring<T>(this T obj) where T : IResponse, IRequest
        {
            ArgumentNullException.ThrowIfNull(obj);
            return JsonSerializer.Serialize(obj, obj.GetType());
        }
    }
}
