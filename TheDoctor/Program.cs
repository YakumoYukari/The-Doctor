using System;
using System.Configuration;
using System.IO;
using TheDoctor.Library.DependencyInjection;

namespace TheDoctor
{
    internal class Program
    {
        private static void Main()
        {
            LogErrorsFor(() =>
            {
                Console.WriteLine("Starting The Doctor...");
                IoC.Get<IBot>().Run();
            });
        }

        private static void LogErrorsFor(Action Try)
        {
            try
            {
                Try();
            }
            catch (Exception Ex)
            {
                File.WriteAllText(ConfigurationManager.AppSettings["LogFile"], $"{Ex.Message}\r\n{Ex.StackTrace}");
            }
        }
    }
}
