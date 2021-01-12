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

        private Mock<ISettingService> _settingService;

        [SetUp]
        public void Setup()
        {
            _settingService = new Mock<ISettingService>();
            _settingService.Setup(s => s.GetSetting(It.IsAny<string>(), 0, false))
                           .Returns(new Setting()
                           {
                               Value = "test setting"
                           });

            _service = new FreshAddressService(
                new Mock<ILogger>().Object,
                _settingService.Object
            );
        }

        [Test]
        public void Loads_Settings_On_Initialization()
        {
            _settingService.Verify(x => x.GetSetting(It.IsAny<string>(), 0, false), Times.AtLeastOnce);
        }
    }
}