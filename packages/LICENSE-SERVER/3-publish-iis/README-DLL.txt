Linx.Framework.BV.dll (Release) goes in:

  3-publish-iis\Service\bin\Linx.Framework.BV.dll

Compile pack 2:

  MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release

Then run Publish-ToIis.ps1. Do not copy Portal.dll or Application DLLs.

Already on a normal Service install (reuse):
  RestSharp.dll  Newtonsoft.Json.dll  Linx.Tools.dll  Linx.Framework.BV.WebAPI.DS.dll

Português: LEIA-ME-DLL.txt
