# SI-PDR CI/CD (GitHub Actions → AWS IIS)

Deploys **Application**, **Service**, and **Portal** to IIS on the shared Windows EC2 host.

**Full guide:** [docs/si-pdr-aws-iis.md](../../docs/si-pdr-aws-iis.md)

## Scripts

| Script | Role |
|--------|------|
| `scripts/Ensure-BuildTools.ps1` | VS 2022 Build Tools + targeting packs |
| `scripts/Ensure-IisSiPdr.ps1` | IIS/ASP.NET + sites on ports 8080/8081/8082 |
| `scripts/Invoke-SiPdrAwsPipeline.ps1` | End-to-end publish + deploy + smoke |
| `scripts/Clear-BuildWorkspace.ps1` | Prune old `C:\lx\*` CI trees |

## Workflow

`.github/workflows/si-pdr-aws-iis.yml`

Requires GitHub secrets `AWS_ACCESS_KEY_ID` / `AWS_SECRET_ACCESS_KEY` on **this** repository.
