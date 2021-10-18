using System;
using System.IO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace logging_service
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(
                    exchange: "order_exchange",
                    type: "direct",
                    durable: true);

                channel.QueueDeclare(
                    queue: "logging_queue", 
                    durable: true);

                channel.QueueBind(
                    queue: "logging_queue",
                    exchange: "order_exchange",
                    routingKey: "create_order");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    using (StreamWriter stream = File.AppendText("./log.txt"))
                    {
                        stream.WriteLine("log: " + message);
                    }

                    Console.WriteLine(" [x] Received {0}", message);

                };

                channel.BasicConsume(
                    queue: "logging_queue",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
