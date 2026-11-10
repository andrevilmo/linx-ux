#!/bin/bash
# Runs decision + live HML smoke against Linx.LicenseServer.Client via the sync adapter.
# Kept the historic script name; execution is net8 (mcs + netstandard facades conflict on this host).
set -euo pipefail

ROOT="$(cd "$(dirname "$0")/../../.." && pwd)"
SMOKE="$ROOT/Main/Business/Linx.License.Server.Access.Tests/Linx.License.Server.Dll.Smoke.csproj"

export PATH="${HOME}/.dotnet:${PATH}"
cd "$ROOT"
dotnet run --project "$SMOKE" -c Release
