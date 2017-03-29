using Discord;
using Discord.Commands;
using TheDoctor.Commands;

namespace TheDoctor
{
    public class CommandBuilder : ICommandBuilder
    {
        public const string Args = "Args";
        
        private readonly CommandService _Commands;

        public CommandBuilder(DiscordClient Client)
        {
            _Commands = Client.AddService(
                new CommandService(new CommandServiceConfigBuilder {HelpMode = HelpMode.Public, PrefixChar = '!'}));
        }

        public void RegisterCommands(IBot ToBot)
        {
            var Commands = new ObjectLoader<IBotCommand>().GetAll();
            foreach (var Command in Commands)
            {
                _Commands.CreateCommand(Command.Command)
                    .Description(Command.Description)
                    .Parameter("Args", ParameterType.Unparsed)
                    .Do(Event => Command.Function(Event, ToBot));
            }
        }
    }
}
