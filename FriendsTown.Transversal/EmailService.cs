using System.Net.Mail;

namespace FriendsTown.Transversal;

public class EmailService:IEmailService
{
    public void SendMail(string from, string to, string subject, 
        string message)
    {
        MailMessage mailMessage = new MailMessage(from, to, subject, 
            message);
        mailMessage.IsBodyHtml = true;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.DeliveryMethod = SmtpDeliveryMethod
                                    .SpecifiedPickupDirectory;
        smtpClient.PickupDirectoryLocation =    
                            @"C:\FriendsTown\FriendsEmail";
        smtpClient.UseDefaultCredentials = true;

        smtpClient.Send(mailMessage);
    }
}

