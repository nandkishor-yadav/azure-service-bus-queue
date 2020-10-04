using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace AzureServiceBusDemo.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailSenderOptions Options { get; }

        public Task SendEmailAsync(string emails, string subject, string message)
        {
            return Execute(Environment.GetEnvironmentVariable("SENDEMAILDEMO_ENVIRONMENT_SENDGRID_KEY"), subject, message, emails);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {

                From = new EmailAddress("youremail@gmail.com", "AzureServiceBus"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }


    }
}
