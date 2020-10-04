using AzureServiceBusDemo.Models;
using System.Threading.Tasks;

namespace AzureServiceBusDemo.Services
{
    public class ProcessData : IProcessData
    {
        private readonly IEmailSender _emailSender;
        public ProcessData(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Process(PayloadForServiceBus messages)
        {
            await _emailSender.SendEmailAsync(messages.Email, "Message from Azure Service Bus", messages.Message);
        }
    }
}
