# FusionCore Runtime Path Conventions

## Purpose

This file provides a repository-friendly implementation summary for FusionCore runtime directory and file layout rules.

Use this file when:
- writing code that resolves runtime paths
- designing configuration/data/log/runtime file placement
- implementing scripts that initialize or validate the runtime root
- reviewing whether a new file type belongs in the correct runtime directory

This file is a condensed engineering guide.
The formal baseline is defined by:
- `FusionCore Runtime Directory and File Layout Design`

---

## Core Rule

FusionCore uses the concept of a **logical runtime root**.

Default convention:
- `R:\`

Important:
- `R:\` is the default logical runtime root
- it is **not** the only valid physical path
- code must not hardcode `R:\` as the only truth
- scripts are responsible for creating or mapping the runtime root

---

## Separation Rule

Repository directory and runtime directory must remain separated.

Do:
- keep source code, docs, templates, and schemas in the repository
- keep real runtime config, logs, databases, temp files, runtime files outside the repository

Do not:
- write production logs into the repository
- store real runtime databases inside the repo
- use repo folders as long-term runtime roots

---

## Runtime Root Resolution Order

Recommended resolution priority:

1. explicit startup parameter
2. environment variable `FUSIONCORE_RUNTIME_ROOT`
3. default logical root `R:\`
4. fail clearly if none is valid

Do not silently fall back to the repository root.

---

## Standard Runtime Layout

Recommended logical layout:

```text
R:\
  config\
  data\
    db\
    recipes\
    snapshots\
    imports\
    exports\
  logs\
    FusionUI\
    FusionScheduler\
    FusionFA\
    FusionDeviceHost\
    FusionVisionHost\
  runtime\
    pid\
    locks\
    pipes\
    sockets\
    health\
  temp\
  backups\
  deploy\
    current\
    versions\
    packages\
  scripts\

Additional guidance:
- `data\` is a logical data boundary, not a catch-all bucket
- configuration files still belong under `config\`
- log outputs still belong under `logs\`
- process-private transient state still belongs under `runtime\` or `temp\`
- for `FusionData` P1 classification details, see `data-classification-matrix.md` and `data-storage-conventions.md`
