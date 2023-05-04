using MassTransit;

namespace Masstransit_rabbitMQ_API.Command
{
    public record CommandMessage(long Id, string MessageString);
    public class CommandMessageConsumer : IConsumer<CommandMessage>
    {
        public async Task Consume(ConsumeContext<CommandMessage> context)
        {
            var message = context.Message;
            //do the task needed
        }
    }
}
