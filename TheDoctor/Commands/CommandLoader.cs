using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TheDoctor.Commands
{
    public class CommandLoader
    {
        public IEnumerable<IBotCommand> AllCommands()
        {
            return Assembly.GetCallingAssembly()
                .GetExportedTypes()
                .Where(T => !T.IsInterface && !T.IsAbstract)
                .Where(T => typeof(IBotCommand).IsAssignableFrom(T))
                .Select(T => (IBotCommand) Activator.CreateInstance(T));
        }
    }
}
