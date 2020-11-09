using System.Threading.Tasks;

namespace E_Commerce.MVC.EmailServices
{
    public interface IEmailSender
    {
        // smtp => gmail,hotmail

        // api => sendgrid
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}