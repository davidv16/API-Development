using System;
using System.Security.Authentication;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using RabbitMQ.Client;
using System.Text;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        private IModel _channel;
        private IConfiguration _configuration;
        private IConnection _connection;

        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "pass"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }


        public void Dispose()
        {
            _connection.Close();
            _channel.Close();
        }

        public void PublishMessage(string routingKey, object body)
        {
            var messageBrokerSection = _configuration.GetSection("MessageBroker");
            _channel.BasicPublish(
                exchange: messageBrokerSection.GetSection("ExchangeName").Value,
                routingKey,
                mandatory: true,
                basicProperties: null,
                body: ConvertJsonToBytes(body)
            );
        }
        private byte[] ConvertJsonToBytes(object obj) => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
    }
}