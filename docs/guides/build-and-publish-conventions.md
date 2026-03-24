
---

## `docs/guides/build-and-publish-conventions.md`

```md
# FusionCore Build and Publish Conventions

## Purpose

This file provides a repository-friendly implementation summary for FusionCore build, publish, runtime initialization, packaging, and environment startup scripts.

Use this file when:
- adding or updating scripts under `build/`
- reviewing script responsibilities
- defining build/publish/package/run workflows
- checking whether a script is crossing architecture boundaries

This file is a condensed engineering guide.
The formal baseline is defined by:
- `FusionCore Build Publish and Runtime Script Specification`

---

## Core Rule

FusionCore scripts are part of the platform governance layer.

Scripts should:
- prepare environments
- build solutions
- publish processes
- package deliverables
- start environments in a controlled way

Scripts should not:
- contain business logic
- become protocol runtimes
- bypass architecture boundaries
- replace proper application semantics

---

## Recommended Script Set

Recommended first-class scripts:

- `init-runtime.ps1`
- `build.ps1`
- `publish.ps1`
- `package.ps1`
- `run-dev.ps1`
- `run-sim.ps1`
- `run-prod.ps1`

Optional future scripts:
- `clean-runtime.ps1`
- `rollback.ps1`
- `diagnostics.ps1`
- `health-check.ps1`

---

## Script Responsibilities

| Script | Responsible For | Not Responsible For |
|---|---|---|
| `init-runtime.ps1` | create/map runtime root, create directories, copy missing templates | building source code |
| `build.ps1` | restore, build, test, baseline validation | modifying live runtime data |
| `publish.ps1` | publish processes into versioned output directories | starting all processes |
| `package.ps1` | create delivery packages | installer implementation details |
| `run-dev.ps1` | start dev profile environment | replacing publish |
| `run-sim.ps1` | start sim profile environment | implementing simulation logic |
| `run-prod.ps1` | start prod profile environment | compiling source |

---

## Script Layering Rules

Scripts must remain decoupled by responsibility.

Examples:
- runtime initialization should not secretly build the solution
- build should not silently alter on-site runtime data
- publish should not automatically become a full process supervisor
- run scripts should not act as packaging scripts

Scripts may be chained, but should remain independently callable.

---

## Recommended Repository Layout

Recommended structure:

```text
build/
  init-runtime.ps1
  build.ps1
  publish.ps1
  package.ps1
  run-dev.ps1
  run-sim.ps1
  run-prod.ps1
  common/
  env/

deploy/
  config/
  database/
    schema/
  packages/
