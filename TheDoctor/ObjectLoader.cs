using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TheDoctor
{
    public class ObjectLoader<TInterface> where TInterface : IMassInstantiable
    {
        public IEnumerable<TInterface> GetAll()
        {
            return Assembly.GetCallingAssembly()
                .GetExportedTypes()
                .Where(T => !T.IsInterface && !T.IsAbstract)
                .Where(T => typeof(TInterface).IsAssignableFrom(T))
                .Select(T => (TInterface) Activator.CreateInstance(T));
        }
    }
}
