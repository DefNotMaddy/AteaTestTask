using Core.Interfaces;

namespace Core.Types.ConfigurationTypes
{
    public class JwtOptions
    {
        public const string Section = "Configuration:Jwt";
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}
