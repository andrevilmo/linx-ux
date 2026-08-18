# How to download the changed-files zip

The single file `original_vs_footer_gpecon_changed_files.zip` is **~84 MB**. Cursor’s Files UI often fails on that size with:

`Failed to load file` / `exec-daemon ReadBinaryFile failed: 404`

Use one of the options below.

## Option A — smallest (recommended for reading)

On the agent **Files** tab, download:

- `original_vs_footer_gpecon_README.zip` (~149 KB) — explanation + full file list
- `original_vs_footer_gpecon_core_source_files.zip` (~1.3 MB) — 86 first-party files
- `original_vs_footer_gpecon_changed_files_source.zip` (~1.9 MB) — source/config/views **without** DLL/PDB/publish-output

Also on GitHub (this branch):

- https://github.com/andrevilmo/linx-ux/tree/cursor/compare-original-footer-gpecon-b03b/docs/compare-original-vs-footer-gpecon

## Option B — full 768-file zip, split into 8 MB parts

On the **Files** tab, download **all** of these (11 files):

`original_vs_footer_gpecon_changed_files.zip.part00`  
… through …  
`original_vs_footer_gpecon_changed_files.zip.part10`

Put them in the same folder. **Do not rename** the `part00`–`part10` suffix.

### Windows (PowerShell)

```powershell
cd $HOME\Downloads   # or the folder where the parts were saved
cmd /c copy /b original_vs_footer_gpecon_changed_files.zip.part00+original_vs_footer_gpecon_changed_files.zip.part01+original_vs_footer_gpecon_changed_files.zip.part02+original_vs_footer_gpecon_changed_files.zip.part03+original_vs_footer_gpecon_changed_files.zip.part04+original_vs_footer_gpecon_changed_files.zip.part05+original_vs_footer_gpecon_changed_files.zip.part06+original_vs_footer_gpecon_changed_files.zip.part07+original_vs_footer_gpecon_changed_files.zip.part08+original_vs_footer_gpecon_changed_files.zip.part09+original_vs_footer_gpecon_changed_files.zip.part10 original_vs_footer_gpecon_changed_files.zip
```

Or, shorter:

```powershell
cd $HOME\Downloads
$out = "original_vs_footer_gpecon_changed_files.zip"
if (Test-Path $out) { Remove-Item $out }
Get-ChildItem "original_vs_footer_gpecon_changed_files.zip.part*" | Sort-Object Name | ForEach-Object {
  $in = [System.IO.File]::OpenRead($_.FullName)
  $dest = [System.IO.File]::Open($out, [System.IO.FileMode]::Append)
  $in.CopyTo($dest); $in.Close(); $dest.Close()
}
```

Then right-click the joined `.zip` → Extract All.

### macOS / Linux

```bash
cd ~/Downloads
cat original_vs_footer_gpecon_changed_files.zip.part* > original_vs_footer_gpecon_changed_files.zip
unzip -t original_vs_footer_gpecon_changed_files.zip
```

## Option C — rebuild the full set with git (no Cursor Files)

This checks out every path that differs, from the footer branch:

```bash
git clone https://github.com/andrevilmo/linx-ux.git
cd linx-ux
git fetch origin original footer-presente-colocando-filtro-codigo-gpecon-na-exportacao
mkdir -p changed-files
git diff --name-only origin/original origin/footer-presente-colocando-filtro-codigo-gpecon-na-exportacao > /tmp/changed.txt
while IFS= read -r f; do
  mkdir -p "changed-files/$(dirname "$f")"
  git show "origin/footer-presente-colocando-filtro-codigo-gpecon-na-exportacao:$f" > "changed-files/$f"
done < /tmp/changed.txt
```

On Windows PowerShell (Git for Windows):

```powershell
git clone https://github.com/andrevilmo/linx-ux.git
cd linx-ux
git fetch origin original footer-presente-colocando-filtro-codigo-gpecon-na-exportacao
New-Item -ItemType Directory -Force changed-files | Out-Null
git diff --name-only origin/original origin/footer-presente-colocando-filtro-codigo-gpecon-na-exportacao | ForEach-Object {
  $dest = Join-Path "changed-files" $_
  New-Item -ItemType Directory -Force (Split-Path $dest) | Out-Null
  git show "origin/footer-presente-colocando-filtro-codigo-gpecon-na-exportacao:$_" | Set-Content -Encoding Byte $dest
}
```

(For binary files on Windows, prefer Git Bash and the `while read` loop above.)
