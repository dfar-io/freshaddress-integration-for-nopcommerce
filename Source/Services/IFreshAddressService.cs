using Nop.Plugin.Misc.FreshAddressIntegration.Models;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Services
{
    public interface IFreshAddressService
    {
        FreshAddressResponse ValidateEmail(string email);
    }
}
