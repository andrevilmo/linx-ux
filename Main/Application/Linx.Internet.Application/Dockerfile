# Windows-only: .NET Framework 4.8 SDK on Windows Server Core (matches docker-compose default).
# Build context must be the repository Main folder (parent of Application):
#   docker build -f Application/Linx.Internet.Application/Dockerfile -t linx-internet-application-build .
#
# From this directory (context = Main):
#   docker build -f Dockerfile -t linx-internet-application-build ..\..
#
# Same constraints as docker-compose.yml (Docker Desktop Windows containers, not Linux/Podman VM).

# escape=`
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022

WORKDIR C:\src
COPY . .

WORKDIR C:\src\Application\Linx.Internet.Application

SHELL ["powershell", "-NoProfile", "-ExecutionPolicy", "Bypass", "-Command"]
RUN nuget restore Linx.Internet.Application.sln -NonInteractive; `
    msbuild Linx.Internet.Application.sln /p:Configuration=Release /m /v:minimal /nologo
