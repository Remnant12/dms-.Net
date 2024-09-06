using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using DriverService1.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DriverService1.Service;

public class RabbitMqListenerService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly SharedStateService _sharedStateService;

    public RabbitMqListenerService(IConnection connection, SharedStateService sharedStateService)
    {
        _connection = connection;
        _channel = _connection.CreateModel();
        _sharedStateService = sharedStateService;

    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"Method triggered by RabbitMqListenerService");

        // _channel.QueueDeclare(queue: "user.created", durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueDeclare(queue: "user.created", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received message: {message}");

            try
            {
                var userCreatedEvent = JsonSerializer.Deserialize<UserCreatedEvent>(message);

                // Call the service method to create a driver with the UserId
                if (userCreatedEvent != null)
                {
                    Console.WriteLine($"UserId received: {userCreatedEvent.UserId}");
                    _sharedStateService.UserId = userCreatedEvent.UserId;
                }
                else
                {
                    Console.WriteLine("Failed to deserialize message to UserCreatedEvent.");
                }
            }
            catch (JsonException exception)
            {
                Console.WriteLine($"Error deserializing message: {exception.Message}");

            }
        };

        _channel.BasicConsume(queue: "user.created", autoAck: true, consumer: consumer);
        
        return Task.CompletedTask;
    }
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}