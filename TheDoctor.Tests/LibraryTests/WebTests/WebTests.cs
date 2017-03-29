using System;
using System.Linq;
using FluentAssert;
using NUnit.Framework;
using TheDoctor.Library.DependencyInjection;
using TheDoctor.Library.Web;

namespace TheDoctor.Tests.LibraryTests.WebTests
{
    [TestFixture]
    public class WebTests
    {
        private IHtmlDownloader _Downloader;

        [SetUp]
        public void Init()
        {
            _Downloader = IoC.Get<IHtmlDownloader>();
        }

        [Test]
        public void CanGetHtml()
        {
            var Url = new Uri("http://www.google.com");
            _Downloader.GetHtml(Url).ShouldNotBeNull();
        }

        [Test]
        public void CanGetLinks()
        {
            var Url = new Uri("http://www.google.com");
            var Links = _Downloader.GetLinks(Url).ToList();

            Links.ShouldNotBeNull();
            Links.Count.ShouldBeGreaterThan(1);
        }

        [Test]
        public void CanGetImages()
        {
            var Url = new Uri("http://www.google.com");
            var Links = _Downloader.GetImageSources(Url);

            Links.ShouldNotBeNull();
        }
    }
}
