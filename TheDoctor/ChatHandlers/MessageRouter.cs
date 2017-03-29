using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using TheDoctor.Library.DependencyInjection;

namespace TheDoctor.ChatHandlers
{
    public class MessageRouter : IMessageRouter
    {
        private readonly List<IChatHandler> _Handlers;
        public MessageRouter()
        {
            _Handlers = IoC.GetAll<IChatHandler>().ToList();
        }

        public async Task HandleMessage(object Sender, MessageEventArgs Event)
        {
            if (Event.Message.IsAuthor) return;
            var Handlers = _Handlers.Where(H => H.CanHandle(Event));

            foreach (var Handler in Handlers)
            {
                await Handler.Handle(Event);
            }
        }
    }
}
