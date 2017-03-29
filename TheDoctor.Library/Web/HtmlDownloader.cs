using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace TheDoctor.Library.Web
{
    public class HtmlDownloader : IHtmlDownloader
    {
        private const string RequestHeaders = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

        public string GetHtml(Uri Address)
        {
            using (var Web = new WebClient())
            {
                Web.Headers[HttpRequestHeader.UserAgent] = RequestHeaders;
                return Web.DownloadString(Address.AbsoluteUri);
            }
        }

        public IEnumerable<string> GetLinks(Uri Address)
        {
            using (var Web = new WebClient())
            {
                Web.Headers[HttpRequestHeader.UserAgent] = RequestHeaders;
                var Doc = new HtmlDocument();
                Doc.LoadHtml(Web.DownloadString(Address));

                return Doc.DocumentNode.Descendants("a")
                    .Where(N => N.Attributes.Any(A => A.Name == "href"))
                    .Select(N => N.Attributes["href"].Value);
            }
        }

        public IEnumerable<string> GetImageSources(Uri Address)
        {
            using (var Web = new WebClient())
            {
                Web.Headers[HttpRequestHeader.UserAgent] = RequestHeaders;
                var Doc = new HtmlDocument();
                Doc.LoadHtml(Web.DownloadString(Address));

                return Doc.DocumentNode.Descendants("img")
                    .Select(N => N.Attributes["src"]?.Value)
                    .Where(S => !string.IsNullOrWhiteSpace(S));
            }
        }
    }
}