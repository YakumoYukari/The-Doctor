using Ninject.Modules;
using TheDoctor.Library.API.Wikipedia;
using TheDoctor.Library.Web;

namespace TheDoctor.Library.DependencyInjection
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            IoC.Bind<IHtmlDownloader, HtmlDownloader>();
            IoC.Bind<IWikiApi, WikiApi>();
        }
    }
}
