# SI-PDR on AWS Windows — IIS Application / Service / Portal

Same pattern as OmniPOS AWS CI: GitHub Actions packages sources → S3 → SSM on the shared Windows EC2 host → MSBuild publish → deploy into three IIS sites.

## Sites

| IIS site | Port | Content root |
|----------|------|----------------|
| **Application** | `8080` | `C:\Linx Program Files\Linx Framework 6.0.0\Application` |
| **Portal** | `8081` (also `8172`) | `...\Portal` |
| **Service** (ServiceBus) | `1710` (also `8082`) | `...\Service` |

Port **8172** is the Binary / IIS Express Portal URL (`Main/Binary/Portal/WebIISExpress.bat`, `PortalUrl` / Application `loginUrl`). On AWS it is an extra IIS binding on the Portal site so `http://localhost:8172/` responds without starting IIS Express.

Publish/deploy logic matches [`.vscode/stack-to-publish.ps1`](../.vscode/stack-to-publish.ps1) and [`.vscode/deploy-to-linx-framework.ps1`](../.vscode/deploy-to-linx-framework.ps1).

## Branch

- Base: `footer-presente-colocando-filtro-codigo-gpecon-na-exportacao`
- CI branch: **`SI-PDR-CICD-AWS`**

## Shared AWS host

Reuses the OmniPOS Windows build machine:

| Resource | Value |
|----------|--------|
| Instance | `i-0a266494b999c1b81` (`t3.small`, 100 GiB, `sa-east-1`) |
| S3 bucket | `omnipos-cicd-253957900820-sa-east-1` (prefix `linx-ux/runs/`) |
| Remote extract | `C:\lx\<GITHUB_RUN_ID>\` |
| Host lock file | `C:\lx\.ci-lock` (avoids overlapping OmniPOS CI) |

## GitHub secrets (required on **linx-ux**)

Add repository secrets (same IAM user as OmniPOS CI works):

- `AWS_ACCESS_KEY_ID`
- `AWS_SECRET_ACCESS_KEY`

Without these, the workflow cannot talk to S3/SSM.

## Workflow

`.github/workflows/si-pdr-aws-iis.yml`

1. Zip repo (excludes heavy Demos/Automation/CEF)
2. Upload to S3
3. SSM runs `Invoke-SiPdrAwsPipeline.ps1`:
   - `Ensure-BuildTools.ps1` — VS 2022 Build Tools (+ web)
   - `Ensure-IisSiPdr.ps1` — IIS + ASP.NET, create 3 sites, seed from `Main\Binary`
   - `stack-to-publish.ps1` — build Tools → BV → WebAPI → Application → Portal, stage package
   - `deploy-to-linx-framework.ps1 -SkipBackup -Force`
   - Smoke GET `http://127.0.0.1:8080|8081|8172|1710|8082/`
4. Cleanup old `C:\lx\*` workdirs

Manual dispatch supports `skip_build=true` to publish/deploy from Binary outputs only.

## Local / RDP runbook

1. Start EC2; wait for SSM Online
2. RDP as `Administrator` to the **current** public IP
3. Open:

```text
http://localhost:8080/   # Application
http://localhost:8081/   # Portal
http://localhost:8172/   # Portal (Binary PortalUrl / loginUrl alias)
http://localhost:1710/   # Service (ServiceBus; also :8082)
http://localhost:8082/   # Service alias
```

If Portal on `:8172` does not respond but `:8081` does, re-run `Ensure-IisSiPdr.ps1` (or the one-shot below) so the extra binding exists.

### One-shot: bind Portal on 8172 (RDP / SSM)

```powershell
Import-Module WebAdministration
New-WebBinding -Name 'Portal' -Protocol http -Port 8172 -IPAddress '*' -ErrorAction SilentlyContinue
if (-not (Get-NetFirewallRule -DisplayName 'SI-PDR-IIS-8172' -ErrorAction SilentlyContinue)) {
  New-NetFirewallRule -DisplayName 'SI-PDR-IIS-8172' -Direction Inbound -Protocol TCP -LocalPort 8172 -Action Allow
}
Start-Website -Name 'Portal' -ErrorAction SilentlyContinue
Invoke-WebRequest http://127.0.0.1:8172/ -UseBasicParsing -TimeoutSec 15
```

From your laptop (after opening SG inbound 8080–8082 and optionally 8172 to your `/32`):

```text
http://<public-ip>:8080/
http://<public-ip>:8081/
http://<public-ip>:8172/
http://<public-ip>:8082/
```

## Manual pipeline on the host

```powershell
cd C:\lx\<run_id>   # or a git clone
pwsh -File infra\si-pdr-cicd\scripts\Invoke-SiPdrAwsPipeline.ps1 -RepoRoot (Get-Location)
```

## Fixes included on this branch

- `stack-to-publish.ps1` / `stack-to-deploy.ps1` resolve `Main\` correctly (same as deploy-to-linx-framework)
- Publish build order includes **Linx.Tools** (matches Build All)

## Security / networking

- Windows firewall rules for 8080–8082 are created by `Ensure-IisSiPdr.ps1`
- AWS security group still needs inbound TCP 8080–8082 from your IP (RDP SG is separate)
- App may need SQL/config under the Framework root for full functionality; CI validates IIS sites respond

## Related

- OmniPOS runbook (other repo): `docs/omnipos-aws-build.md` in `nyxrepoadmin-license-server-dll`
- VS Code tasks: `.vscode/TASKS.README.md`
