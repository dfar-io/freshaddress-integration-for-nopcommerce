using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.FreshAddressIntegration.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class FreshAddressIntegrationController : BasePluginController
    {
        private readonly string CompanyIdSettingKey = "freshaddressintegration.companyid";
        private readonly string ContractIdSettingKey = "freshaddressintegration.contractid";
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly INotificationService _notificationService;

        public FreshAddressIntegrationController(
            ILocalizationService localizationService,
            ISettingService settingService,
            INotificationService notificationService
        )
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _notificationService = notificationService;
        }

        public IActionResult Configure()
        {
            var companyId = _settingService.GetSetting(CompanyIdSettingKey)?.Value;
            var contractId = _settingService.GetSetting(ContractIdSettingKey)?.Value;

            var model = new FreshAddressIntegrationModel();
            model.CompanyId = companyId;
            model.ContractId = contractId;

            return View(
                "~/Plugins/Misc.FreshAddressIntegration/Views/Configure.cshtml",
                model);
        }

        [HttpPost]
        public IActionResult Configure(FreshAddressIntegrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }

            _settingService.SetSetting(CompanyIdSettingKey, model.CompanyId);
            _settingService.SetSetting(ContractIdSettingKey, model.ContractId);

            _notificationService.SuccessNotification(
                _localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
    }
}
