using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace TheDoctor.Commands
{
    public class UnmuteCommand : IBotCommand
    {
        public string Command { get; } = "speak";
        public string Description { get; } = "Get professional medical help.";
        public Func<CommandEventArgs, IBot, Task> Function { get; } = async (Event, Bot) => {
            await Event.Channel.SendMessage("What seems to be the problem?");
            Bot.IsMuted = false;
        };

        //Required for reflection to instantiate
        // ReSharper disable once EmptyConstructor
        public UnmuteCommand() { }
    }
}
