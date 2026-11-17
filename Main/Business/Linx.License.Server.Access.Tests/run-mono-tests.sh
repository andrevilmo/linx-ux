#!/bin/bash
# Decision suite + Web.config/live smoke for License Server (DLL 1.0.1).
set -euo pipefail

ROOT="$(cd "$(dirname "$0")/../../.." && pwd)"
SMOKE="$ROOT/Main/Business/Linx.License.Server.Access.Tests/Linx.License.Server.Dll.Smoke.csproj"

export PATH="${HOME}/.dotnet:${PATH}"
cd "$ROOT"
dotnet run --project "$SMOKE" -c Release
