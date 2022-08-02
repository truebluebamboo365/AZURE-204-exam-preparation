using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace topic_send
{
    class Program
    {
        private static string whizlab_connection_string = "Endpoint=sb://servicebusnamespace-1-endev.servicebus.windows.net/;SharedAccessKeyName=azure_topic_policy;SharedAccessKey=vv3BMdyrQRVq8eemmxaHIxNT6C7rrKY0ArJL1iMH73E=";
        private static string whizlab_topic = "azure_6_qn";
        private static ITopicClient whizlab_client;
        static async Task Main(string[] args)
        {
            // Create a connection to the topic
            whizlab_client = new TopicClient(whizlab_connection_string, whizlab_topic);

            for (int i = 1; i <= 20; i++)
            {
                // Create a message
                
                var whizlab_message = new Message(Encoding.UTF8.GetBytes($"Message {i}"));
                whizlab_message.MessageId = $"{i}";
                Console.WriteLine($"Message : {i} sent");
                // Send the message to the topic
                await whizlab_client.SendAsync(whizlab_message);
            }
            }
    }
}
