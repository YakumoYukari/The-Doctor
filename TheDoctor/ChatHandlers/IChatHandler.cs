using System.Threading.Tasks;
using Discord;

namespace TheDoctor.ChatHandlers
{
    public interface IChatHandler
    {
        bool CanHandle(MessageEventArgs Event);
        Task Handle(MessageEventArgs Event);
    }
}
