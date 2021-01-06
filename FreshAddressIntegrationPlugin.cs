using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.FreshAddressIntegration
{
    public class FreshAddressIntegrationPlugin : BasePlugin, IMiscPlugin
    {
        private readonly IWebHelper _webHelper;

        public FreshAddressIntegrationPlugin(
            IWebHelper webHelper
        )
        {
            _webHelper = webHelper;
        }

        public override string GetConfigurationPageUrl()
        {
            return
                $"{_webHelper.GetStoreLocation()}Admin/FreshAddressIntegration/Configure";
        }
    }
}
