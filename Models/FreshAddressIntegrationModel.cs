using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Models
{
    public class FreshAddressIntegrationModel
    {
        [NopResourceDisplayName(Constants.CompanyIdLocaleKey)]
        public string CompanyId { get; set; }

        [NopResourceDisplayName(Constants.ContractIdLocaleKey)]
        public string ContractId { get; set; }
    }
}
