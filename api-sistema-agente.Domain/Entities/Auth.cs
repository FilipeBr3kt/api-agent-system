namespace api_sistema_agente.Domain.Entities;

public partial class Auth
{
    public int Id { get; private set; }
    public string? Login { get; private set; }
    public string? Mail { get; private set; }
    public string? Password { get; private set; }

    public Auth(string login, string mail, string password)
    {
        Login = login;
        Mail = mail;
        SetPassword(password);
    }

    public Auth(string login, string password)
    {
        Login = login;
        Password = password;
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Password);
    }

    public void SetPassword(string password)
    {
        Password = HashPassword(password);
    }
}
