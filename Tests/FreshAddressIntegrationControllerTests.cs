using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Stores;
using Nop.Plugin.Misc.FreshAddressIntegration.Controllers;
using Nop.Plugin.Misc.FreshAddressIntegration.Models;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using NUnit.Framework;
using Nop.Plugin.Misc.FreshAddressIntegration.Services;
using System.Net.Http;
using Nop.Services.Configuration;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Tests
{
    public class FreshAddressIntegrationControllerTests
    {
        private FreshAddressIntegrationController _controller;

        private Mock<ISettingService> _settingService = new Mock<ISettingService>();
        private Mock<INotificationService> _notificationService = new Mock<INotificationService>();

        [SetUp]
        public void Setup()
        {
            _controller = new FreshAddressIntegrationController(
                new Mock<ILocalizationService>().Object,
                _settingService.Object,
                _notificationService.Object
            );
        }

        [Test]
        public void Return_Get()
        {
            _controller.Configure().Should().BeOfType<ViewResult>();
            
            _settingService.Verify(s => s.GetSetting(It.IsAny<string>(), 0, false), Times.AtLeastOnce);
        }

        [Test]
        public void Return_Post()
        {
            var model = new FreshAddressIntegrationModel()
            {
                CompanyId = "123",
                ContractId = "123"
            };
            _controller.Configure(model).Should().BeOfType<ViewResult>();
            
            _settingService.Verify(s => s.GetSetting(It.IsAny<string>(), 0, false), Times.AtLeastOnce);
            _notificationService.Verify(s => s.SuccessNotification(It.IsAny<string>(), true), Times.AtLeastOnce);
        }
    }
}