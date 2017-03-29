using Discord;
using Discord.Commands;
using TheDoctor.Commands;

namespace TheDoctor
{
    public class CommandBuilder
    {
        public const string Args = "Args";

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
            var Commands = new ObjectLoader<IBotCommand>().GetAll();
            foreach (var Command in Commands)
            {
                _Commands.CreateCommand(Command.Command)
                    .Description(Command.Description)
                    .Parameter("Args", ParameterType.Unparsed)
                    .Do(Event => Command.Function(Event, _Bot));
            }
        }
    }
}
