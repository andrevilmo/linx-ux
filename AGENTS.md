# AGENTS.md

## Cursor Cloud specific instructions

### Repository reality (read first)
This repo (`linx-ux`, the Linx Framework ERP monorepo) is **overwhelmingly Windows-only .NET Framework 4.6.1/4.8**. The web app (`Main/Application/Linx.Internet.Application`), `Linx.Portal`, and `Linx.ImageService` require **MSBuild on Windows** (or the `mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore` container). They **cannot be built or run on this Linux cloud VM** — the repo's own `Dockerfile`/`docker-compose.yml` explicitly say Linux Docker/Podman cannot pull or run the Windows base images. Don't waste time trying; there is no MSBuild/Mono path that handles the WCF RIA DomainServices + Telerik OpenAccess + CefSharp stack here.

The **front-end SPA** (`Main/Workarea`, `Main/User Interface`) depends on Linx's **private npm registry** packages `@linx.uxmobile/linx-web-host` and `@linx.uxmobile/linx-bootstrap`. These are **404 on public npm** and there is no `.npmrc` auth configured, so `npm install`/`npm run dev` for the SPA cannot run here without private-registry credentials.

The prebuilt .NET Framework web apps — **Service** (`Main/Binary/Service`, port 1710), **Portal** (`Main/Binary/Portal`, port 8172), and **Application** (`Main/Binary/Application`) — are classic ASP.NET `System.Web` apps launched on Windows via `WebDev.WebServer40.exe` or IIS Express (`WebDevWeb.bat` / `WebIISExpress.bat`). **Do not try to run these on the Linux cloud desktop.** Mono + `xsp4` is NOT a workaround: Ubuntu 24.04's `mono-xsp4` (Mono 6.8) crashes on startup even for an empty site (`TypeLoadException` on `Mono.Security.Protocol.Tls.PrivateKeySelectionCallback`), and even if hosted, the apps depend on **WCF RIA DomainServices** (`System.ServiceModel.DomainServices.*`, unimplemented in Mono), **Telerik Reporting/OpenAccess** (Windows-native), and Integrated-Security SQL. Running Service/Portal/Application requires a **Windows host** (IIS Express) or **Windows containers**. On Linux, the `CoreServiceBus` (below) is the only runnable analog of "Service".

### What actually runs on Linux: the ServiceBus (`CoreServiceBus`)
The only cross-platform, runnable component is the **prebuilt .NET Core 2.0 data/domain API host** at `Main/Binary/CoreServiceBus` (`LinxHostCore.dll`). It is already compiled (binaries are committed), so no restore/build is needed.

Run it:
```
cd Main/Binary/CoreServiceBus
DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 dotnet LinxHostCore.dll
```
- Listens on `http://localhost:1710` (Kestrel). Configured via `appsettings.json` (`server.urls`).
- `StartService.bat` just runs `dotnet LinxHostCore.dll`.
- Smoke test / "hello world": `curl http://localhost:1710/swagger/v1/swagger.json` → HTTP 200 OpenAPI JSON (`title: "LinxHostCore APIs"`). This proves the API host is up.

Non-obvious gotchas:
- **`DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1` is required** — the VM has no ICU package compatible with .NET Core 2.x, so it crashes on startup without it.
- `.NET Core 2.x` needs **`libssl1.1`** (installed during env setup; not in Ubuntu 24.04 by default). If startup errors with `No usable version of libssl was found`, reinstall `libssl1.1`.
- The Swagger `paths` object is **empty by design here**: business modules are loaded at startup from `BusinessModules/*/{BM,BV,API}/**/bin/*.dll`, and those module DLLs are produced by the Windows .NET Framework build, so none are present on Linux. The host still serves the API surface and responds to HTTP.
- Startup does **not** require SQL Server. The connection strings in `appsettings.json` point at internal hosts with Windows Integrated Security and are only exercised when data endpoints are called. Data-backed endpoints will not work here without a reachable SQL Server and non-Integrated-Security auth.

### Runtime / tooling
- `.NET Core runtime 2.1.30` is installed at `/usr/share/dotnet` (symlinked to `/usr/local/bin/dotnet`). The app targets `netcoreapp2.0` and rolls forward to 2.1.x.
- There is no lockfile-based dependency refresh for this repo; the runnable component ships prebuilt binaries. The update script only ensures the .NET Core runtime + `libssl1.1` are present.

### CI/CD — building the Windows artifacts
`.github/workflows/build-artifacts.yml` builds **Application**, **Portal**, and **Service** on the GitHub-hosted `windows-latest` runner (matrix, `fail-fast: false`) and uploads a **deployable `.zip` package per app** (`<App>-<run_number>-<short_sha>.zip`, artifact names `Application-package` / `Portal-package` / `Service-package`). It runs on push to `master`, on pull requests, and via `workflow_dispatch`. This is the only place these Windows-only apps can be built end-to-end in CI (they cannot build on the Linux cloud VM).

Packaging strategy per app:
- **Application / Portal** (ASP.NET MVC web apps): MSBuild Web Publishing Pipeline (`/p:DeployOnBuild=true`) assembles the deployable web app into `obj\Release\Package\PackageTmp` (bin + Views + Content + Web.config, no source); that folder is zipped. The classic `WebPublishMethod=FileSystem` `publishUrl` copy is unreliable on the runner, so we package `PackageTmp` directly.
- **Service** (`Linx.DataService`, a class library): the deployable service web root committed at `Binary/Service` is copied and its `bin` is overlaid with the freshly built assemblies, then zipped.

Non-obvious things the workflow has to do (so the build is green on a stock runner):
- **Install the .NET Framework 4.6.1 targeting pack** (`choco install netfx-4.6.1-devpack`) — not preinstalled, otherwise `MSB3644`.
- **`msbuild ... /p:PostBuildEvent=`** — neutralizes per-project post-build `xcopy` steps (e.g. in `Linx.Framework.BV.csproj`) that copy DLLs into the committed `Binary/` folders on a dev machine; they fail on CI and are irrelevant to artifact generation.
- **Restore the full `.sln`, but compile a scoped target.** For **Application** it builds the web project `Linx.Internet.Application.csproj` (not the full `.sln`) to skip the CefSharp WPF desktop shell `Linx.Internet.Application.WinHost` (targets .NET Framework v4.0, whose pack isn't on the runner, and pulls heavy native CefSharp bits). The web project's `bin` already contains all copy-local dependencies.
- **Stage the WCF RIA Services SDK assembly for Service.** `Data/Linx.DataService/Linx.DataService.csproj` references `System.ServiceModel.DomainServices.Server.dll` via a relative HintPath that resolves to one directory *above* the checkout (`<workspace>\..\Program Files (x86)\Microsoft SDKs\RIA Services\v1.0\Libraries\Server\`). The DLL is vendored at `Main/Binary/Service/bin`, so the workflow copies it there rather than installing the discontinued RIA Services SDK.

If you change target frameworks, project layout (checkout depth changes the RIA HintPath resolution), or add projects with new proprietary references (Telerik, RIA), expect to adjust these steps.

### Git note
`/workspace/.git` is a gitlink file; the git toplevel is `/workspace` and the source tree lives under `/workspace/Main`.
