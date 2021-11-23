#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 6.0 -InstallDir ./dotnet6
./dotnet6/dotnet --version
./dotnet6/dotnet publish -c Release -o output

#Prerender the site
npx react-snap

#Replace the duplicate script tags
find . -name "*.html" | while read htmlFile; do
            sed -i 's/<script>var Module; window.__wasmmodulecallback__(); delete window.__wasmmodulecallback__;<\/script><script src="_framework\/dotnet.6.0.0.okun7sem7f.js" defer="" integrity="sha256-LOq52qjRa3+zt3w4UL10rS3UdwHxVQxW0JAsMPO9QAM=" crossorigin="anonymous"><\/script>//g' $htmlFile
         done
