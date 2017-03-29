using System;

namespace TheDoctor.Library.API.Wikipedia
{
    public interface IWikiApi
    {
        Uri GetWikipediaLink(string SearchTerm);
        bool TryGetArticle(string SearchTerm, out string Html);
        bool TryGetArticle(Uri Url, out string Html);
    }
}