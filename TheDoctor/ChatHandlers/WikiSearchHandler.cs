using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using RestSharp.Extensions.MonoHttp;

namespace TheDoctor.ChatHandlers
{
    public class WikiSearchHandler : IChatHandler
    {
        public bool CanHandle(MessageEventArgs Event)
        {
            return Event.Message.Text.StartsWith("[[")
                   && Event.Message.Text.EndsWith("]]");
        }

        public async Task Handle(MessageEventArgs Event)
        {
            string SearchTerm = Event.Message.Text.TrimStart('[').TrimEnd(']');
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return;

            string UrlSafe = $"https://en.wikipedia.org/wiki/{HttpUtility.UrlEncode(SearchTerm.Replace(" ", "_"))}";

            using (var Web = new WebClient())
            {
                try
                {
                    string Html = await Web.DownloadStringTaskAsync(new Uri(UrlSafe));
                    if (Html.Contains("Wikipedia does not have an article with this exact name."))
                    {
                        await Event.Channel.SendMessage($"No Wikipedia article found for: {SearchTerm}");
                    }

                    await Event.Channel.SendMessage(UrlSafe);
                }
                // ReSharper disable once UnusedVariable
                catch (HttpRequestException Ex)
                {
                    //Disregard 404 errors
                }
            }
        }
    }
}
