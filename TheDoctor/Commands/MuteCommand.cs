using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace TheDoctor.Commands
{
    public class MuteCommand : IBotCommand
    {
        public string Command { get; } = "shutup";
        public string Description { get; } = "Goes against your doctor's professional advice.";
        public Func<CommandEventArgs, DoctorBot, Task> Function { get; } = async (Event, Bot) => {
            await Event.Channel.SendMessage("Well, I never!");
            Bot.IsMuted = true;
        };

        //Required for reflection to instantiate
        // ReSharper disable once EmptyConstructor
        public MuteCommand() { }
    }
}
