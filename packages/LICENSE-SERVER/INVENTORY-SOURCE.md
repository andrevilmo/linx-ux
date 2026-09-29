# Inventory — source overwrite pack (`2-overwrite-main`)

Same tree as `Main\`. Overlay onto a clone, then compile. Files are from `origin/ux-license-server`.

## Overwrite (required to compile the gate)

| Path under `2-overwrite-main\` | Action |
|-------------------------------|--------|
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessDecision.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessGuard.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessResult.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessSnapshot.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseException.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerApiClient.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerBootstrap.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerModels.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerSettings.cs` | add |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\Utils\LicenseControl.cs` | replace |

Call sites already in product (`AuthenticateUser` / `UpdateToken` / logout `Remove`) are **unchanged** on this branch. Do not overlay those files.

## csproj

| File | Action |
|------|--------|
| `...\Linx.Framework.BV.csproj.LicenseServer.fragment` | **merge** 9 `<Compile>` lines (`Copy-ToMain.ps1` / `Merge-Csproj.ps1`; safe on MFA/SSO trees) |
| `...\Linx.Framework.BV.csproj.FROM-ux-license-server` | full csproj from the branch; only `-ReplaceCsproj` on master-like trees |

## Tests (optional; compile/run, not IIS)

| Path | Action |
|------|--------|
| `Main\Business\Linx.License.Server.Access.Tests\LicenseAccessDecisionTests.cs` | add |
| `Main\Business\Linx.License.Server.Access.Tests\LicenseServerLiveTests.cs` | add |
| `Main\Business\Linx.License.Server.Access.Tests\Linx.License.Server.Access.Tests.csproj` | add |
| `Main\Business\Linx.License.Server.Access.Tests\Program.cs` | add |
| `Main\Business\Linx.License.Server.Access.Tests\run-mono-tests.sh` | add |

## Config (merge, do not replace)

| Path | Action |
|------|--------|
| `Main\Binary\Service\Web.config.LicenseServer.keys.xml` | merge into `Main\Binary\Service\Web.config` |

## Not in this pack (on purpose)

- Full `Main\Binary\Service\Web.config` (SQL / ServiceBus would be wiped)
- Portal / Application / SPA sources
- `RestSharp` / `Newtonsoft` packages (already referenced by Linx.Framework.BV.csproj)

Hash list of this pack: `2-overwrite-main\FILE-LIST.txt`.

## After overlay

```text
MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release
copy bin\Linx.Framework.BV.dll → IIS Service\bin
```
