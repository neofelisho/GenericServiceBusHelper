using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace GenericServiceBusHelper
{
    public static class ServiceBusHelper
    {
        private const string QueueName = "test";

        /// <summary>
        ///     Service Bus Connection String
        /// </summary>
        public static readonly string ServiceBusConnectionString =
            AmbientConnectionStringProvider.Instance.GetConnectionString(ConnectionStringNames.ServiceBus);

        private static readonly Lazy<QueueClient> QueueClientLazy = new Lazy<QueueClient>(() =>
        {
            if (string.IsNullOrEmpty(ServiceBusConnectionString))
                throw new Exception("Setting of service bus connection string is missing.");
            if (string.IsNullOrEmpty(QueueName))
                throw new Exception("Setting of queue name is missing.");
            return QueueClient.CreateFromConnectionString(ServiceBusConnectionString, QueueName);
        });

        /// <summary>
        ///     Convert message to BrokeredMessage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payload"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static BrokeredMessage ToBrokeredMessage<T>(this T payload, string messageId = null)
        {
            return new BrokeredMessage(payload)
            {
                ContentType = typeof(T).AssemblyQualifiedName,
                MessageId = string.IsNullOrEmpty(messageId) ? payload.ToString() : messageId
            };
        }

        /// <summary>
        ///     Get content (body) of BrokeredMessage
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static object GetPayload(this BrokeredMessage message)
        {
            var type = Type.GetType(message.ContentType, true);

            var stream = message.GetBody<Stream>();
            var serializer = new DataContractSerializer(type);
            var reader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max);
            return serializer.ReadObject(reader);
        }

        public static async Task EnqueuePayload<T>(T payload)
        {
            await QueueClientLazy.Value.SendAsync(payload.ToBrokeredMessage());
        }
    }
}