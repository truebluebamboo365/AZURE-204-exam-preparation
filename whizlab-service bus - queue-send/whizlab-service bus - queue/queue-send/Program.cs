using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace queue_send
{
    class Program
    
    {
        // Remove the queue name
        private static string whizlab_connection_string = "Endpoint=sb://servicebusnamespace-1-endev.servicebus.windows.net/;SharedAccessKeyName=azure_qn;SharedAccessKey=h1HMWOARMResns87rU6nISsbRapM5NboK4rYIKrVi9U=";
        private static string whizlab_queue = "azure_5_qn";
        static async Task Main(string[] args)
        {
            IQueueClient w_queueclient;
            // Create a client connection to the queue
            w_queueclient = new QueueClient(whizlab_connection_string, whizlab_queue);
            Console.WriteLine("Sending Messages");
            for (int i = 1; i <=20; i++)
            {
                // Create a new message
                var whizlab_message = new Message(Encoding.UTF8.GetBytes($"a &amp; b {i}"));
                
                // Send a message to the queue
                await w_queueclient.SendAsync(whizlab_message);
                Console.WriteLine($"Message sent: a &amp; b {i}");
            }
        }

    }
}

