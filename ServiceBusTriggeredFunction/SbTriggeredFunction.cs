using GenericServiceBusHelper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace ServiceBusTriggeredFunction
{
    public static class SbTriggeredFunction
    {
        [FunctionName("TriggeredFunction")]
        public static void Run([ServiceBusTrigger("test", Connection = "AzureWebJobsServiceBus")]
            ServiceBusTestObject myQueueItem, TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}