using System;

namespace TheDoctor.Library.DependencyInjection
{
    public class BindAs : IBindAs
    {
        private readonly Type _From;
        private readonly Type _To;

        public BindAs(Type From, Type To)
        {
            _From = From;
            _To = To;
        }

        public void AsSingleton()
        {
            IoC.Kernel.Rebind(_From).To(_To).InSingletonScope();
        }
    }
}