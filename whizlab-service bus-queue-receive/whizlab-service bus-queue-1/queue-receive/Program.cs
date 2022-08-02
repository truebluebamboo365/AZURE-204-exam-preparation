using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace queue_receive
{
    class Program
    {
        private static string whizlab_connection_string = "Endpoint=sb://servicebusnamespace-1-endev.servicebus.windows.net/;SharedAccessKeyName=azure_qn;SharedAccessKey=h1HMWOARMResns87rU6nISsbRapM5NboK4rYIKrVi9U=";
        private static string whizlab_queue = "azure_5_qn";
        private static QueueClient whizlab_client;

        static void Main(string[] args)
        {
            Whizlab_receive().GetAwaiter().GetResult();
        }

        static async Task Whizlab_receive()
        {
            // Create a connection to the queue
            whizlab_client = new QueueClient(whizlab_connection_string, whizlab_queue);

            // Specify the options on how you will receive a message
            var _options = new MessageHandlerOptions(whizlab_exception)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            // Register the function and that will receive the messages
            whizlab_client.RegisterMessageHandler(whizlab_message, _options);
            Console.ReadKey();

        }

        static async Task whizlab_message(Message whizlab_message, CancellationToken _token)
        {
            Console.WriteLine(Encoding.UTF8.GetString(whizlab_message.Body));
            // Receive a message from the queue
            await whizlab_client.CompleteAsync(whizlab_message.SystemProperties.LockToken);
        }

        static Task whizlab_exception(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

    }
}
