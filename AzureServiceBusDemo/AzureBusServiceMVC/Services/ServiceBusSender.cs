using AzureBusServiceMVC.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AzureBusServiceMVC.Services
{
    public class ServiceBusSender
    {
        private readonly QueueClient _queueClient;
        private readonly IConfiguration _configuration;
        private const string QUEUE_NAME = "sendemailtest";

        public ServiceBusSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _queueClient = new QueueClient("SET-YOUR-CONNECTION-STRING", QUEUE_NAME);
        }

        public async Task SendMessage(PayloadForServiceBus payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }
    }
}
