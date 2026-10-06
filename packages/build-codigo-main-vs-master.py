#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""Pacote com TODOS os arquivos alterados: master → SI-PDR-CICD-AWS ∪ feature MFA.

A feature contém a SI-PDR (ancestral). O conjunto é o diff
origin/master...feature/implementacao-completa-ss-mfa-5-10-2026, sem filtros
de dll/obj/publish-output. Não inclui este próprio pacote.
"""

from __future__ import annotations

import hashlib
import os
import shutil
import subprocess
import zipfile
from collections import defaultdict
from datetime import datetime, timezone
from pathlib import Path

REPO = Path("/workspace")
BASE = "origin/master"
# Tip of feature implementations (before this pack folder existed).
SOURCE_REF = "8be30b711cab1962a8889100842bce7fad4e2b70"
PKG_NAME = "CODIGO-Main-vs-master"
PKG = REPO / "packages" / PKG_NAME
ART = Path("/opt/cursor/artifacts")
ZIP_NAME = f"{PKG_NAME}.zip"

SELF_PREFIXES = (
    "packages/CODIGO-Main-vs-master/",
    "packages/CODIGO-Main-vs-master.zip",
    "packages/build-codigo-main-vs-master.py",
)


def git(*args: str) -> str:
    return subprocess.check_output(["git", *args], cwd=REPO, text=True)


def sha256_file(path: Path) -> str:
    h = hashlib.sha256()
    with path.open("rb") as fh:
        for chunk in iter(lambda: fh.read(1024 * 1024), b""):
            h.update(chunk)
    return h.hexdigest()


def is_self(path: str) -> bool:
    p = path.replace("\\", "/")
    return any(p == s.rstrip("/") or p.startswith(s) for s in SELF_PREFIXES)


def parse_name_status(ref: str) -> tuple[list[tuple[str, str]], list[str]]:
    """Return (copied status+path, deleted old paths)."""
    copied: list[tuple[str, str]] = []
    deleted: list[str] = []
    raw = git("diff", "--name-status", f"{BASE}...{ref}")
    for line in raw.splitlines():
        parts = line.split("\t")
        status = parts[0]
        if status.startswith("R") or status.startswith("C"):
            old, new = parts[1].replace("\\", "/"), parts[2].replace("\\", "/")
            deleted.append(old)
            if not is_self(new):
                copied.append(("R" if status.startswith("R") else "C", new))
        elif status == "D":
            path = parts[1].replace("\\", "/")
            if not is_self(path):
                deleted.append(path)
        else:
            path = parts[1].replace("\\", "/")
            if not is_self(path):
                copied.append((status[0], path))
    return copied, deleted


def classify(path: str) -> str:
    p = path.replace("\\", "/")
    if p.startswith("Main/Application/Linx.Portal/"):
        return "Portal"
    if p.startswith("Main/Application/"):
        return "Application"
    if p.startswith("Main/Business/"):
        return "Service"
    if p.startswith("Main/User Interface/"):
        return "SPA"
    if p.startswith("Main/BM/"):
        return "BM"
    if p.startswith("Main/Binary/"):
        return "Binary"
    if p.startswith("Main/"):
        return "Main-outros"
    if p.startswith("packages/"):
        return "packages"
    if p.startswith("docs/"):
        return "docs"
    if p.startswith("infra/"):
        return "infra"
    return "repo-raiz"


def extract_from_worktree(src_root: Path, repo_path: str, dest: Path) -> None:
    dest.parent.mkdir(parents=True, exist_ok=True)
    shutil.copy2(src_root / repo_path, dest)


def prepare_worktree(ref: str) -> Path:
    wt = Path("/tmp/codigo-src-" + ref[:12])
    subprocess.run(
        ["git", "worktree", "remove", "--force", str(wt)],
        cwd=REPO,
        stdout=subprocess.DEVNULL,
        stderr=subprocess.DEVNULL,
        check=False,
    )
    if wt.exists():
        shutil.rmtree(wt)
    git("worktree", "add", "--detach", str(wt), ref)
    return wt


def main() -> None:
    stamp = datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S UTC")
    master_sha = git("rev-parse", BASE).strip()
    sipdr_sha = git("rev-parse", "origin/SI-PDR-CICD-AWS").strip()
    source_sha = git("rev-parse", SOURCE_REF).strip()

    copied_spec, deleted = parse_name_status(SOURCE_REF)

    if PKG.exists():
        shutil.rmtree(PKG)
    PKG.mkdir(parents=True)

    wt = prepare_worktree(SOURCE_REF)
    copied_meta: list[tuple[str, str, int, str]] = []
    missing: list[str] = []
    try:
        for status, path in copied_spec:
            dest = PKG / path
            src = wt / path
            if not src.is_file():
                missing.append(path)
                continue
            extract_from_worktree(wt, path, dest)
            copied_meta.append((status, path, dest.stat().st_size, sha256_file(dest)))
    finally:
        subprocess.run(
            ["git", "worktree", "remove", "--force", str(wt)],
            cwd=REPO,
            check=False,
        )

    db_dir = PKG / "DB"
    db_dir.mkdir(exist_ok=True)
    sql_names = []
    for status, path, size, digest in copied_meta:
        if path.endswith(".sql"):
            name = Path(path).name
            shutil.copy2(PKG / path, db_dir / name)
            sql_names.append(name)

    lines = [
        f"{PKG_NAME} — delta completo vs master",
        f"UTC {stamp}",
        f"master            {master_sha}",
        f"SI-PDR-CICD-AWS   {sipdr_sha}  (ancestral da feature; 0 commits fora)",
        f"feature MFA       {source_sha}",
        "",
        f"{'ST':<2} {'SIZE':>12}  {'SHA256':<64}  PATH",
    ]
    for status, path, size, digest in copied_meta:
        lines.append(f"{status:<2} {size:12d}  {digest}  {path}")
    (PKG / "FILE-LIST.txt").write_text("\n".join(lines) + "\n", encoding="utf-8")

    (PKG / "DELETED.txt").write_text(
        "Caminhos em master que saíram (rename ou delete). Remova no destino se existirem:\n\n"
        + "\n".join(deleted)
        + "\n",
        encoding="utf-8",
    )
    (PKG / "OMITIDOS.txt").write_text(
        "Nenhum arquivo do diff master...feature foi omitido por tipo (dll/obj/publish-output).\n"
        "Só fica de fora este próprio pacote (packages/CODIGO-Main-vs-master*).\n",
        encoding="utf-8",
    )

    groups: dict[str, list[tuple[str, str, int]]] = defaultdict(list)
    for status, path, size, digest in copied_meta:
        groups[classify(path)].append((status, path, size))

    def bullets(key: str, limit: int = 40) -> str:
        items = groups.get(key, [])
        if not items:
            return "_nenhum_"
        shown = items[:limit]
        text = "\n".join(f"- `{st}` `{p}` ({sz} bytes)" for st, p, sz in shown)
        if len(items) > limit:
            text += f"\n- … +{len(items) - limit} arquivos (ver FILE-LIST.txt)"
        return text

    leia = f"""# Pacote de atualização — delta completo vs master

Todos os arquivos **alterados ou novos** das branches:

- `SI-PDR-CICD-AWS` (`{sipdr_sha[:12]}`)
- `feature/implementacao-completa-ss-mfa-5-10-2026` (`{source_sha[:12]}`)

em relação a `master` (`{master_sha[:12]}`).

A feature **já contém** a SI-PDR (ancestral). O pacote é o union = diff `master...feature`.

| | |
|--|--|
| Gerado | {stamp} |
| Arquivos copiados | {len(copied_meta)} |
| Removidos / renomeados (origem) | {len(deleted)} |
| Falhas ao extrair | {len(missing)} |

**Não é** um checkout inteiro das branches. Só o que mudou vs `master`.  
**Não filtra** DLL, `obj`, `publish-output`, PDB, `node_modules` rastreados, docs, infra, `packages/INSTALL_MFA_SSO`.

## Como aplicar sobre um checkout master

1. Extraia o zip (ou use esta pasta).
2. Copie cada caminho relativo (ex. `Main\\...`, `docs\\...`, `packages\\INSTALL_MFA_SSO\\...`) para a mesma pasta no destino.
3. Apague os caminhos de `DELETED.txt`.
4. **Web.config:** mescle MFA/SSO; não sobrescreva connection string / SMTP / secrets de produção.
5. SQL: `DB\\APPLY_SSO_MFA.sql` no catálogo **Portal** (não no da Application).
6. Recicle os pools IIS se for publicar binários; ou recompile a partir do fonte.

## SQL (atalho na raiz do pacote)

{chr(10).join(f"- `DB\\\\{n}`" for n in sorted(set(sql_names))) or "- (nenhum)"}

## Por área (amostra)

### Portal
{bullets("Portal")}

### Application
{bullets("Application")}

### Service
{bullets("Service")}

### SPA
{bullets("SPA")}

### BM
{bullets("BM")}

### Binary
{bullets("Binary")}

### packages (INSTALL_MFA_SSO etc.)
{bullets("packages")}

### docs / infra / raiz
{bullets("docs")}
{bullets("infra")}
{bullets("repo-raiz")}

Inventário SHA256: `FILE-LIST.txt`.
"""
    (PKG / "LEIA-ME.md").write_text(leia, encoding="utf-8")
    (PKG / "VERSIONS.txt").write_text(
        "\n".join(
            [
                f"PKG={PKG_NAME}",
                "FROM_BRANCH=master",
                f"FROM_COMMIT={master_sha}",
                f"SI_PDR_BRANCH=SI-PDR-CICD-AWS",
                f"SI_PDR_COMMIT={sipdr_sha}",
                "TO_BRANCH=feature/implementacao-completa-ss-mfa-5-10-2026",
                f"TO_COMMIT={source_sha}",
                f"UTC={stamp}",
                f"COPIED={len(copied_meta)}",
                f"DELETED={len(deleted)}",
                f"MISSING={len(missing)}",
                "OMITTED=0",
                "",
            ]
        ),
        encoding="utf-8",
    )

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
    zip_size = pkg_zip.stat().st_size
    print("copied", len(copied_meta), "deleted", len(deleted), "missing", missing)
    print("zip", pkg_zip, zip_size)
    if zip_size >= 100 * 1024 * 1024:
        print("WARNING: zip >= 100MB; do not git add the zip (GitHub limit). Use exploded folder.")
    try:
        ART.mkdir(parents=True, exist_ok=True)
        shutil.copy2(tmp_zip, ART / ZIP_NAME)
        print("artifacts zip ok")
    except OSError as exc:
        print("artifacts zip skipped:", exc)


if __name__ == "__main__":
    main()
