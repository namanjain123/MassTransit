using MassTransit;

namespace Masstransit_rabbitMQ_API.Consumer
{
    public interface IConsumer<in TMessage> : IConsumer
where TMessage : class
    {
        Task Consume(ConsumeContext<TMessage> context);
    }
    public interface IConsumer { }
}
