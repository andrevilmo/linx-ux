# Agent instructions

## SI-PDR / Linx UX (Windows IIS)

This repository’s runtime stack is three IIS applications under Linx Framework 6.0.0:

| Site | Port (AWS CI) |
|------|----------------|
| Application | 8080 |
| Portal | 8081 |
| Service | 8082 |

Source lives under `Main\`. Publish/deploy scripts are in `.vscode/` (`stack-to-publish.ps1`, `deploy-to-linx-framework.ps1`).

### AWS CI/CD

Branch **`SI-PDR-CICD-AWS`** adds GitHub Actions → EC2 SSM → IIS deploy.

- Guide: [docs/si-pdr-aws-iis.md](docs/si-pdr-aws-iis.md)
- Workflow: `.github/workflows/si-pdr-aws-iis.yml`
- Scripts: `infra/si-pdr-cicd/scripts/`

Managed Linux Cursor agents cannot MSBuild this .NET Framework 4.6.1 stack; use the AWS Windows host.

### Required secrets

`AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY` on the GitHub repo (same pattern as OmniPOS CI).
