using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace TheDoctor.Commands
{
    public interface IBotCommand
    {
        string Command { get; }
        string Description { get; }
        Func<CommandEventArgs, IBot, Task> Function { get; }
    }
}
