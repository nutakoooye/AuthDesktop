namespace AuthDesktop.ViewModels;

public record LoginCredsViewModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    
    public LoginCredsViewModel(string login, string password)
    {
        Login = login;
        Password = password;
    }
}