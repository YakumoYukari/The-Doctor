using System;
using System.Threading.Tasks;
using Discord;

namespace TheDoctor
{
    public class MessageHandler
    {
        private readonly string[] _Diseases = {"cancer", "AIDS", "the plague", "the vapors", "yellow fever", "SARS", "consumption", "yeast infection"};
        
        public async Task HandleMessage(object Sender, MessageEventArgs Event)
        {
            await Event.Channel.SendMessage($@"{Event.User.Name} said, ""{Event.Message.Text}"" - it appears to be a case of {_Diseases[new Random().Next(0, _Diseases.Length)]}.");
        }
    }
}
