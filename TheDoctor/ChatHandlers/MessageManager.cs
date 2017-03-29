using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace TheDoctor.ChatHandlers
{
    public class MessageManager
    {
        public async Task HandleMessage(object Sender, MessageEventArgs Event)
        {
            var Handlers = new ObjectLoader<IChatHandler>().GetAll();
            var Handle = Handlers.FirstOrDefault(H => H.CanHandle(Event))?.Handle(Event);

            if (Handle != null)
                await Handle;
        }
    }
}
