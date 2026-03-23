# FusionCore Project Structure

## Repository Layout
- `src/` production code
- `tests/` automated tests
- `docs/` architecture and design documents
- `samples/` example applications
- `tools/` internal tooling and scripts

## Source Projects
- `FusionKernel` runtime foundation and shared base abstractions
- `FusionDomain` domain objects, enums, value objects, domain events
- `FusionApp` application services and use-case orchestration
- `FusionUI` WPF shell and presentation layer
- `FusionEquipment.Abstractions` equipment module abstractions and contracts
- `FusionScheduler` scheduling and material flow orchestration contracts
- `FusionFA` factory automation contracts and mappings
- `FusionLog` logging abstractions and baseline services
- `FusionConfig` configuration abstractions and baseline services

## Test Projects
Each source project should have a matching test project using xUnit.
Naming rule:
- `FusionKernel.Tests`
- `FusionDomain.Tests`
- `FusionApp.Tests`
- etc.

## Documentation Layout
- `docs/architecture/` architecture baselines and design specs
- `docs/standards/` standard-related notes and mapping references
- `docs/adr/` architecture decision records
- `docs/guides/` contributor and implementation guides

## Sample Applications
Reserved for later phases:
- `FusionCore.Samples.BasicShell`
- `FusionCore.Samples.VirtualEquipment`
- `FusionCore.Samples.SecsGemDemo`

## Future Reserved Modules
The following modules are planned but not required in phase 1:
- `FusionCom`
- `FusionIO`
- `FusionMotion`
- `FusionVision`
- `FusionSim`
- `FusionData`
- `FusionSecurity`

## Structural Rules
- Concrete PM/TM/CM/BM implementations must not be placed in `FusionEquipment.Abstractions`.
- UI pages must remain in `FusionUI`.
- Domain truth must remain in `FusionDomain`.
- Scheduler-related coordination logic must remain in `FusionScheduler`.
- Factory-facing standard mappings must remain in `FusionFA`.
