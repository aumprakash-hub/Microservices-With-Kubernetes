using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient: IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory()
            { HostName = _configuration["RabbitMQHost"], Port = int.Parse(_configuration["RabbitMQPort"]) };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMQConnectionShutdown;
            Console.WriteLine("Connected to MessageBus.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not connect to the Message bus : {ex.Message}");
        }
    }

    private void RabbitMQConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
       Console.WriteLine("RabbitMQ connection shutdown.");
    }

    public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
    {
        var message = JsonSerializer.Serialize(platformPublishedDto);
        if (_connection.IsOpen)
        {
            Console.WriteLine("RabbitMQ connection open, sending messages..");
            SendMessage(message);
        }
        else
        {
            Console.WriteLine("RabbitMQ connection not open, sending messages..");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "trigger", routingKey: "",basicProperties: null, body: body);
        Console.WriteLine($"We have sent {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("Message bus disposed.");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}