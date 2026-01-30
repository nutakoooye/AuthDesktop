namespace AuthDesktop.ViewModels;

public record LogPasViewModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    
    public LogPasViewModel(string login, string password)
    {
        Login = login;
        Password = password;
    }
}