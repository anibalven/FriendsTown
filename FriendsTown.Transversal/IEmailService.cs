namespace FriendsTown.Transversal;

public interface IEmailService
{
    void SendMail(string from, string to, string subject, string message);
}

