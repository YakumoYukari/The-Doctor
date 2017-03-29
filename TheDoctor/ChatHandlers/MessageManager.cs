using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace TheDoctor.ChatHandlers
{
    public class MessageManager
    {
        private readonly List<IChatHandler> _Handlers;
        public MessageManager()
        {
            _Handlers = new ObjectLoader<IChatHandler>().GetAll().ToList();
        }

        public async Task HandleMessage(object Sender, MessageEventArgs Event)
        {
            if (Event.Message.IsAuthor) return;
            var Handle = _Handlers.FirstOrDefault(H => H.CanHandle(Event))?.Handle(Event);

            if (Handle != null)
                await Handle;
        }
    }
}
