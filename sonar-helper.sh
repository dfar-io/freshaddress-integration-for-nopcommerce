# Marks all non-plugin .NET projects as skipped for Sonar
# to clean up scan results

for file in $(find ../.. -type f -name "*.csproj" -not -path "../../Plugins/tests/Nop.Plugin.Misc.FreshAddressIntegration/Nop.Plugin.Misc.FreshAddressIntegration.Tests.csproj" -not -path "../../Plugins/src/Nop.Plugin.Misc.FreshAddressIntegration/Nop.Plugin.Misc.FreshAddressIntegration.csproj")
do
    echo "processing $file"
    sed -i -- 's/<\/Project>//' $file > /dev/null
    echo -e "<PropertyGroup>\n<SonarQubeExclude>true</SonarQubeExclude>\n</PropertyGroup>\n</Project>" >> $file
done