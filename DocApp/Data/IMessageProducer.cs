using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace DocApp.Data;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}

public class RabbitMQMessageProducer : IMessageProducer
{
    public static readonly string QueueName = "statistic";
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory {HostName = "rabbitmq", UserName = "user", Password = "1234"};
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(QueueName, exclusive: false);
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: QueueName, body: body);
    }
}

public class RequestStatistic()
{
    public string Path { get; set; }
}