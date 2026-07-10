'use strict';
const path = require('path');
const fs = require('fs');
const { spawnSync } = require('child_process');

const root = path.join(__dirname, '..');
const ps1 = path.join(__dirname, 'deploy-to-podman-volume.ps1');

if (!fs.existsSync(ps1)) {
  console.error('Missing:', ps1);
  process.exit(1);
}

const r = spawnSync(
  'powershell.exe',
  ['-NoProfile', '-ExecutionPolicy', 'Bypass', '-File', ps1],
  { stdio: 'inherit', cwd: root }
);

process.exit(typeof r.status === 'number' ? r.status : 1);
