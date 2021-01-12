# Marks all non-plugin .NET projects as skipped for Sonar
# to clean up scan results

for file in $(find ../.. -type f -name "*.csproj" -not -path "../../Plugins/Nop.Plugin.Misc.FreshAddressIntegration/Source/Nop.Plugin.Misc.FreshAddressIntegration.csproj" -not -path "../../Plugins/Nop.Plugin.Misc.FreshAddressIntegration/Tests/Nop.Plugin.Misc.FreshAddressIntegration.Tests.csproj")
do
    echo "processing $file"
    sed -i -- 's/<\/Project>//' $file > /dev/null
    echo -e "<PropertyGroup>\n<SonarQubeExclude>true</SonarQubeExclude>\n</PropertyGroup>\n</Project>" >> $file
done