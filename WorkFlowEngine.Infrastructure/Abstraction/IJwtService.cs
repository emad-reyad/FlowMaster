namespace WorkFlowEngine.Infrastructure.Abstraction
{
    public interface IJwtService
    {
        public string GenerateToken(Guid userId, string name);
    }
}
