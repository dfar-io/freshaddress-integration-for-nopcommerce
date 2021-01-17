using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nop.Core;
using Nop.Core.Domain.Configuration;
using Nop.Plugin.Misc.FreshAddressIntegration.Controllers;
using Nop.Plugin.Misc.FreshAddressIntegration.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using NUnit.Framework;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Tests
{
    public class CustomNewsletterControllerTests
    {
        private CustomNewsletterController _controller;

        private const string InvalidEmail = "Invalid email.";

        [SetUp]
        public void Setup()
        {
            var localizationService = new Mock<ILocalizationService>();
            localizationService.Setup(s => s.GetResource("Newsletter.Email.Wrong"))
                               .Returns(InvalidEmail);

            _controller = new CustomNewsletterController(
                localizationService.Object,
                new Mock<IWorkContext>().Object,
                new Mock<INewsLetterSubscriptionService>().Object,
                new Mock<IWorkflowMessageService>().Object,
                new Mock<IStoreContext>().Object,
                new Mock<ILogger>().Object,
                new Mock<IFreshAddressService>().Object
            );
        }

        [Test]
        public void Reject_Invalid_Email()
        {
            _controller.SubscribeNewsletter("abc", true).Should().BeEquivalentTo(
                new JsonResult(new { Success = false, Result = InvalidEmail })
            );
        }
    }
}