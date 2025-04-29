namespace WorkFlowEngine.Application.Abstractions.Authontication
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid userId, string firstName, string lastName);
    }
}
