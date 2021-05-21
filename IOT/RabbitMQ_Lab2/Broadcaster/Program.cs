using System;
using Shared;


namespace Broadcaster
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = "localhost";
            bool newMessage = false;
            var rabbitMQManager = new RabbitMqManager(hostName);

            
            var connection = rabbitMQManager.Factory.CreateConnection();
            var channel = connection.CreateModel();

            rabbitMQManager.SubscribeQueue(channel, QueueNames.HELLO_MIKE, (message) =>
            {
                Console.WriteLine($">>> User's login is : '{message}' <<<");
                newMessage = true;
            });

            while (true)
            {
                Console.WriteLine(">>> Press key to continue or type 'q' to exist app. <<<");
                string userMessage = Console.ReadLine();

                if (userMessage == "q")
                    return;

                Console.WriteLine("[Start, sending user invitation.]");
                try
                {
                    rabbitMQManager.SendMessage(QueueNames.HELLO_WORLD, "Hello, user, introduce yourself!");
                    while(newMessage == false)
                    {

                    }

                    newMessage = false;  
                    Console.WriteLine("[Done]");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Something went wrong: {ex.Message}]");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}
