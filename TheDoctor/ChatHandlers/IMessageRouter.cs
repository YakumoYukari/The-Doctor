using System.Threading.Tasks;
using Discord;

namespace TheDoctor.ChatHandlers
{
    public interface IMessageRouter
    {
        Task HandleMessage(object Sender, MessageEventArgs Event);
    }
}