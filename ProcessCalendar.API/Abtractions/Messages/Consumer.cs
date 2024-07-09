using Common.Abstractions.Messages;
using MassTransit;
using MediatR;

namespace ProcessCalendar.API.Abtractions.Messages
{
    public abstract class Consumer<TMessage> : IConsumer<IMessage>
        where TMessage : class, INotificationEvent
    {
        private readonly ISender Sender;

        protected Consumer(ISender sender)
        {
            Sender = sender;
        }

        public async Task Consume(ConsumeContext<IMessage> context)
        {
            await Sender.Send(context.Message);
        }
    }
}
