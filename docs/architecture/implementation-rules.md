# FusionCore Implementation Rules

## Purpose
This document defines the implementation constraints for FusionCore and is binding for code generation, manual development, and code review.

## Core Principles
- Follow the approved architecture documents first, implementation second.
- Keep domain semantics stable and independent from UI and infrastructure.
- Use abstraction before implementation.
- Prefer modular and testable design.
- Keep SEMI-aligned concepts consistent across modules.

## Layering Rules
- FusionUI may depend on FusionApp and FusionDomain, but must not directly depend on hardware, protocol, or infrastructure implementations.
- FusionDomain must not depend on WPF, database frameworks, protocol stacks, or device driver implementations.
- FusionScheduler must not directly control IO, motion, serial, TCP, or protocol drivers.
- FusionFA must not implement equipment internal scheduling logic.
- FusionEquipment.Abstractions contains abstractions only, not concrete PM/TM/CM/BM implementations.
- Infrastructure modules must not define business truth that belongs to Domain.

## Collaboration Rules
- Cross-module collaboration should go through interfaces, contracts, and events.
- Avoid static mutable global state.
- Avoid circular project references.
- Prefer dependency injection over direct instantiation.

## Module Boundary Rules
- PM/TM/CM/BM modules execute module-local behavior only.
- Scheduler decides route, sequence, and recovery strategy.
- FA exposes standardized factory-facing behavior and mappings.
- UI is responsible for presentation, navigation, and operator interaction only.

## Naming Rules
- Project names use `FusionXxx`.
- Interfaces start with `I`.
- Contracts and abstractions should be grouped clearly.
- Events go under `Events`.
- Enums go under `Enums`.
- Value objects go under `ValueObjects`.

## Phase-1 Constraints
- Only build project skeletons and minimal placeholders.
- Do not implement complex scheduling algorithms yet.
- Do not implement full SECS/GEM behavior yet.
- Do not introduce database coupling unless explicitly required.

## Review Rules
- Any new project reference must be justified.
- Any boundary-crossing implementation must be rejected.
- Architecture constraints take priority over convenience.
