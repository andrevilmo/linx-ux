$ErrorActionPreference = 'Stop'

$outRoot = 'C:\Linx Workspace\out'
$workspace = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path

$projects = @(
    @{
        Name   = 'Linx.Framework.BV'
        Source = Join-Path $workspace 'User Interface\Linx.Framework.BV\podman-volume-output'
    },
    @{
        Name   = 'Linx.Framework.BV.WebAPI.DS'
        Source = Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI.DS\podman-volume-output'
    },
    @{
        Name   = 'Linx.Internet.Application'
        Source = Join-Path $workspace 'Application\Linx.Internet.Application\podman-volume-output'
    },
    @{
        Name   = 'Linx.Portal'
        Source = Join-Path $workspace 'Application\Linx.Portal\podman-volume-output'
    }
)

New-Item -ItemType Directory -Force -Path $outRoot | Out-Null

$failed = $false
foreach ($project in $projects) {
    $dest = Join-Path $outRoot $project.Name
    if (-not (Test-Path $project.Source)) {
        Write-Warning "Skipping $($project.Name): source not found at '$($project.Source)'"
        continue
    }

    Write-Host "Copying $($project.Name) -> $dest"
    New-Item -ItemType Directory -Force -Path $dest | Out-Null

    # robocopy: 0-7 = success; >=8 = failure
    & robocopy $project.Source $dest /E /COPY:DAT /R:2 /W:2 /NFL /NDL /NP | Out-Null
    if ($LASTEXITCODE -ge 8) {
        Write-Error "robocopy failed for $($project.Name) (exit $LASTEXITCODE)"
        $failed = $true
    }
}

if ($failed) {
    exit 1
}

Write-Host "All generated files copied to: $outRoot"
exit 0
