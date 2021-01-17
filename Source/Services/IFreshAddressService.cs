using Nop.Plugin.Misc.FreshAddressIntegration.Models;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Services
{
    public interface IFreshAddressService
    {
        FreshAddressResponse ValidateEmail(string email);
    }
}
