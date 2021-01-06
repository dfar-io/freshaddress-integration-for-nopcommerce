using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.FreshAddressIntegration.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    public class FreshAddressIntegrationController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;

        public FreshAddressIntegrationController(ILocalizationService localizationService, ISettingService settingService)
        {
            _localizationService = localizationService;
            _settingService = settingService;
        }

        public ActionResult Configure()
        {
            var companyId = _settingService.GetSetting(Constants.CompanyIdSettingKey)?.Value;
            var contractId = _settingService.GetSetting(Constants.ContractIdSettingKey)?.Value;

            var model = new FreshAddressIntegrationModel
            {
                CompanyId = companyId,
                ContractId = contractId
            };

            return View("~/Plugins/Misc.FreshAddressIntegration/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public ActionResult Configure(FreshAddressIntegrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }

            _settingService.SetSetting(Constants.CompanyIdSettingKey, model.CompanyId);
            _settingService.SetSetting(Constants.ContractIdSettingKey, model.ContractId);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
    }
}
