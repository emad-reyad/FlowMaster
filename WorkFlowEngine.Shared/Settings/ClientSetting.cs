namespace WorkFlowEngine.Shared.Settings
{
    public class ClientSettings
    {
        public static string SectionName = "ClientSettings";
        public List<Client> Clients { get; set; }
    }
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
