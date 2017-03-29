using Discord;
using Ninject.Modules;
using TheDoctor.ChatHandlers;
using TheDoctor.Commands;
using TheDoctor.Library.DependencyInjection;

namespace TheDoctor.DependencyInjection
{
    public class Configuration : NinjectModule
    {
        public Configuration() { }
        public override void Load()
        {
            IoC.BindSelf<DiscordClient>().AsSingleton();
            IoC.Bind<IBot, DoctorBot>().AsSingleton();

            IoC.Bind<IMessageRouter, MessageRouter>();
            IoC.Bind<ICommandBuilder, CommandBuilder>();

            //Registered commands
            IoC.Bind<IBotCommand, MuteCommand>();
            IoC.Bind<IBotCommand, UnmuteCommand>();
            IoC.Bind<IBotCommand, RecordCommand>();

            //Registered chat handlers
            IoC.Bind<IChatHandler, WikiSearchHandler>();
        }
    }
}