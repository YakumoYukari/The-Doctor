using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using TheDoctor.Library.API.Wikipedia;
using TheDoctor.Library.DependencyInjection;

namespace TheDoctor.ChatHandlers
{
    public class WikiSearchHandler : IChatHandler
    {
        private DateTime _LastRequestTime;

        public bool CanHandle(MessageEventArgs Event)
        {
            return Regex.IsMatch(Event.Message.Text, @"^.*?\[\[(.*?)\]\].*$");
        }

        public async Task Handle(MessageEventArgs Event)
        {
            string SearchTerm = Regex.Match(Event.Message.Text, @"\[\[(.*?)\]\]").Groups[1].Value;
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return;
            
            if (!CanRequestAgain())
                return;

            _LastRequestTime = DateTime.UtcNow;
            await FetchWikiArticle(Event, SearchTerm);
        }

        private async Task FetchWikiArticle(MessageEventArgs Event, string SearchTerm)
        {
            var Api = IoC.Get<IWikiApi>();

            var UrlSafe = Api.GetWikipediaLink(SearchTerm);

            string Html;
            if (Api.TryGetArticle(UrlSafe, out Html))
            {
                await Event.Channel.SendMessage(UrlSafe.AbsoluteUri);
                return;
            }

            await ReportNoArticle(Event, SearchTerm);
        }

        private async Task ReportNoArticle(MessageEventArgs Event, string SearchTerm)
        {
            await Event.Channel.SendMessage($"No Wikipedia article found for: {SearchTerm}");
        }

        private bool CanRequestAgain()
        {
            return DateTime.UtcNow - _LastRequestTime > TimeSpan.FromSeconds(2);
        }
    }
}
