using Newtonsoft.Json;
using Nop.Core;
using Nop.Plugin.Misc.FreshAddressIntegration.Models;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Services
{
    public class FreshAddressService : IFreshAddressService
    {
        private readonly string _companyId;
        private readonly string _contractId;

        public FreshAddressService(
            ILogger logger,
            ISettingService settingService
            )
        {
            _companyId = settingService.GetSetting("freshaddressintegration.companyid")?.Value;
            _contractId = settingService.GetSetting("freshaddressintegration.contractId")?.Value;
        }

        public FreshAddressResponse ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(_companyId)) { throw new ConfigurationErrorsException("Company ID is null - please provide a Company ID in Configuration Settings."); }
            if (string.IsNullOrWhiteSpace(_contractId)) { throw new ConfigurationErrorsException("Contract ID is null - please provide a Contract ID in Configuration Settings."); }

            return CallFreshAddressAPI(email);
        }

        private FreshAddressResponse CallFreshAddressAPI(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync("https://rt.freshaddress.biz/v7.2?service=react&company=" + _companyId + "&contract=" + _contractId + "&email=" + email).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Error when making GET request to FreshAddress API: " + response.ReasonPhrase.ToString());
                }

                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var jsonResult = JsonConvert.DeserializeObject<FreshAddressResponse>(content);

                return jsonResult;
            }
        }

    }
}
