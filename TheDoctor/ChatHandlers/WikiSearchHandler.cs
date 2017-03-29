using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using RestSharp.Extensions.MonoHttp;

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

        private static async Task FetchWikiArticle(MessageEventArgs Event, string SearchTerm)
        {
            string UrlSafe = GetWikipediaLink(SearchTerm);

            using (var Web = new WebClient())
            {
                try
                {
                    string Html = await Web.DownloadStringTaskAsync(new Uri(UrlSafe));
                    if (Html.Contains("Wikipedia does not have an article with this exact name."))
                    {
                        await ReportNoArticle(Event, SearchTerm);
                    }

                    await Event.Channel.SendMessage(UrlSafe);
                }
                // ReSharper disable once UnusedVariable
                catch (WebException Ex)
                {
                    await ReportNoArticle(Event, SearchTerm);
                }
            }
        }

        private static async Task ReportNoArticle(MessageEventArgs Event, string SearchTerm)
        {
            await Event.Channel.SendMessage($"No Wikipedia article found for: {SearchTerm}");
        }

        private static string GetWikipediaLink(string SearchTerm)
        {
            return $"https://en.wikipedia.org/wiki/{HttpUtility.UrlEncode(SearchTerm.Replace(" ", "_"))}";
        }

        private bool CanRequestAgain()
        {
            return DateTime.UtcNow - _LastRequestTime > TimeSpan.FromSeconds(2);
        }
    }
}
