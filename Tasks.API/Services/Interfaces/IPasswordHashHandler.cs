namespace Tasks.API.Services.Interfaces
{
    public interface IPasswordHashHandler
    {
        string GetPasswordHash(string password);

        bool VerifyPassword(string password, string hashedPassword);
    }
}
