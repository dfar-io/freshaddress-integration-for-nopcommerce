using System;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace Nop.Plugin.Misc.FreshAddressIntegration
{
    public class FreshAddressIntegrationPlugin : BasePlugin, IMiscPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;

        public FreshAddressIntegrationPlugin(IWebHelper webHelper, ISettingService settingService)
        {
            _webHelper = webHelper;
            _settingService = settingService;
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/FreshAddressIntegration/Configure";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource(Constants.CompanyIdLocaleKey, "Company ID");
            this.AddOrUpdatePluginLocaleResource(Constants.ContractIdLocaleKey, "Contract ID");

            base.Install();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override void Uninstall()
        {
            DeleteSetting(Constants.CompanyIdSettingKey);
            DeleteSetting(Constants.ContractIdSettingKey);

            this.DeletePluginLocaleResource(Constants.CompanyIdLocaleKey);
            this.DeletePluginLocaleResource(Constants.ContractIdLocaleKey);

            base.Uninstall();
        }

        private void DeleteSetting(string key)
        {
            var setting = _settingService.GetSetting(key);
            if (setting != null)
            {
                _settingService.DeleteSetting(setting);
            }
        }
    }
}