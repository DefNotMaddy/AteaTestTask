using Core.Enums;

namespace Core.Types.ConfigurationTypes
{
    public class SectionAOptions
    {
        public const string Section = "Configuration:SectionA";
        public string? Id { get; set; }
        public Uri? Uri { get; set; }
        public AuthType AuthType { get; set; }
    }
}
