# AGENTS.md

## Cursor Cloud specific instructions

### Repository reality (read first)
This repo (`linx-ux`, the Linx Framework ERP monorepo) is **overwhelmingly Windows-only .NET Framework 4.6.1/4.8**. The web app (`Main/Application/Linx.Internet.Application`), `Linx.Portal`, and `Linx.ImageService` require **MSBuild on Windows** (or the `mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore` container). They **cannot be built or run on this Linux cloud VM** — the repo's own `Dockerfile`/`docker-compose.yml` explicitly say Linux Docker/Podman cannot pull or run the Windows base images. Don't waste time trying; there is no MSBuild/Mono path that handles the WCF RIA DomainServices + Telerik OpenAccess + CefSharp stack here.

The **front-end SPA** (`Main/Workarea`, `Main/User Interface`) depends on Linx's **private npm registry** packages `@linx.uxmobile/linx-web-host` and `@linx.uxmobile/linx-bootstrap`. These are **404 on public npm** and there is no `.npmrc` auth configured, so `npm install`/`npm run dev` for the SPA cannot run here without private-registry credentials.

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

### Git note
`/workspace/.git` is a gitlink file; the git toplevel is `/workspace` and the source tree lives under `/workspace/Main`.
