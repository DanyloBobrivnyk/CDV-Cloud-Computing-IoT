using System;
using Shared;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "localhost";
            
            var rabbitMQManager = new RabbitMqManager(host);

            bool newMessage = false;

            var connection = rabbitMQManager.Factory.CreateConnection();
            var channel = connection.CreateModel();

            rabbitMQManager.SubscribeQueue(channel, QueueNames.HELLO_WORLD, (message) =>
            {
                Console.WriteLine($">>> Received message: '{message}' <<<");
                newMessage = true;
            });

            Console.WriteLine("Welcome in ClientOne application.");

            while(true)
            {
                if(newMessage)
                {
                    Console.Write("Enter your login or quit via 'q': ");
                    var userMessage = Console.ReadLine();
                    if(userMessage == "q")
                    {
                        return;
                    }
                    rabbitMQManager.SendMessage(QueueNames.HELLO_MIKE, userMessage);
                    newMessage = false;
                }

            }
        }
    }
}
