using System;
using System.Reflection;
using MoMoney.Infrastructure.Container.Windsor;
using MoMoney.Presentation.Core;
using MoMoney.Utility.Core;
using MoMoney.Utility.Extensions;

namespace MoMoney.boot.container.registration
{
    internal class wire_up_the_presentation_modules : ICommand
    {
        readonly IDependencyRegistration registry;

        public wire_up_the_presentation_modules(IDependencyRegistration registry)
        {
            this.registry = registry;
        }

        public void run()
        {
            Assembly
                .GetExecutingAssembly()
                .GetTypes()
                //.Where(x => typeof (IPresentationModule).IsAssignableFrom(x))
                .where(x => typeof (IPresenter).IsAssignableFrom(x))
                .where(x => !x.IsInterface)
                .each(register);
        }

        void register(Type type)
        {
            registry.transient(typeof (IPresenter), type);
        }
    }
}