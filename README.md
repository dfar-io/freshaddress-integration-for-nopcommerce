[![Maintainability](https://api.codeclimate.com/v1/badges/3113d15a8dbae8ed928b/maintainability)](https://codeclimate.com/github/dfar-io/freshaddress-integration-for-nopcommerce/maintainability)
[![Test Coverage](https://api.codeclimate.com/v1/badges/3113d15a8dbae8ed928b/test_coverage)](https://codeclimate.com/github/dfar-io/freshaddress-integration-for-nopcommerce/test_coverage)

# freshaddress-integration-for-nopcommerce
Provides integration with FreshAddress (https://www.freshaddress.com/) and nopCommerce (https://www.nopcommerce.com)

## Getting Started

To develop on this plugin locally, you'll need to set up a NopCommerce instance based on the matching version.

First, download and extract NopCommerce to a directory on your PC.

````
# find the version at https://github.com/nopSolutions/nopCommerce/releases
wget https://github.com/nopSolutions/nopCommerce/releases/download/release-4.30/nopCommerce_4.30_Source.zip
unzip nopCommerce_4.30_Source.zip -d nop430
rm nopCommerce_4.30_Source.zip
````

Next, clone this repository into `Plugins/`.

````
cd nop430/Plugins
git clone https://github.com/dfar-io/freshaddress-integration-for-nopcommerce.git Nop.Plugin.Misc.FreshAddressIntegration
````

Finally, add the plugin to the solution and verify a successful build:

````
dotnet sln ../NopCommerce.sln add Nop.Plugin.Misc.FreshAddressIntegration/Source/Nop.Plugin.Misc.FreshAddressIntegration.csproj
dotnet sln ../NopCommerce.sln add Nop.Plugin.Misc.FreshAddressIntegration/Tests/Nop.Plugin.Misc.FreshAddressIntegration.Tests.csproj
cd ..
dotnet clean && dotnet build && dotnet test
````
