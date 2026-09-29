Runtime binary for Omni app-licensing on UX login:

  1-apply-iis\Service\bin\Linx.Framework.BV.dll   →  IIS Service\bin\Linx.Framework.BV.dll

This file is produced by compiling the source overlay:

  2-overwrite-main\Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj
  Configuration=Release  →  bin\Release\Linx.Framework.BV.dll

Copy that DLL into 1-apply-iis\Service\bin\ then run Copy-ToIis.ps1.

Do NOT copy Portal.dll or Application DLLs. LicenseControl.Validate already runs
inside Linx.Framework.BV on authenticateUser / UpdateToken.

Already present on a normal Service install (reuse, do not replace):

  RestSharp.dll
  Newtonsoft.Json.dll
  Linx.Tools.dll
  Linx.Framework.BV.WebAPI.DS.dll   (controllers unchanged; they load BV.dll)
