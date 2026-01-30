namespace AuthDesktop.ViewModels.Messages;

public class RegistrationClosedMessage(LoginCredsViewModel loginCreds)
{
    public LoginCredsViewModel LoginCreds { get; } = loginCreds;
}