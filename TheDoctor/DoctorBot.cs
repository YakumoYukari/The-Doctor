using System.Configuration;
using Discord;
using TheDoctor.ChatHandlers;

namespace TheDoctor
{
    public class DoctorBot
    {
        private readonly DiscordClient _Client;
        private readonly MessageManager _Handler;

        public DoctorBot()
        {
            _Client = new DiscordClient();
            _Handler = new MessageManager();
            new CommandBuilder(this, _Client).RegisterCommands();

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
