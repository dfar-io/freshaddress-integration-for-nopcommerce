using Nop.Core.Infrastructure.DependencyManagement;
using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.FreshAddressIntegration.Services;
using System.Diagnostics.CodeAnalysis;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return int.MaxValue;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<FreshAddressService>().As<IFreshAddressService>().InstancePerLifetimeScope();
        }
    }
}
