using System.Configuration;
using Discord;
using TheDoctor.ChatHandlers;

namespace TheDoctor
{
    public class DoctorBot
    {
        public DiscordClient Client { get; }
        private readonly MessageManager _Handler;

        public DoctorBot()
        {
            Client = new DiscordClient();
            _Handler = new MessageManager();
            new CommandBuilder(this, Client).RegisterCommands();

            HookMessageEvents();
        }

        private bool CanSpeak()
        {
            return !IsMuted;
        }

        private void HookMessageEvents()
        {
            Client.MessageReceived += async (Sender, Event) =>
            {
                if (!Event.Message.IsAuthor && CanSpeak())
                    await _Handler.HandleMessage(Sender, Event);
            };
        }

        public void Run()
        {
            Client.ExecuteAndWait(async () =>
            {
                await Client.Connect(ConfigurationManager.AppSettings["AuthenticationToken"], TokenType.Bot);
            });
        }

        public bool IsMuted { get; set; }
        public void Mute() => IsMuted = true;
        public void Unmute() => IsMuted = false;
    }
}
