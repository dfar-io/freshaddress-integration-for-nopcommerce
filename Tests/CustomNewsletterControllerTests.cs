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

namespace Nop.Plugin.Misc.FreshAddressIntegration.Tests
{
    public class CustomNewsletterControllerTests
    {
        private CustomNewsletterController _controller;
        private Mock<ILogger> _logger = new Mock<ILogger>();

        private const string InvalidEmail = "Invalid email.";
        private const string SubscribeEmailSent = "Subscribe email sent.";
        private const string UnsubscribeEmailSent = "Unsubscribe email sent.";
        private const string ExistingEmail = "b@b.com";
        private const string FailingEmail = "d@d.it";
        private const string ExceptionEmail = "exception@d.it";

        [SetUp]
        public void Setup()
        {
            var localizationService = new Mock<ILocalizationService>();
            localizationService.Setup(s => s.GetResource("Newsletter.Email.Wrong"))
                               .Returns(InvalidEmail);
            localizationService.Setup(s => s.GetResource("Newsletter.SubscribeEmailSent"))
                               .Returns(SubscribeEmailSent);
            localizationService.Setup(s => s.GetResource("Newsletter.UnsubscribeEmailSent"))
                               .Returns(UnsubscribeEmailSent);

            var workContext = new Mock<IWorkContext>();
            workContext.SetupGet(c => c.WorkingLanguage)
                       .Returns(new Language(){ Id = 1 });

            var newsletterSubscriptionService = new Mock<INewsLetterSubscriptionService>();
            newsletterSubscriptionService.Setup(s => s.GetNewsLetterSubscriptionByEmailAndStoreId(ExistingEmail, It.IsAny<int>()))
                                         .Returns(new NewsLetterSubscription() {
                                            Active = false
                                         });

            var storeContext = new Mock<IStoreContext>();
            storeContext.SetupGet(c => c.CurrentStore).Returns(new Store() { Id = 1 });

            var freshAddressService = new Mock<IFreshAddressService>();
            freshAddressService.Setup(s => s.ValidateEmail(It.IsAny<string>()))
                               .Returns(
                                   new FreshAddressResponse() {
                                       Finding = "V"
                                   }
                               );
            freshAddressService.Setup(s => s.ValidateEmail(FailingEmail))
                               .Returns(
                                   new FreshAddressResponse() {
                                       Finding = "A",
                                       ErrorResponse = "failure"
                                   }
                               );
            freshAddressService.Setup(s => s.ValidateEmail(ExceptionEmail))
                               .Throws(new HttpRequestException());

            _controller = new CustomNewsletterController(
                localizationService.Object,
                workContext.Object,
                newsletterSubscriptionService.Object,
                new Mock<IWorkflowMessageService>().Object,
                storeContext.Object,
                _logger.Object,
                freshAddressService.Object
            );
        }

        [Test]
        public void Reject_Invalid_Email()
        {
            _controller.SubscribeNewsletter("abc", true).Should().BeEquivalentTo(
                new JsonResult(new { Success = false, Result = InvalidEmail })
            );
        }

        [Test]
        public void Send_Subscribe_Email()
        {
            _controller.SubscribeNewsletter("a@a.com", true).Should().BeEquivalentTo(
                new JsonResult(new { Success = true, Result = SubscribeEmailSent })
            );
        }

        [Test]
        public void Send_Subscribe_Email_Existing_Inactive_User()
        {
            _controller.SubscribeNewsletter(ExistingEmail, true).Should().BeEquivalentTo(
                new JsonResult(new { Success = true, Result = SubscribeEmailSent })
            );
        }

        [Test]
        public void Send_Unsubscribe_Email()
        {
            _controller.SubscribeNewsletter(ExistingEmail, false).Should().BeEquivalentTo(
                new JsonResult(new { Success = true, Result = UnsubscribeEmailSent })
            );
        }

        [Test]
        public void Fail_Submission_From_FreshAddress()
        {
            _controller.SubscribeNewsletter(FailingEmail, true).Should().BeEquivalentTo(
                new JsonResult(new { Success = false, Result = "failure" })
            );
        }

        [Test]
        public void Catches_ValidExceptions_And_Logs()
        {
            Action act = () => _controller.SubscribeNewsletter(ExceptionEmail, true);
            act.Should().NotThrow<Exception>();
            _logger.Verify(x => x.Error(It.IsAny<string>(), null, null), Times.Once);
        }
    }
}