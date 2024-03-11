using ProductAndRequests.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductAndRequests.Service
{
    public class RabbitMQService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; 
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "ordersQueue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void PublishOrder(Order order)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order));

            _channel.BasicPublish(exchange: "",
                                  routingKey: "ordersQueue",
                                  basicProperties: null,
                                  body: body);
        }
    }
}