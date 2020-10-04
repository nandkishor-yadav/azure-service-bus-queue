using System.Threading.Tasks;

namespace AzureServiceBusDemo.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emails, string subject, string message);
    }
}
