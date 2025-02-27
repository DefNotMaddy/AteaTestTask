namespace Web.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSection<T>(this IConfiguration config, string sectionName) where T : class, new()
        {
            T result = new();
            config.GetSection(sectionName).Bind(result);
            return result;
        }
    }
}
