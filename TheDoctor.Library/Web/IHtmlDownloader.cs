using System;
using System.Collections.Generic;

namespace TheDoctor.Library.Web
{
    public interface IHtmlDownloader
    {
        string GetHtml(Uri Address);
        IEnumerable<string> GetLinks(Uri Address);
        IEnumerable<string> GetImageSources(Uri Address);
    }
}