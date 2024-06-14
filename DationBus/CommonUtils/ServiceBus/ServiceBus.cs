using Azure.Messaging.ServiceBus;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommonUtils.ServiceBus
{
    public class ServiceBus : IServiceBus
    {
        private readonly ServiceBusClient client;
        private readonly ServiceBusSender sender;
        private readonly ServiceBusReceiver receiver;

        public ServiceBus(string connectionString, string queueName)
        {
            client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(queueName);
            receiver = client.CreateReceiver(queueName);
        }

        public async Task SendPlainTextMessageAsync(string messageBody)
        {
            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage serviceBusMessage = new(messageBody);

            // send the message
            await sender.SendMessageAsync(serviceBusMessage);
        }

        public async Task<string> ReceiveMessageAsync()
        {
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
            // get the message body as a string
            string body = receivedMessage.Body.ToString();

            //await receiver.CompleteMessageAsync(receivedMessage);

            return body;
        }
    }
}
