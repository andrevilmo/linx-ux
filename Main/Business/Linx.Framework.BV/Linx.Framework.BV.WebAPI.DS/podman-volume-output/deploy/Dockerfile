# Windows-only: .NET Framework 4.8 SDK on Windows Server Core (matches docker-compose default).
# Build context must be the repository Main folder (parent of Business):
#   docker build -f Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Dockerfile -t linx-framework-bv-webapi-ds-build .
#
# From this directory (context = Main):
#   docker build -f Dockerfile -t linx-framework-bv-webapi-ds-build ..\..\..
#
# Same constraints as docker-compose.yml (Docker Desktop Windows containers, not Linux/Podman VM).

# escape=`
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022

WORKDIR C:\src
COPY . .

WORKDIR C:\src\Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI.DS

SHELL ["powershell", "-NoProfile", "-ExecutionPolicy", "Bypass", "-Command"]
RUN msbuild Linx.Framework.BV.WebAPI.DS.csproj /p:Configuration=Release /m /v:minimal /nologo
