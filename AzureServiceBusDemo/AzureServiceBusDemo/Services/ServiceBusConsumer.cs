using AzureServiceBusDemo.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureServiceBusDemo.Services
{
    public class ServiceBusConsumer : IServiceBusConsumer
    {

        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "sendemailtest";
        private readonly IProcessData _processData;

        public ServiceBusConsumer(IProcessData processData)
        {
            _processData = processData;
            _queueClient = new QueueClient("SET-YOUR-CONNECTION-STRING", QUEUE_NAME);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var myPayload = JsonConvert.DeserializeObject<PayloadForServiceBus>(Encoding.UTF8.GetString(message.Body));
            await _processData.Process(myPayload);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _ = exceptionReceivedEventArgs.ExceptionReceivedContext;

            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await _queueClient.CloseAsync();
        }
    }
}
