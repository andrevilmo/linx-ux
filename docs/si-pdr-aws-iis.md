# SI-PDR on AWS Windows — IIS Application / Service / Portal

Same pattern as OmniPOS AWS CI: GitHub Actions packages sources → S3 → SSM on the shared Windows EC2 host → MSBuild publish → deploy into three IIS sites.

## Sites

| IIS site | Port | Content root |
|----------|------|----------------|
| **Application** | `8174` (also `8080`) | `C:\Linx Program Files\Linx Framework 6.0.0\Application` |
| **Portal** | `8172` (also `8081`) | `...\Portal` |
| **Service** (ServiceBus) | `1710` (also `8082`) | `...\Service` |

Primary ports match Binary `web.config` (`PortalUrl`, `ServiceBus`, `authorizationServiceAddress`). `8080` / `8081` / `8082` remain optional CI aliases.

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
| Persistent workspace | `C:\lx\si-pdr` (robocopy merge; preserves `**/obj`) |
| Host lock file | `C:\lx\.ci-lock` (avoids overlapping OmniPOS CI) |

## GitHub secrets (required on **linx-ux**)

Add repository secrets (same IAM user as OmniPOS CI works):

- `AWS_ACCESS_KEY_ID`
- `AWS_SECRET_ACCESS_KEY`

Without these, the workflow cannot talk to S3/SSM.

### Binary web.config (authoritative)

Deploy syncs `Main/Binary/{Service,Application,Portal}/Web.config` into the IIS Framework root. Current Binary configs target **QA 3-12** (`tcp:10.16.0.4` / `qa-ux-portal-3-12` / `qa-ux-app-3-12`), with:

- Application / Service `ShellMode=PROD`
- Service `LocalServiceBusSettings/mode=PROD` (so Portal `CurrentUser` headers are honored)
- Portal `authorizationServiceAddress` + Application `ServiceBus` → `http://localhost:1710/`
- Portal `PortalUrl` → `http://localhost:8172/`

`Set-SiPdrSqlConnectionStrings.ps1` only rewrites SQL / Service URL / ShellMode when the matching `SI_PDR_*` env/secret is set; otherwise Binary configs are left as-is.

## Workflow

`.github/workflows/si-pdr-aws-iis.yml`

1. Detect `skip_build` when the diff vs previous commit has no compilable `Main/` source (only `Main/Binary`, configs, `infra/`, `.vscode/`, docs) — or force via `workflow_dispatch`
2. Package sources (extra excludes: CoreServiceBus/ImageService/SelfHost/WinHost/publish-output; lighter package when `skip_build`)
3. Upload to S3
4. SSM merges into **persistent workspace** `C:\lx\si-pdr` (preserves `**/obj` for incremental MSBuild)
5. `Invoke-SiPdrAwsPipeline.ps1`:
   - `Ensure-BuildTools.ps1` — VS 2022 Build Tools (+ web)
   - `Ensure-IisSiPdr.ps1` — IIS sites; `-SkipHeavySeed` skips Library robocopy when already present
   - `stack-to-publish.ps1` — MSBuild Tools → BV → WebAPI → Application → Portal (skipped when `skip_build`)
   - `deploy-to-linx-framework.ps1 -SkipBackup -Force`
   - `Set-SiPdrSqlConnectionStrings.ps1` — optional overrides only when `SI_PDR_*` set
   - Short smoke on `:8080|:8081|:8082` then `:8174|:8172|:1710`
6. Cleanup old per-run dirs; **keep** `C:\lx\si-pdr` obj caches

Manual dispatch: `skip_build=true` (Binary-only), `force_full_seed=true` (re-robocopy Library).

## Local / RDP runbook

1. Start EC2; wait for SSM Online
2. RDP as `Administrator` to the **current** public IP
3. Open:

```text
http://localhost:8174/   # Application (primary)
http://localhost:8172/   # Portal (primary)
http://localhost:1710/   # Service (primary)
http://localhost:8080/   # Application alias
http://localhost:8081/   # Portal alias
http://localhost:8082/   # Service alias
```

From your laptop (after opening SG inbound for those ports to your `/32`):

```text
http://<public-ip>:8174/
http://<public-ip>:8172/
http://<public-ip>:1710/
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
