# FusionCore

FusionCore is a modular semiconductor equipment control platform built with WPF and .NET.

## Phase 1 Scope

The repository is currently in Phase 1: solution and project skeleton initialization.

Current focus:
- establish a clean solution layout
- create baseline source and test projects
- enforce project reference boundaries from the approved architecture
- keep the solution restorable and buildable
- avoid complex business logic, protocol implementation, or scheduling algorithms

## Solution Layout

- `src/` production code
- `tests/` automated tests
- `docs/` architecture and design documents
- `samples/` reserved for later phases

## Source Projects

- `FusionKernel`
- `FusionDomain`
- `FusionApp`
- `FusionUI`
- `FusionEquipment.Abstractions`
- `FusionScheduler`
- `FusionFA`
- `FusionLog`
- `FusionConfig`

## Design Constraints

- `FusionDomain` remains independent from WPF, database, network, and driver implementations.
- `FusionUI` depends only on presentation-safe upstream modules.
- `FusionScheduler` does not directly control concrete hardware drivers.
- `FusionFA` exposes factory-facing contracts and mappings, not equipment-internal scheduling.
- `FusionEquipment.Abstractions` contains contracts only.

## Validation

Typical validation commands:

```powershell
dotnet restore
dotnet build
dotnet test
```
