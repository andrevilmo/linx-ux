# Downloadable packages

Generated from `origin/original` (`c5023c7c`) vs `origin/SI-PDR-CICD-AWS` (`64739300`).

| File | What to open |
|------|----------------|
| `original_vs_si-pdr-cicd-aws_README.zip` | Docs only. Start with `README.md`. |
| `original_vs_si-pdr-cicd-aws_core_source_files.zip` | Docs + 121 first-party files under `changed-files/`. |
| `original_vs_si-pdr-cicd-aws_changed_files.zip` | Docs + **all 813** changed files under `changed-files/`. |

Re-create the full zip from a clone:

```bash
git fetch origin original SI-PDR-CICD-AWS
git diff --name-only origin/original origin/SI-PDR-CICD-AWS > /tmp/changed.txt
git archive origin/SI-PDR-CICD-AWS -o /tmp/sipdr.tar
mkdir -p /tmp/extract && tar -xf /tmp/sipdr.tar -C /tmp/extract
rsync --files-from=/tmp/changed.txt /tmp/extract/ /tmp/changed-files/
```
