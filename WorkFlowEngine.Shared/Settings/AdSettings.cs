namespace WorkFlowEngine.Shared.Settings
{
    public class AdSettings
    {
        public static string SectionName = "AdSetting";
        public string Path { get; init; } = null!;
        public string User { get; init; } = null!;

        public string Password { get; init; }

        public string SearchFilterPostfix { get; init; } = null!;
    }
}
