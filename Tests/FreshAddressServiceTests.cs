using System;
using System.Configuration;
using System.Net.Http;
using FluentAssertions;
using Moq;
using Nop.Core.Domain.Configuration;
using Nop.Plugin.Misc.FreshAddressIntegration.Services;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using NUnit.Framework;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Tests
{
    public class FreshAddressServiceTests
    {
        private IFreshAddressService _service;
        private IFreshAddressService _serviceInvalid;

        private Mock<ISettingService> _settingService = new Mock<ISettingService>();
        private Mock<ISettingService> _settingServiceInvalid = new Mock<ISettingService>();

        [SetUp]
        public void Setup()
        {
            _settingService.Setup(s => s.GetSetting(It.IsAny<string>(), 0, false))
                           .Returns(new Setting()
                           {
                               Value = "test setting"
                           });

            _service = new FreshAddressService(
                new Mock<ILogger>().Object,
                _settingService.Object
            );
            _serviceInvalid = new FreshAddressService(
                new Mock<ILogger>().Object,
                _settingServiceInvalid.Object
            );
        }

        [Test]
        public void Loads_Settings_On_Initialization()
        {
            _settingService.Verify(x => x.GetSetting(It.IsAny<string>(), 0, false), Times.AtLeastOnce);
        }

        [Test]
        public void Validate_Email_Throws_ConfigException()
        {
            Action act = () => _serviceInvalid.ValidateEmail("a@a.com");
            act.Should().Throw<ConfigurationErrorsException>();
        }

        [Test]
        public void Validate_Email_Throws_HttpException()
        {
            Action act = () => _service.ValidateEmail("a@a.com");
            act.Should().Throw<HttpRequestException>();
        }
    }
}