using System.Threading.Tasks;

namespace AzureServiceBusDemo.Services
{
    public interface IServiceBusConsumer
    {
        void RegisterOnMessageHandlerAndReceiveMessages();

        Task CloseQueueAsync();
    }
}