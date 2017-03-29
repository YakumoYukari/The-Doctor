﻿using Discord;
using Discord.Commands;
using TheDoctor.Commands;

namespace TheDoctor
{
    public class CommandBuilder
    {
        private readonly DoctorBot _Bot;
        private readonly CommandService _Commands;

        public CommandBuilder(DoctorBot Bot, DiscordClient Client)
        {
            _Bot = Bot;
            _Commands = Client.AddService(
                new CommandService(new CommandServiceConfigBuilder {HelpMode = HelpMode.Public, PrefixChar = '!'}));
        }

        public void RegisterCommands()
        {
            var Commands = new CommandLoader().AllCommands();
            foreach (var Command in Commands)
            {
                _Commands.CreateCommand(Command.Command)
                    .Description(Command.Description)
                    .Do(Event => Command.Function(Event, _Bot));
            }
        }
    }
}