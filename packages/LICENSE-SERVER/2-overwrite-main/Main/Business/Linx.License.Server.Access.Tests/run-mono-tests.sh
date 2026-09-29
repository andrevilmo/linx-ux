#!/bin/bash
set -euo pipefail

ROOT="$(cd "$(dirname "$0")/../../.." && pwd)"
OUT="${TMPDIR:-/tmp}/license-access-tests"
NEWTONSOFT="$ROOT/Main/Business/Linx.Framework.BV/Linx.Framework.BV/bin/Debug/Newtonsoft.Json.dll"
if [ ! -f "$NEWTONSOFT" ]; then
  NEWTONSOFT="$ROOT/Main/Binary/Library/Common/Microsoft/Web API/Newtonsoft.Json.dll"
fi
RESTSHARP="$ROOT/Main/User Interface/Linx.Framework.BV/packages/RestSharp.106.2.2/lib/net452/RestSharp.dll"
SRC="$ROOT/Main/Business/Linx.Framework.BV/Linx.Framework.BV"

mkdir -p "$OUT"
mcs -sdk:4.7 -out:"$OUT/Linx.License.Server.Access.Tests.exe" \
  -r:"$NEWTONSOFT" \
  -r:"$RESTSHARP" \
  -r:/usr/lib/mono/4.5/System.Configuration.dll \
  -r:System.Xml \
  "$ROOT/Main/Business/Linx.License.Server.Access.Tests/Program.cs" \
  "$ROOT/Main/Business/Linx.License.Server.Access.Tests/LicenseServerLiveTests.cs" \
  "$SRC/LicenseServer/LicenseAccessDecision.cs" \
  "$SRC/LicenseServer/LicenseAccessResult.cs" \
  "$SRC/LicenseServer/LicenseAccessSnapshot.cs" \
  "$SRC/LicenseServer/LicenseException.cs" \
  "$SRC/LicenseServer/LicenseServerApiClient.cs" \
  "$SRC/LicenseServer/LicenseServerModels.cs" \
  "$SRC/LicenseServer/LicenseServerSettings.cs"

cp "$NEWTONSOFT" "$OUT/"
cp "$RESTSHARP" "$OUT/"
cd "$ROOT"
mono "$OUT/Linx.License.Server.Access.Tests.exe"
