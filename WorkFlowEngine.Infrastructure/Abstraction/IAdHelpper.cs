namespace WorkFlowEngine.Infrastructure.Abstraction
{
    public interface IAdHelpper
    {
        public bool CheckUserInGroup(string UserName, string GroupName);
        public List<string> GetUserGroups(string UserName);
    }
}
