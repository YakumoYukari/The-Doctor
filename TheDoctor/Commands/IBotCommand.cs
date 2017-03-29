﻿using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace TheDoctor.Commands
{
    public interface IBotCommand : IMassInstantiable
    {
        string Command { get; }
        string Description { get; }
        Func<CommandEventArgs, DoctorBot, Task> Function { get; }
    }
}
