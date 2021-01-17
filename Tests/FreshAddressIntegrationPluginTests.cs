using FluentAssertions;
using Moq;
using Nop.Core;
using Nop.Core.Domain.Configuration;
using Nop.Plugin.Misc.FreshAddressIntegration.Services;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using NUnit.Framework;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Tests
{
    public class FreshAddressIntegrationPluginTests
    {
        private FreshAddressIntegrationPlugin _plugin;

        [SetUp]
        public void Setup()
        {
            _plugin = new FreshAddressIntegrationPlugin(
                new Mock<IWebHelper>().Object
            );
        }

        [Test]
        public void Provides_Configuration_Page_Url()
        {
            _plugin.GetConfigurationPageUrl().Should().Be($"Admin/FreshAddressIntegration/Configure");
        }
    }
}