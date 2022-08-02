using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace topic_receive
{
    class Program
    {
        private static string whizlab_connection_string = "Endpoint=sb://servicebusnamespace-1-endev.servicebus.windows.net/;SharedAccessKeyName=azure_topic_policy;SharedAccessKey=vv3BMdyrQRVq8eemmxaHIxNT6C7rrKY0ArJL1iMH73E=";
        private static string whizlab_subscription = "topicsub";
        private static ISubscriptionClient whizlab_client;
        static void Main(string[] args)
        {
            Whizlab_receive().GetAwaiter().GetResult();
        }

        static async Task Whizlab_receive()
        {
            // Create a connection to the subscription
            ServiceBusConnectionStringBuilder w_builder = new ServiceBusConnectionStringBuilder(whizlab_connection_string);
            whizlab_client = new SubscriptionClient(w_builder, whizlab_subscription);

            // Specify how the message will be received
            var _options = new MessageHandlerOptions(whizlab_exception)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            // Register the function that will process the messages
            whizlab_client.RegisterMessageHandler(whizlab_message, _options);
            Console.ReadKey();

        }

        static async Task whizlab_message(Message whizlab_message, CancellationToken _token)
        {
            
            Console.WriteLine(Encoding.UTF8.GetString(whizlab_message.Body));

            // Receive a message from the topic
            await whizlab_client.CompleteAsync(whizlab_message.SystemProperties.LockToken);
        }

        static Task whizlab_exception(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }
    }
}
