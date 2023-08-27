namespace Tasks.API.Services.Interfaces
{
    public interface ITokenManager
    {
        string GetToken(string userId, string email);
    }
}
