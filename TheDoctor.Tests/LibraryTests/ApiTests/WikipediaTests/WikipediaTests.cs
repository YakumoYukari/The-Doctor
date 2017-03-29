using System;
using System.Threading;
using FluentAssert;
using NUnit.Framework;
using TheDoctor.Library.API.Wikipedia;
using TheDoctor.Library.DependencyInjection;

namespace TheDoctor.Tests.LibraryTests.ApiTests.WikipediaTests
{
    [TestFixture]
    public class WikipediaTests
    {
        [Test]
        public void GetsCorrectLink()
        {
            var Api = IoC.Get<IWikiApi>();

            Api.GetWikipediaLink("carbon dioxide").AbsoluteUri
                .ShouldBeEqualTo("https://en.wikipedia.org/wiki/carbon_dioxide");
        }

        [Test]
        public void FetchesArticleThatExists()
        {
            var Api = IoC.Get<IWikiApi>();

            string Html;
            Api.TryGetArticle("carbon dioxide", out Html).ShouldBeTrue();
            Html.ShouldNotBeNull();

            Thread.Sleep(1000);

            Html = null;
            var Url = new Uri("https://en.wikipedia.org/wiki/Boise_National_Forest");
            Api.TryGetArticle(Url, out Html)
                .ShouldBeTrue();

            Html.ShouldNotBeNull();
        }

        [Test]
        public void DoesNotFetchArticleThatDoesNotExist()
        {
            var Api = IoC.Get<IWikiApi>();

            string Html;
            Api.TryGetArticle("flar garblllll", out Html).ShouldBeFalse();
            Html.ShouldBeNull();

            Thread.Sleep(1000);

            Html = null;
            var Url = new Uri("https://en.wikipedia.org/wiki/Boise_National_Forest_Explodes_into_Pieces");
            Api.TryGetArticle(Url, out Html).ShouldBeFalse();

            Html.ShouldBeNull();
        }
    }
}
