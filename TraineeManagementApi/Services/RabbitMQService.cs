using RabbitMQ.Client;
using System.Text.Json;
namespace TraineeManagementApi.Services;

public class RabbitMQProducer
{
    private IConnection _connection;

    public RabbitMQProducer( IConnection connection)
    {
        _connection = connection;   
    }

    public async Task PublishAsync<T>(string exchangeAndRoutingKey, string queue, T message) where T : class
    {
        using var channel = await _connection.CreateChannelAsync();

        string dlxName = $"{exchangeAndRoutingKey}.dlx";
        string dlqName = $"{queue}-dead-letter";
        string deadLetterRoutingKey = $"{exchangeAndRoutingKey}.failed";

        string routingKey = $"{exchangeAndRoutingKey}.requested";
        string exchange = $"{exchangeAndRoutingKey}.exchange";

        await channel.ExchangeDeclareAsync(exchange: dlxName, type: ExchangeType.Direct, durable: true);
        await channel.QueueDeclareAsync(queue: dlqName, durable: true, exclusive: false, autoDelete: false);
        await channel.QueueBindAsync(queue: dlqName, exchange: dlxName, routingKey: deadLetterRoutingKey);

        await channel.ExchangeDeclareAsync(exchange: $"{exchangeAndRoutingKey}.exchange", type: ExchangeType.Topic, durable: true);
        var queueArguments = new Dictionary<string, object?>
        {
            { "x-dead-letter-exchange", dlxName },
            { "x-dead-letter-routing-key", deadLetterRoutingKey }
        };
        await channel.QueueDeclareAsync(
            queue: queue, 
            durable: true,
            exclusive: false, 
            autoDelete: false, 
            arguments: null);

        await channel.QueueBindAsync(
            queue: queue, 
            exchange: exchange, 
            routingKey: routingKey);

        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        var properties = new BasicProperties { DeliveryMode = DeliveryModes.Persistent };

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routingKey,
            mandatory: true,
            basicProperties: properties,
            body: body);
    }

}