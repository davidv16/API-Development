using System;
using System.Text;
using System.Text.Json;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        private IModel _channel;
        private IConnection _connection;
        private IConfiguration _configuration;
        private string _hostName = Environment.GetEnvironmentVariable("RMQ_HOST");
        private string _userName = Environment.GetEnvironmentVariable("RMQ_USER");
        private string _password = Environment.GetEnvironmentVariable("RMQ_PASS");

        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _hostName != null ? _hostName : "localhost",
                UserName = _userName != null ? _userName : "guest",
                Password = _password != null ? _password : "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            // Dispose the connection and channel
            _channel.Close();
            _connection.Close();
        }

        public void PublishMessage(string routingKey, object body)
        {
            // Publishes a message to the message broker using a routing key and a body.
            // Serialize the object to JSON.
            // Publish the message using a channel created with the Rabbit MQ client.

            var messageBrokerSection = _configuration.GetSection("MessageBroker");
            _channel.BasicPublish(
                exchange: messageBrokerSection.GetSection("ExchangeName").Value,
                routingKey,
                mandatory: true,
                basicProperties: null,
                body: ConvertJsonToBytes(body));
        }

        private byte[] ConvertJsonToBytes(object obj) => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
    }
}