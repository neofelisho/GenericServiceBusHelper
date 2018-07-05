using System;
using System.Threading.Tasks;
using GenericServiceBusHelper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace TestFunction
{
    public static class TimerTriggeredFunction
    {
        [FunctionName("EnqueueObject")]
        public static async Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            await ServiceBusHelper.EnqueuePayload(new ServiceBusTestObject { Foo = "123", Bar = 456 });
        }
    }
}