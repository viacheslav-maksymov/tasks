namespace Tasks.API.Models.Authentication
{
    public sealed class AuthenticationRequestBody
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
