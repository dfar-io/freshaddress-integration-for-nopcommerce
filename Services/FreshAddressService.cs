﻿using Newtonsoft.Json;
using Nop.Core;
using Nop.Plugin.Misc.FreshAddressIntegration.Models;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Services
{
    public class FreshAddressService : IFreshAddressService
    {
        private readonly string _companyId;
        private readonly string _contractId;
        private readonly ILogger _logger;
        private readonly ISettingService _settingService;

        public FreshAddressService(
            ILogger logger,
            ISettingService settingService
            )
        {
            _logger = logger;
            _settingService = settingService;

            _companyId = _settingService.GetSetting("freshaddressintegration.companyid")?.Value;
            _contractId = _settingService.GetSetting("freshaddressintegration.contractId")?.Value;
        }

        public FreshAddressResponse ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(_companyId)) { throw new Exception("Company ID is null - please provide a Company ID in Configuration Settings."); }
            if (string.IsNullOrWhiteSpace(_contractId)) { throw new Exception("Contract ID is null - please provide a Contract ID in Configuration Settings."); }

            return CallFreshAddressAPI(email);
        }

        private FreshAddressResponse CallFreshAddressAPI(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync("https://rt.freshaddress.biz/v7.2?service=react&company=" + _companyId + "&contract=" + _contractId + "&email=" + email).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    throw new Exception("Error when making GET request to FreshAddress API: " + response.ReasonPhrase.ToString() + " : " + errorContent);
                }

                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var jsonResult = JsonConvert.DeserializeObject<FreshAddressResponse>(content);

                return jsonResult;
            }
        }

    }
}
