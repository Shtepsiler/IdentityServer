using MassTransit;

namespace BLL.EventBus;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint _endpoint;

    public EventBus(IPublishEndpoint endpoint)
    {
        _endpoint = endpoint;
    }

    public Task PublishAsync<T>(T Message, CancellationToken cancellationToken = default) where T : class
    {
        _endpoint.Publish(Message, cancellationToken);
        return Task.CompletedTask;
    }
}
