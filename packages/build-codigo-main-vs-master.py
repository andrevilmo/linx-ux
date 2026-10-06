#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""Export Main/ source delta: feature/implementacao-completa-ss-mfa-5-10-2026 vs master."""

from __future__ import annotations

import hashlib
import os
import shutil
import subprocess
import zipfile
from datetime import datetime, timezone
from pathlib import Path

REPO = Path("/workspace")
BASE = "origin/master"
HEAD = "HEAD"
PKG_NAME = "CODIGO-Main-vs-master"
PKG = REPO / "packages" / PKG_NAME
ART = Path("/opt/cursor/artifacts")
ZIP_NAME = f"{PKG_NAME}.zip"


def git(*args: str) -> str:
    return subprocess.check_output(["git", *args], cwd=REPO, text=True)


def skip_reason(path: str) -> str | None:
    p = path.replace("\\", "/").lower()
    parts = p.split("/")
    if "node_modules" in parts or "obj" in parts or ".vs" in parts:
        return "build"
    if "/publish-output/" in p:
        return "publish-output"
    if p.endswith(
        (
            ".pdb",
            ".cache",
            ".suo",
            ".rej",
            ".orig",
            ".up2date",
            ".ide",
            ".ide-shm",
            ".ide-wal",
            ".log",
            ".zip",
            ".application",
            ".manifest",
        )
    ):
        return "build"
    if p.endswith((".dll", ".exe")) and "microsoft.identity" not in p:
        return "binaries"
    if "linx.framework.selfhost" in p:
        return "vendor-selfhost"
    if "/common/mobile/" in p:
        return "vendor-mobile"
    if "bootstrap-wysihtml5" in p or "wysihtml5-0.3.0" in p:
        return "vendor-web"
    return None


def sha256_file(path: Path) -> str:
    h = hashlib.sha256()
    with path.open("rb") as fh:
        for chunk in iter(lambda: fh.read(1024 * 1024), b""):
            h.update(chunk)
    return h.hexdigest()


def classify(path: str) -> str:
    p = path.replace("\\", "/")
    if "/Scripts/" in p and p.endswith(".sql"):
        return "DB"
    if p.startswith("Main/Application/Linx.Portal/"):
        return "Portal"
    if p.startswith("Main/Application/Linx.Internet.Application/"):
        return "Application"
    if p.startswith("Main/User Interface/"):
        return "SPA"
    if p.startswith("Main/Business/"):
        return "Service"
    if p.startswith("Main/BM/"):
        return "BM"
    if p.startswith("Main/Binary/"):
        return "Binary"
    if p.startswith("Main/Common/"):
        return "Common"
    return "Outros"


def main() -> None:
    branch = git("rev-parse", "--abbrev-ref", "HEAD").strip()
    head_sha = git("rev-parse", "HEAD").strip()
    master_sha = git("rev-parse", BASE).strip()
    stamp = datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S UTC")

    rows = []
    for line in git("diff", "--name-status", f"{BASE}...{HEAD}", "--", "Main/").splitlines():
        status, path = line[0], line.split("\t", 1)[1]
        rows.append((status, path.replace("\\", "/")))

    kept, deleted, omitted = [], [], []
    for status, path in rows:
        if status == "D":
            deleted.append(path)
            continue
        reason = skip_reason(path)
        if reason:
            omitted.append((reason, path))
            continue
        kept.append((status, path))

    if PKG.exists():
        shutil.rmtree(PKG)
    PKG.mkdir(parents=True)

    copied = []
    for status, path in kept:
        src = REPO / path
        dest = PKG / path
        dest.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(src, dest)
        copied.append((status, path, dest.stat().st_size, sha256_file(dest)))

    db_dir = PKG / "DB"
    db_dir.mkdir(exist_ok=True)
    sql_copied = []
    for status, path, size, digest in copied:
        if path.endswith(".sql") and "/Scripts/" in path.replace("\\", "/"):
            dest = db_dir / Path(path).name
            shutil.copy2(PKG / path, dest)
            sql_copied.append(Path(path).name)

    file_list_lines = [
        f"{PKG_NAME} — arquivos de código",
        f"UTC {stamp}",
        f"from {BASE} ({master_sha[:12]})",
        f"to   {branch} ({head_sha[:12]})",
        "",
        f"{'ST':<2} {'SIZE':>10}  {'SHA256':<64}  PATH",
    ]
    for status, path, size, digest in copied:
        file_list_lines.append(f"{status:<2} {size:10d}  {digest}  {path}")
    (PKG / "FILE-LIST.txt").write_text("\n".join(file_list_lines) + "\n", encoding="utf-8")

    deleted_txt = "Arquivos presentes em master e ausentes nesta branch (não copiar; remover no destino se existirem):\n\n"
    deleted_txt += "\n".join(deleted) + "\n"
    (PKG / "DELETED.txt").write_text(deleted_txt, encoding="utf-8")

    omitted_lines = [
        "Arquivos do diff Main/ que NÃO entram neste pacote de código.",
        "Motivo: artefato de build, binário compilado (rebuild), vendor ou publish-output.",
        "",
    ]
    by_reason = {}
    for reason, path in omitted:
        by_reason.setdefault(reason, []).append(path)
    for reason in sorted(by_reason):
        omitted_lines.append(f"## {reason} ({len(by_reason[reason])})")
        for path in by_reason[reason]:
            omitted_lines.append(path)
        omitted_lines.append("")
    (PKG / "OMITIDOS.txt").write_text("\n".join(omitted_lines), encoding="utf-8")

    groups = {}
    for status, path, size, digest in copied:
        groups.setdefault(classify(path), []).append((status, path, size))

    def bullets(key: str) -> str:
        items = groups.get(key, [])
        if not items:
            return "_nenhum_"
        return "\n".join(f"- `{status}` `{path}` ({size} bytes)" for status, path, size in items)

    leia = f"""# Pacote de código — Main/ vs master

Atualização de **fonte** (não é overlay IIS de DLLs).  
Use `packages/INSTALL_MFA_SSO` quando o destino for só copiar binários no IIS.

| | |
|--|--|
| Branch origem | `{branch}` |
| Commit origem | `{head_sha}` |
| Base | `master` `{master_sha}` |
| Gerado | {stamp} |
| Arquivos copiados | {len(copied)} |
| Deletados em relação ao master | {len(deleted)} |
| Omitidos (build/vendor/binários) | {len(omitted)} |

Estrutura: os caminhos em `Main\\...` são os mesmos do repositório. Cole a pasta `Main\\` deste pacote **em cima** de um checkout `master`.

## O que este pacote traz

Implementações em `Main/` da branch `feature/implementacao-completa-ss-mfa-5-10-2026` que não estão em `master`:

- MFA TOTP (Service, Portal `/Mfa/Challenge`, ticket no Application)
- SSO Azure / MSAL (identifier-first, vínculo OID/UPN, Revoga SSO)
- Cadastros UX: Utiliza MFA/SSO, Revoga MFA, Revoga SSO
- Lookup de Ambientes (usuário editado + IdLinx) em `Events.cs` + SPA
- Schema SQL `APPLY_SSO_MFA.sql` e scripts por objeto
- Pacotes NuGet MSAL (`Microsoft.Identity.Client` 4.54.1)

## O que não entra

Ver `OMITIDOS.txt`. Resumo: `obj/`, `node_modules`, `publish-output`, `.pdb`, DLLs compiladas do produto (exceto MSAL), imagens vendor SelfHost/Mobile.

Para publicar IIS **sem rebuild**, use `packages/INSTALL_MFA_SSO` e depois aplique o fonte do lookup (`UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP.Events.cs`) via rebuild ou patch Cecil.

## Como aplicar

1. Checkout `master` (ou árvore equivalente).
2. Copie `Main\\` deste pacote sobre `Main\\` do destino (substituir arquivos).
3. Apague os arquivos de `DELETED.txt` se ainda existirem.
4. **Não** sobrescreva `Web.config` de produção: mescle só as seções MFA/SSO (`azureAd`, flags). Ajuste SMTP, connection strings e secrets do ambiente.
5. No SSMS, catálogo **Portal / FrameworkAutorizacao**, rode `DB\\APPLY_SSO_MFA.sql` (idempotente). Não rode no catálogo Application.
6. Restaure NuGet do Portal (MSAL já está em `Main\\Application\\Linx.Portal\\packages\\`).
7. Compile Portal, Service (`Linx.Framework.BV` + `Linx.Framework.BV.WebAPI.DS`) e Application / SPA.
8. Recicle os pools IIS.

## SQL (atalho)

Os scripts também estão em `DB\\` na raiz do pacote:

{chr(10).join(f"- `DB\\\\{name}`" for name in sql_copied) or "- (nenhum)"}

## Arquivos por área

### Portal
{bullets("Portal")}

### Application
{bullets("Application")}

### Service
{bullets("Service")}

### SPA (User Interface)
{bullets("SPA")}

### BM / SQL
{bullets("BM")}

### Binary (views/config publicados)
{bullets("Binary")}

### Common
{bullets("Common")}

### Outros
{bullets("Outros")}

## Arquivos deletados vs master

Ver `DELETED.txt`.

Inventário com SHA256: `FILE-LIST.txt`.
"""
    (PKG / "LEIA-ME.md").write_text(leia, encoding="utf-8")

    versions = f"""PKG={PKG_NAME}
FROM_BRANCH=master
FROM_COMMIT={master_sha}
TO_BRANCH={branch}
TO_COMMIT={head_sha}
UTC={stamp}
COPIED={len(copied)}
DELETED={len(deleted)}
OMITTED={len(omitted)}
"""
    (PKG / "VERSIONS.txt").write_text(versions, encoding="utf-8")

    ART.mkdir(parents=True, exist_ok=True)
    tmp_zip = Path("/tmp") / ZIP_NAME
    if tmp_zip.exists():
        tmp_zip.unlink()
    with zipfile.ZipFile(tmp_zip, "w", zipfile.ZIP_DEFLATED) as zf:
        for root, _dirs, files in os.walk(PKG):
            for name in files:
                full = Path(root) / name
                arc = Path(PKG_NAME) / full.relative_to(PKG)
                zf.write(full, arc.as_posix())
    pkg_zip = REPO / "packages" / ZIP_NAME
    shutil.copy2(tmp_zip, pkg_zip)
    print("zip", pkg_zip, pkg_zip.stat().st_size)
    art_zip = ART / ZIP_NAME
    try:
        shutil.copy2(tmp_zip, art_zip)
        print("zip", art_zip, art_zip.stat().st_size)
    except OSError as exc:
        print("artifacts zip skipped:", exc)

    print("copied", len(copied), "deleted", len(deleted), "omitted", len(omitted))
    print("pkg", PKG)


if __name__ == "__main__":
    main()
