using System;
using System.Net;
using System.Web;
using TheDoctor.Library.Web;

namespace TheDoctor.Library.API.Wikipedia
{
    public class WikiApi : IWikiApi
    {
        private readonly IHtmlDownloader _HtmlDownloader;

        public WikiApi(IHtmlDownloader HtmlDownloader)
        {
            _HtmlDownloader = HtmlDownloader;
        }

        public Uri GetWikipediaLink(string SearchTerm)
        {
            return new Uri($"https://en.wikipedia.org/wiki/{HttpUtility.UrlEncode(SearchTerm.Replace(" ", "_"))}");
        }

        public bool TryGetArticle(string SearchTerm, out string Html)
        {
            return TryGetArticle(GetWikipediaLink(SearchTerm), out Html);
        }

        public bool TryGetArticle(Uri Url, out string Html)
        {
            try
            {
                Html = _HtmlDownloader.GetHtml(Url);
                return !Html.Contains("Wikipedia does not have an article with this exact name.");
            }
            // ReSharper disable once UnusedVariable
            catch (WebException Ex)
            {
                Html = null;
                return false;
            }
        }
    }
}
