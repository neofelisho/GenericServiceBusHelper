using GenericServiceBusHelper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace TestFunction
{
    public static class SbTriggeredFunction
    {
        [FunctionName("SbTriggeredFunction")]
        public static void Run([ServiceBusTrigger("test", AccessRights.Manage, Connection = "AzureWebJobsServiceBus")]
            ServiceBusTestObject myQueueItem, TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}