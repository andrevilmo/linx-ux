param(
    [string]$TargetRoot = $env:LINX_IIS_ROOT,
    [switch]$SkipBackup,
    [switch]$Force,
    [switch]$SkipBinarySync,
    [switch]$SkipAppLogin,
    # skip_build: keep IIS DLLs from the last MSBuild. Git Binary DLLs often have
    # extract timestamps newer than the built Portal.dll and would replace MFA bits.
    [switch]$KeepExistingIisDlls
)

$ErrorActionPreference = 'Stop'

# ---------------------------------------------------------------------------
# Paths
# ---------------------------------------------------------------------------

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$workspace = if (Test-Path (Join-Path $repoRoot 'Main\Application')) {
    Join-Path $repoRoot 'Main'
}
else {
    $repoRoot
}

if (-not $TargetRoot) {
    $TargetRoot = 'C:\Linx Program Files\Linx Framework 6.0.0'
}

$targetApplication = Join-Path $TargetRoot 'Application'
$targetPortal = Join-Path $TargetRoot 'Portal'
$targetService = Join-Path $TargetRoot 'Service'

$portalProject = Join-Path $workspace 'Application\Linx.Portal\Linx.Portal'
$applicationProject = Join-Path $workspace 'Application\Linx.Internet.Application\Linx.Internet.Application'
$apiRoot = Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI.DS'
$bvRoot = Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV'

$binaryPortalBin = Join-Path $workspace 'Binary\Portal\bin'
$binaryPortalViews = Join-Path $workspace 'Binary\Portal\Views'
$binaryServiceBin = Join-Path $workspace 'Binary\Service\bin'
$binaryApplicationBin = Join-Path $workspace 'Binary\Application\bin'
$binaryApplicationViews = Join-Path $workspace 'Binary\Application\Views'
$applicationPublishBin = Join-Path $workspace 'Application\Linx.Internet.Application\publish-output\bin'
$appLoginSource = Join-Path $applicationProject 'AppLogin'
$applicationAppTarget = Join-Path $targetApplication 'App'

$portalDllSources = @{
    'Linx.Portal.dll'                 = @(
        (Join-Path $portalProject 'bin\Linx.Portal.dll')
        (Join-Path $binaryPortalBin 'Linx.Portal.dll')
    )
    'Linx.Tools.dll'                  = @(
        (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll')
        (Join-Path $binaryPortalBin 'Linx.Tools.dll')
        (Join-Path $binaryServiceBin 'Linx.Tools.dll')
    )
    'Linx.Resources.Localization.dll' = @(
        (Join-Path $binaryServiceBin 'Linx.Resources.Localization.dll')
        (Join-Path $binaryPortalBin 'Linx.Resources.Localization.dll')
    )
}

$serviceDllSources = @{
    'Linx.Tools.dll'                   = @(
        (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll')
        (Join-Path $binaryServiceBin 'Linx.Tools.dll')
    )
    'Linx.Framework.BV.dll'            = @(
        (Join-Path $bvRoot 'bin\Release\Linx.Framework.BV.dll')
        (Join-Path $binaryServiceBin 'Linx.Framework.BV.dll')
    )
    'Linx.Framework.BV.WebAPI.DS.dll'  = @(
        (Join-Path $apiRoot 'bin\Release\Linx.Framework.BV.WebAPI.DS.dll')
        (Join-Path $binaryServiceBin 'Linx.Framework.BV.WebAPI.DS.dll')
    )
    'Linx.Framework.Autorizacao.BM.dll' = @(
        (Join-Path $workspace 'BM\Linx.Framework.Autorizacao.BM\Linx.Framework.Autorizacao.BM\bin\Release\Linx.Framework.Autorizacao.BM.dll')
        (Join-Path $workspace 'Binary\Library\Business Model\Linx.Framework.Autorizacao.BM.dll')
        (Join-Path $binaryServiceBin 'Linx.Framework.Autorizacao.BM.dll')
    )
}

$script:copiedCount = 0
$script:skippedCount = 0
$script:sitesChanged = @{}
$script:appliedFiles = [System.Collections.Generic.List[object]]::new()
$script:releaseStamp = Get-Date -Format 'yyyyMMdd_HHmmss'

# ---------------------------------------------------------------------------
# Helpers
# ---------------------------------------------------------------------------

function Write-Step {
    param([string]$Message)
    Write-Host ''
    Write-Host "=== $Message ===" -ForegroundColor Cyan
}

function Get-GitBranchName {
    param([string]$RepoPath)

    try {
        Push-Location $RepoPath
        $branch = (& git rev-parse --abbrev-ref HEAD 2>$null)
        if ($LASTEXITCODE -eq 0 -and $branch) {
            return $branch.Trim()
        }
    }
    catch {
    }
    finally {
        Pop-Location -ErrorAction SilentlyContinue
    }

    return '(unknown)'
}

function Get-GitCommitInfo {
    param([string]$RepoPath)

    try {
        Push-Location $RepoPath
        $hash = (& git rev-parse --short HEAD 2>$null)
        if ($LASTEXITCODE -eq 0 -and $hash) {
            return $hash.Trim()
        }
    }
    catch {
    }
    finally {
        Pop-Location -ErrorAction SilentlyContinue
    }

    return '(unknown)'
}

function Get-TargetRelativePath {
    param([string]$FullPath)

    $full = $FullPath
    if ($full.StartsWith($TargetRoot, [StringComparison]::OrdinalIgnoreCase)) {
        return $full.Substring($TargetRoot.Length).TrimStart('\', '/')
    }

    return $FullPath
}

function Add-AppliedFile {
    param(
        [string]$Kind,
        [string]$Site,
        [string]$Destination,
        [string]$SourcePath
    )

    $script:appliedFiles.Add([PSCustomObject]@{
            Kind        = $Kind
            Site        = $Site
            RelativePath = (Get-TargetRelativePath -FullPath $Destination)
            Destination = $Destination
            Source      = $SourcePath
        }) | Out-Null
}

function Get-FileKindFromSite {
    param(
        [string]$Site,
        [string]$Destination
    )

    if ($Site -eq 'Application\App' -or $Destination -match '\\Application\\App\\') {
        return 'App'
    }

    if ($Destination -match '\\Views\\' -or $Site -match 'Views') {
        return 'Views'
    }

    return 'Binary'
}

function Resolve-NewestSource {
    param([string[]]$Candidates)

    $found = @()
    foreach ($path in $Candidates) {
        if ($path -and (Test-Path -LiteralPath $path)) {
            $found += Get-Item -LiteralPath $path
        }
    }

    if ($found.Count -eq 0) {
        return $null
    }

    return ($found | Sort-Object LastWriteTime -Descending | Select-Object -First 1)
}

function Test-NeedsCopy {
    param(
        [System.IO.FileInfo]$Source,
        [string]$Destination,
        [switch]$Force
    )

    if ($Force -or -not (Test-Path -LiteralPath $Destination)) {
        return $true
    }

    $destItem = Get-Item -LiteralPath $Destination
    if ($Source.Length -ne $destItem.Length) {
        return $true
    }

    if ($Source.LastWriteTimeUtc -gt $destItem.LastWriteTimeUtc) {
        return $true
    }

    $srcHash = (Get-FileHash -LiteralPath $Source.FullName -Algorithm MD5).Hash
    $dstHash = (Get-FileHash -LiteralPath $Destination -Algorithm MD5).Hash
    return $srcHash -ne $dstHash
}

function Copy-ItemWithRetry {
    param(
        [string]$LiteralPath,
        [string]$Destination,
        [int]$Retries = 5,
        [int]$DelayMs = 1000,
        [switch]$AllowSkipOnLock
    )

    $attempt = 0
    while ($true) {
        $attempt++
        try {
            Copy-Item -LiteralPath $LiteralPath -Destination $Destination -Force -ErrorAction Stop
            return $true
        }
        catch [System.IO.IOException] {
            $locked = $_.Exception.Message -match 'user-mapped section|being used by another process|cannot access the file'
            if (-not $locked -or $attempt -ge $Retries) {
                if ($AllowSkipOnLock -and $locked) {
                    Write-Warning ("Skipped locked file after {0} attempts: {1} -> {2} ({3})" -f $attempt, $LiteralPath, $Destination, $_.Exception.Message)
                    return $false
                }
                throw
            }
            Start-Sleep -Milliseconds $DelayMs
        }
    }
}

function Copy-FileIfNeeded {
    param(
        [System.IO.FileInfo]$Source,
        [string]$Destination,
        [string]$Site,
        [switch]$Force
    )

    $destDir = Split-Path -Parent $Destination
    if (-not (Test-Path -LiteralPath $destDir)) {
        New-Item -ItemType Directory -Force -Path $destDir | Out-Null
    }

    if (-not (Test-NeedsCopy -Source $Source -Destination $Destination -Force:$Force)) {
        $script:skippedCount++
        return $false
    }

    Copy-ItemWithRetry -LiteralPath $Source.FullName -Destination $Destination | Out-Null
    Write-Host "[$Site] $Destination"
    $script:copiedCount++
    $script:sitesChanged[$Site] = $true
    Add-AppliedFile `
        -Kind (Get-FileKindFromSite -Site $Site -Destination $Destination) `
        -Site $Site `
        -Destination $Destination `
        -SourcePath $Source.FullName
    return $true
}

function Write-ReleaseManifest {
    param(
        [string]$OutputDirectory,
        [string]$GitBranch,
        [string]$GitCommit,
        [string]$BackupZipPath
    )

    if (-not (Test-Path -LiteralPath $OutputDirectory)) {
        New-Item -ItemType Directory -Force -Path $OutputDirectory | Out-Null
    }

    $releasePath = Join-Path $OutputDirectory ("RELEASE_{0}.txt" -f $script:releaseStamp)

    $binaries = @($script:appliedFiles | Where-Object { $_.Kind -eq 'Binary' } | Sort-Object RelativePath)
    $views = @($script:appliedFiles | Where-Object { $_.Kind -eq 'Views' } | Sort-Object RelativePath)
    $appFiles = @($script:appliedFiles | Where-Object { $_.Kind -eq 'App' } | Sort-Object RelativePath)

    $lines = New-Object System.Collections.Generic.List[string]
    $lines.Add('Linx Framework 6.0.0 — release deploy manifest') | Out-Null
    $lines.Add('================================================') | Out-Null
    $lines.Add(("GeneratedAt : {0}" -f (Get-Date).ToString('yyyy-MM-dd HH:mm:ss'))) | Out-Null
    $lines.Add(("GitBranch   : {0}" -f $GitBranch)) | Out-Null
    $lines.Add(("GitCommit   : {0}" -f $GitCommit)) | Out-Null
    $lines.Add(("Workspace   : {0}" -f $workspace)) | Out-Null
    $lines.Add(("TargetRoot  : {0}" -f $TargetRoot)) | Out-Null
    if ($BackupZipPath) {
        $lines.Add(("BackupZip   : {0}" -f $BackupZipPath)) | Out-Null
    }
    $lines.Add(("Copied      : {0}" -f $script:copiedCount)) | Out-Null
    $lines.Add(("Skipped     : {0}" -f $script:skippedCount)) | Out-Null
    $lines.Add('') | Out-Null
    $lines.Add('Use this list to update other environments with the same files (relative to the install root).') | Out-Null
    $lines.Add('') | Out-Null

    $lines.Add(('--- Binary ({0}) ---' -f $binaries.Count)) | Out-Null
    if ($binaries.Count -eq 0) {
        $lines.Add('(none)') | Out-Null
    }
    else {
        foreach ($item in $binaries) {
            $lines.Add($item.RelativePath) | Out-Null
        }
    }

    $lines.Add('') | Out-Null
    $lines.Add(('--- Views ({0}) ---' -f $views.Count)) | Out-Null
    if ($views.Count -eq 0) {
        $lines.Add('(none)') | Out-Null
    }
    else {
        foreach ($item in $views) {
            $lines.Add($item.RelativePath) | Out-Null
        }
    }

    $lines.Add('') | Out-Null
    $lines.Add(('--- App ({0}) ---' -f $appFiles.Count)) | Out-Null
    if ($appFiles.Count -eq 0) {
        $lines.Add('(none)') | Out-Null
    }
    else {
        foreach ($item in $appFiles) {
            $lines.Add($item.RelativePath) | Out-Null
        }
    }

    $lines.Add('') | Out-Null
    $lines.Add('--- Detail (destination <- source) ---') | Out-Null
    if ($script:appliedFiles.Count -eq 0) {
        $lines.Add('(no files applied)') | Out-Null
    }
    else {
        foreach ($item in ($script:appliedFiles | Sort-Object Kind, RelativePath)) {
            $lines.Add(('{0,-8} {1}' -f $item.Kind, $item.RelativePath)) | Out-Null
            $lines.Add(('         <- {0}' -f $item.Source)) | Out-Null
        }
    }

    $utf8NoBom = New-Object System.Text.UTF8Encoding $false
    [System.IO.File]::WriteAllLines($releasePath, $lines.ToArray(), $utf8NoBom)

    Write-Host "Release manifest: $releasePath"
    return $releasePath
}

function Copy-DllWithPdb {
    param(
        [System.IO.FileInfo]$Source,
        [string]$TargetBin,
        [string]$BinaryBin,
        [string]$Site,
        [switch]$Force,
        [switch]$SkipBinarySync,
        [switch]$KeepExistingIisDlls
    )

    $dest = Join-Path $TargetBin $Source.Name
    if ($KeepExistingIisDlls -and (Test-Path -LiteralPath $dest)) {
        Write-Host "[$Site] KeepExistingIisDlls: $dest"
        $script:skippedCount++
        return $false
    }

    $copied = Copy-FileIfNeeded -Source $Source -Destination $dest -Site $Site -Force:$Force

    $pdbSourcePath = [System.IO.Path]::ChangeExtension($Source.FullName, '.pdb')
    if (Test-Path -LiteralPath $pdbSourcePath) {
        $pdbSource = Get-Item -LiteralPath $pdbSourcePath
        $pdbDest = Join-Path $TargetBin $pdbSource.Name
        if (Copy-FileIfNeeded -Source $pdbSource -Destination $pdbDest -Site $Site -Force:$Force) {
            $copied = $true
        }
    }

    if (-not $SkipBinarySync -and $BinaryBin) {
        if (-not (Test-Path -LiteralPath $BinaryBin)) {
            New-Item -ItemType Directory -Force -Path $BinaryBin | Out-Null
        }

        $sameBinary = $false
        $binaryDest = Join-Path $BinaryBin $Source.Name
        if (Test-Path -LiteralPath $binaryDest) {
            $sameBinary = ($Source.FullName -eq (Resolve-Path -LiteralPath $binaryDest).Path)
        }

        if (-not $sameBinary) {
            # Binary mirror is secondary to IIS deploy; skip if VS/IIS has the file locked.
            Copy-ItemWithRetry -LiteralPath $Source.FullName -Destination $binaryDest -AllowSkipOnLock | Out-Null
            if (Test-Path -LiteralPath $pdbSourcePath) {
                Copy-ItemWithRetry -LiteralPath $pdbSourcePath -Destination (Join-Path $BinaryBin ([IO.Path]::GetFileName($pdbSourcePath))) -AllowSkipOnLock | Out-Null
            }
        }
    }

    return $copied
}

function Copy-ViewsTree {
    param(
        [string]$SourceViews,
        [string]$TargetViews,
        [string]$BinaryViews,
        [string]$Site,
        [string]$Filter = '*',
        [switch]$Force,
        [switch]$SkipBinarySync
    )

    if (-not (Test-Path -LiteralPath $SourceViews)) {
        Write-Warning "[$Site] Views source not found: $SourceViews"
        return
    }

    if (-not (Test-Path -LiteralPath $TargetViews)) {
        New-Item -ItemType Directory -Force -Path $TargetViews | Out-Null
    }

    Write-Host "[$Site] Views: $SourceViews -> $TargetViews"

    Get-ChildItem -LiteralPath $SourceViews -Recurse -File -Filter $Filter | ForEach-Object {
        $relative = $_.FullName.Substring($SourceViews.Length).TrimStart('\', '/')
        $dest = Join-Path $TargetViews $relative
        Copy-FileIfNeeded -Source $_ -Destination $dest -Site $Site -Force:$Force | Out-Null

        if (-not $SkipBinarySync -and $BinaryViews) {
            $binaryDest = Join-Path $BinaryViews $relative
            $binaryDir = Split-Path -Parent $binaryDest
            if (-not (Test-Path -LiteralPath $binaryDir)) {
                New-Item -ItemType Directory -Force -Path $binaryDir | Out-Null
            }
            Copy-Item -LiteralPath $_.FullName -Destination $binaryDest -Force
        }
    }
}

function Get-ApplicationDllSources {
    $byName = @{}

    $roots = @(
        $binaryApplicationBin
        $applicationPublishBin
        (Join-Path $applicationProject 'bin')
        (Join-Path $workspace 'Binary\Library\User Interface')
    )

    foreach ($root in $roots) {
        if (-not (Test-Path -LiteralPath $root)) {
            continue
        }

        Get-ChildItem -LiteralPath $root -Filter 'Linx*.dll' -File -ErrorAction SilentlyContinue | ForEach-Object {
            if (-not $byName.ContainsKey($_.Name) -or $_.LastWriteTime -gt $byName[$_.Name].LastWriteTime) {
                $byName[$_.Name] = $_
            }
        }
    }

    $uiRoot = Join-Path $workspace 'User Interface'
    if (Test-Path -LiteralPath $uiRoot) {
        Get-ChildItem -LiteralPath $uiRoot -Recurse -Filter 'Linx*.dll' -File -ErrorAction SilentlyContinue |
            Where-Object { $_.FullName -match '\\bin\\(Release|Debug)\\' } |
            ForEach-Object {
                if (-not $byName.ContainsKey($_.Name) -or $_.LastWriteTime -gt $byName[$_.Name].LastWriteTime) {
                    $byName[$_.Name] = $_
                }
            }
    }

    return $byName
}

function Restart-SiteAppDomain {
    param([string]$SiteRoot, [string]$Site)

    $webConfig = Join-Path $SiteRoot 'Web.config'
    if (-not (Test-Path -LiteralPath $webConfig)) {
        Write-Warning "[$Site] Web.config not found; skipped app reload."
        return
    }

    (Get-Item -LiteralPath $webConfig).LastWriteTime = Get-Date
    Write-Host "[$Site] Touched Web.config to reload app domain."
}

function New-FrameworkBackupZip {
    param([string]$SourceRoot)

    if (-not (Test-Path -LiteralPath $SourceRoot)) {
        Write-Error "Install folder not found: $SourceRoot"
    }

    $parent = Split-Path -Parent $SourceRoot
    $zipPath = Join-Path $parent ("Linx Framework 6.0.0_BKP_{0}.zip" -f $script:releaseStamp)

    Write-Host "Creating backup zip..."
    Write-Host "  From: $SourceRoot"
    Write-Host "  To:   $zipPath"

    if (Test-Path -LiteralPath $zipPath) {
        Remove-Item -LiteralPath $zipPath -Force
    }

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::CreateFromDirectory(
        $SourceRoot,
        $zipPath,
        [System.IO.Compression.CompressionLevel]::Optimal,
        $false
    )

    $sizeMb = [math]::Round((Get-Item -LiteralPath $zipPath).Length / 1MB, 1)
    Write-Host "Backup created ($sizeMb MB): $zipPath"
    return $zipPath
}

# ---------------------------------------------------------------------------
# Validate targets
# ---------------------------------------------------------------------------

foreach ($required in @($targetApplication, $targetPortal, $targetService)) {
    if (-not (Test-Path -LiteralPath $required)) {
        Write-Error "Required deploy target not found: $required"
    }
}

Write-Host "Workspace:  $workspace"
Write-Host "Target IIS: $TargetRoot"

# ---------------------------------------------------------------------------
# 1) Backup
# ---------------------------------------------------------------------------

Write-Step 'Backup'
$backupZipPath = $null
if ($SkipBackup) {
    Write-Host 'Skipping backup (-SkipBackup).'
}
else {
    $backupZipPath = New-FrameworkBackupZip -SourceRoot $TargetRoot
}

# ---------------------------------------------------------------------------
# 2) Update bin folders
# ---------------------------------------------------------------------------

Write-Step 'Update bin folders'

# Portal
$portalBin = Join-Path $targetPortal 'bin'
Write-Host "[Portal] bin -> $portalBin"
foreach ($dllName in ($portalDllSources.Keys | Sort-Object)) {
    $source = Resolve-NewestSource -Candidates $portalDllSources[$dllName]
    if (-not $source) {
        Write-Warning "[Portal] Source not found for $dllName"
        continue
    }
    Copy-DllWithPdb -Source $source -TargetBin $portalBin -BinaryBin $binaryPortalBin -Site 'Portal' -Force:$Force -SkipBinarySync:$SkipBinarySync -KeepExistingIisDlls:$KeepExistingIisDlls | Out-Null
}

# Service
$serviceBin = Join-Path $targetService 'bin'
Write-Host "[Service] bin -> $serviceBin"
foreach ($dllName in ($serviceDllSources.Keys | Sort-Object)) {
    $source = Resolve-NewestSource -Candidates $serviceDllSources[$dllName]
    if (-not $source) {
        Write-Warning "[Service] Source not found for $dllName"
        continue
    }
    Copy-DllWithPdb -Source $source -TargetBin $serviceBin -BinaryBin $binaryServiceBin -Site 'Service' -Force:$Force -SkipBinarySync:$SkipBinarySync -KeepExistingIisDlls:$KeepExistingIisDlls | Out-Null
}

# Service Extension (AuthenticateUserExtension is loaded from bin\Extension\)
$serviceExtensionBin = Join-Path $serviceBin 'Extension'
$extensionDllName = 'Linx.Framework.BV.AuthenticateUserExtension.dll'
$extensionSource = Resolve-NewestSource -Candidates @(
    (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.AuthenticateUserExtension\bin\Release\Linx.Framework.BV.AuthenticateUserExtension.dll')
    (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.AuthenticateUserExtension\bin\Debug\Linx.Framework.BV.AuthenticateUserExtension.dll')
    (Join-Path $serviceBin $extensionDllName)
    (Join-Path $binaryServiceBin $extensionDllName)
)
if ($extensionSource) {
    Write-Host "[Service] Extension -> $serviceExtensionBin"
    Copy-DllWithPdb -Source $extensionSource -TargetBin $serviceExtensionBin -BinaryBin $null -Site 'Service' -Force:$Force -SkipBinarySync -KeepExistingIisDlls:$KeepExistingIisDlls | Out-Null
}
else {
    Write-Warning "[Service] Source not found for $extensionDllName (Extension folder)"
}

# Application — only Linx*.dll that already exist in the running Application\bin
$applicationBin = Join-Path $targetApplication 'bin'
Write-Host "[Application] bin -> $applicationBin"
$applicationSources = Get-ApplicationDllSources
$targetDllNames = @()
if (Test-Path -LiteralPath $applicationBin) {
    $targetDllNames = Get-ChildItem -LiteralPath $applicationBin -Filter 'Linx*.dll' -File |
        Select-Object -ExpandProperty Name
}

foreach ($dllName in ($targetDllNames | Sort-Object)) {
    if (-not $applicationSources.ContainsKey($dllName)) {
        continue
    }

    Copy-DllWithPdb `
        -Source $applicationSources[$dllName] `
        -TargetBin $applicationBin `
        -BinaryBin $binaryApplicationBin `
        -Site 'Application' `
        -Force:$Force `
        -SkipBinarySync:$SkipBinarySync `
        -KeepExistingIisDlls:$KeepExistingIisDlls | Out-Null
}

# ---------------------------------------------------------------------------
# 3) Update Views folders
# ---------------------------------------------------------------------------

Write-Step 'Update Views folders'

Copy-ViewsTree `
    -SourceViews (Join-Path $portalProject 'Views') `
    -TargetViews (Join-Path $targetPortal 'Views') `
    -BinaryViews $binaryPortalViews `
    -Site 'Portal' `
    -Filter '*.cshtml' `
    -Force:$Force `
    -SkipBinarySync:$SkipBinarySync

Copy-ViewsTree `
    -SourceViews (Join-Path $applicationProject 'Views') `
    -TargetViews (Join-Path $targetApplication 'Views') `
    -BinaryViews $binaryApplicationViews `
    -Site 'Application' `
    -Filter '*' `
    -Force:$Force `
    -SkipBinarySync:$SkipBinarySync

# ---------------------------------------------------------------------------
# 4) Update Application\App from AppLogin when needed
# ---------------------------------------------------------------------------

Write-Step 'Update Application\App from AppLogin'

if ($SkipAppLogin) {
    Write-Host 'Skipping AppLogin overlay (-SkipAppLogin).'
}
elseif (-not (Test-Path -LiteralPath $appLoginSource)) {
    Write-Warning "AppLogin source not found: $appLoginSource"
}
else {
    if (-not (Test-Path -LiteralPath $applicationAppTarget)) {
        New-Item -ItemType Directory -Force -Path $applicationAppTarget | Out-Null
    }

    Write-Host "From: $appLoginSource"
    Write-Host "To:   $applicationAppTarget"

    Get-ChildItem -LiteralPath $appLoginSource -Recurse -File | ForEach-Object {
        $relative = $_.FullName.Substring($appLoginSource.Length).TrimStart('\', '/')
        $dest = Join-Path $applicationAppTarget $relative
        Copy-FileIfNeeded -Source $_ -Destination $dest -Site 'Application\App' -Force:$Force | Out-Null
    }
}

# ---------------------------------------------------------------------------
# Sync runtime web.config (assemblyBinding redirects, e.g. System.Web.Mvc)
# ---------------------------------------------------------------------------

Write-Step 'Sync site web.config from Binary'

foreach ($pair in @(
        @{ Site = 'Application'; Src = (Join-Path $workspace 'Binary\Application\web.config'); Dst = (Join-Path $targetApplication 'web.config') }
        @{ Site = 'Portal'; Src = (Join-Path $workspace 'Binary\Portal\web.config'); Dst = (Join-Path $targetPortal 'web.config') }
        @{ Site = 'Service'; Src = (Join-Path $workspace 'Binary\Service\web.config'); Dst = (Join-Path $targetService 'web.config') }
    )) {
    if (Test-Path -LiteralPath $pair.Src) {
        Copy-Item -LiteralPath $pair.Src -Destination $pair.Dst -Force
        Write-Host "[$($pair.Site)] Updated web.config from Binary"
        $script:sitesChanged[$pair.Site] = $true
    }
    else {
        Write-Host "[$($pair.Site)] Binary web.config not found; left existing IIS config"
    }
}

# ---------------------------------------------------------------------------
# Recycle changed sites
# ---------------------------------------------------------------------------

Write-Step 'Recycle app domains'

foreach ($pair in @(
        @{ Site = 'Portal'; Root = $targetPortal }
        @{ Site = 'Service'; Root = $targetService }
        @{ Site = 'Application'; Root = $targetApplication }
    )) {
    if ($script:sitesChanged.ContainsKey($pair.Site) -or $script:sitesChanged.ContainsKey(($pair.Site + '\App'))) {
        Restart-SiteAppDomain -SiteRoot $pair.Root -Site $pair.Site
    }
    else {
        Write-Host "[$($pair.Site)] No changes; skipped reload."
    }
}

# ---------------------------------------------------------------------------
# Release manifest (files applied — use to update other environments)
# ---------------------------------------------------------------------------

Write-Step 'Release manifest'

$gitRepo = if (Test-Path (Join-Path $repoRoot '.git')) { $repoRoot } else { $workspace }
$gitBranch = Get-GitBranchName -RepoPath $gitRepo
$gitCommit = Get-GitCommitInfo -RepoPath $gitRepo
$releaseDir = Split-Path -Parent $TargetRoot

$releasePath = Write-ReleaseManifest `
    -OutputDirectory $releaseDir `
    -GitBranch $gitBranch `
    -GitCommit $gitCommit `
    -BackupZipPath $backupZipPath

Write-Host ''
Write-Host "Deploy finished. Copied=$($script:copiedCount) Skipped(up-to-date)=$($script:skippedCount)"
Write-Host "Git branch: $gitBranch ($gitCommit)"
Write-Host "Release file: $releasePath"
exit 0
