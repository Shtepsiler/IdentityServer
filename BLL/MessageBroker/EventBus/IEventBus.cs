namespace BLL.MessageBroker.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T Message, CancellationToken cancellationToken = default) where T : class;
    }
}
