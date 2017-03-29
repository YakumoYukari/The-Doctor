using System.Configuration;
using Discord;
using TheDoctor.ChatHandlers;

namespace TheDoctor
{
    public class DoctorBot : IBot
    {
        private readonly DiscordClient _Client;
        private readonly IMessageRouter _Handler;

        public DoctorBot(DiscordClient Client, IMessageRouter Handler, ICommandBuilder Commands)
        {
            _Client = Client;
            _Handler = Handler;
            Commands.RegisterCommands(this);

            HookMessageEvents();
        }

        private bool CanSpeak()
        {
            return !IsMuted;
        }

        private void HookMessageEvents()
        {
            _Client.MessageReceived += async (Sender, Event) =>
            {
                if (!Event.Message.IsAuthor && CanSpeak())
                    await _Handler.HandleMessage(Sender, Event);
            };
        }

        public void Run()
        {
            _Client.ExecuteAndWait(async () =>
            {
                await _Client.Connect(ConfigurationManager.AppSettings["AuthenticationToken"], TokenType.Bot);
            });
        }

        public bool IsMuted { get; set; }
        public void Mute() => IsMuted = true;
        public void Unmute() => IsMuted = false;
    }
}
