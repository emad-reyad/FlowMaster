namespace WorkFlowEngine.Shared.Settings
{

    public class JwtSettings
    {
        public static string SectionName = "JwtSettings";
        public string Key { get; init; } = null!;
        public string Issuer { get; init; } = null!;

        public int ExpiryMinutes { get; init; }

        public string Audience { get; init; } = null!;

    }
}
