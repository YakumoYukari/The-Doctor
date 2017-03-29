using Discord;
using Ninject.Modules;
using TheDoctor.ChatHandlers;
using TheDoctor.Library.DependencyInjection;

namespace TheDoctor.DependencyInjection
{
    public class Configuration : NinjectModule
    {
        public Configuration() { }
        public override void Load()
        {
            IoC.BindSelf<DiscordClient>();
            IoC.Bind<IBot, DoctorBot>().AsSingleton();

            IoC.Bind<IMessageRouter, MessageRouter>();
            IoC.Bind<ICommandBuilder, CommandBuilder>();
        }
    }
}