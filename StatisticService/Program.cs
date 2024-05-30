using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

class Program
{
    private static readonly string QueueName = "statistic";

    public static void Main(string[] args)
    {
        Console.WriteLine("Statistic service started ...");
        IConnection? connection;
       ConnectionFactory? factory;
       //Pętla wymusza ponawianie połączenia serwisu z RabbitMQ do czasu zakończenia uruchomienia kolejki
        do
        {
            try
            {
                Console.WriteLine("Trying connect to broker...");
                factory = new ConnectionFactory { HostName = "rabbitmq", Password = "1234", UserName = "user" };
                connection = factory.CreateConnection();
                break;
            }
            catch (BrokerUnreachableException e)
            {
                Console.WriteLine("Waiting for broker...");
                Task.Delay(1000).Wait();
            }
        } while (true);
        Console.WriteLine($"Connected to broker {factory.HostName}");
        using var channel = connection.CreateModel();
        channel.QueueDeclare(QueueName, exclusive: false);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };
        channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
        do
        {
        } while (Console.ReadLine() != "Q");

        Console.WriteLine("StatisticService shutdown ...");
    }
}