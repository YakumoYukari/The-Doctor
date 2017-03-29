using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Parameters;

namespace TheDoctor.Library.DependencyInjection
{
    public static class IoC
    {
        public static StandardKernel Kernel;

        static IoC()
        {
            Kernel = new StandardKernel();
            Kernel.Load(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void Reset()
        {
            Kernel = new StandardKernel();
        }

        public static object Get(Type ResolveType)
        {
            return Kernel.Get(ResolveType);
        }
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static IEnumerable<object> GetAll(Type ResolveType)
        {
            return Kernel.GetAll(ResolveType);
        }
        public static IEnumerable<T> GetAll<T>()
        {
            return Kernel.GetAll<T>();
        }

        public static bool CanResolve(Type ResolveType)
        {
            var Request = Kernel.CreateRequest(ResolveType, _ => true, new List<IParameter>(), false, false);
            return Kernel.CanResolve(Request);
        }
        public static bool CanResolve<T>()
        {
            return CanResolve(typeof(T));
        }

        public static IBindAs Bind(Type From, Type To)
        {
            Kernel.Bind(From).To(To);
            return new BindAs(From, To);
        }
        public static IBindAs Bind<TFrom, TTo>()
        {
            Kernel.Bind(typeof(TFrom)).To(typeof(TTo));
            return new BindAs(typeof(TFrom), typeof(TTo));
        }

        public static IBindAs BindSelf(Type From)
        {
            Kernel.Bind(From).ToSelf();
            return new BindAs(From, From);
        }
        public static IBindAs BindSelf<TFrom>()
        {
            Kernel.Bind(typeof(TFrom)).ToSelf();
            return new BindAs(typeof(TFrom), typeof(TFrom));
        }

        public static IBindAs Rebind(Type From, Type To)
        {
            Kernel.Rebind(From).To(To);
            return new BindAs(From, To);
        }
        public static IBindAs Rebind<TFrom, TTo>()
        {
            Kernel.Rebind(typeof(TFrom)).To(typeof(TTo));
            return new BindAs(typeof(TFrom), typeof(TTo));
        }

        public static void Unbind(Type From)
        {
            Kernel.Unbind(From);
        }
        public static void Unbind<T>()
        {
            Kernel.Unbind<T>();
        }
    }
}
