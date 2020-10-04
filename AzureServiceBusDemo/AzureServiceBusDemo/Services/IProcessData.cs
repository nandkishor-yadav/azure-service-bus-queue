using AzureServiceBusDemo.Models;
using System.Threading.Tasks;

namespace AzureServiceBusDemo.Services
{
    public interface IProcessData
    {
        Task Process(PayloadForServiceBus messages);
    }
}
