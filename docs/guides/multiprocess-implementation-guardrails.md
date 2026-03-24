# FusionCore Multi-Process Implementation Guardrails

## Purpose

This file defines implementation guardrails for keeping FusionCore aligned with its multi-process architecture direction.

Use this file when:
- adding new service contracts
- designing new models or events
- deciding whether something should be in-process only or process-boundary-friendly
- reviewing code that may accidentally introduce single-process assumptions
- guiding Codex on architecture-safe implementation choices

This file is a repository-friendly engineering guide.
The formal architecture baseline is defined by:
- `FusionCore Process Topology and IPC_Design`
- `implementation-rules.md`

---

## Core Principle

FusionCore is **not** being designed as a single large in-process desktop application.

FusionCore is evolving toward a **multi-process platform** with explicit:
- process ownership
- semantic ownership
- coordination boundaries
- runtime isolation

Implementation must therefore avoid hidden assumptions that all modules:
- run in the same process
- share the same memory
- call each other synchronously
- mutate the same runtime objects

---

## Rule 1: Do Not Treat In-Process Method Calls as the Architecture

A service interface does **not** mean the implementation must always be a direct in-process method call.

Examples:
- `ISchedulerService`
- `IEquipmentStateProvider`
- `IRemoteCommandGateway`
- `IMaterialTracker`

These interfaces express:
- responsibility
- capability boundary
- ownership boundary

They may later be implemented as:
- local service
- IPC client proxy
- RPC facade
- process-local adapter
- command endpoint

### Do
- design interfaces that remain usable across process boundaries
- keep method signatures simple and explicit
- assume latency and failure may exist later

### Do Not
- assume all collaborators are always local objects
- design interfaces that require shared object identity
- use interfaces as a justification for tight in-memory coupling

---

## Rule 2: Semantic Truth Must Have an Owner

Every important category of runtime truth must have a clear owner.

Examples:
- equipment semantic truth -> `FusionDomain`
- scheduling orchestration truth -> `FusionScheduler`
- automation-facing projection truth -> `FusionFA`
- operator-facing presentation truth -> `FusionUI` view layer only, not business truth

### Do
- define who owns the authoritative state
- let other modules consume snapshots, views, events, queries, or commands
- keep ownership explicit in docs and code

### Do Not
- let multiple modules silently co-own mutable truth
- treat shared files or shared objects as implicit truth without ownership rules
- let UI become the fallback business truth source

---

## Rule 3: Shared Mutable Global State Is Forbidden

Multi-process architecture breaks down quickly when code relies on hidden global mutable state.

### Forbidden Patterns
- static mutable business state
- singleton objects that hold evolving business truth
- process-wide caches used as the only source of truth
- shared mutable context objects passed across module boundaries

### Allowed Patterns
- immutable snapshots
- explicit caches with ownership and invalidation rules
- read models
- query results
- command DTOs
- event payloads with minimal facts

---

## Rule 4: Domain Events Are Not Automatically IPC Messages

A `DomainEvent` represents an internal semantic fact.

It is **not automatically**:
- an IPC message
- a wire contract
- a durable integration event
- a protocol payload

### Correct Interpretation
- Domain events belong to semantic modeling
- IPC messages belong to inter-process transport design
- integration events belong to explicit external or cross-boundary contracts

### Do
- keep domain events lightweight
- use domain events to express facts
- create dedicated IPC/integration contracts later when needed

### Do Not
- expose domain events directly as transport contracts by default
- pollute domain events with transport metadata
- treat event serialization shape as the domain model

---

## Rule 5: Contracts Must Be Serialization-Friendly

Anything likely to cross a process boundary later should be easy to serialize and reconstruct.

### Prefer
- explicit IDs
- explicit states
- simple enums
- lightweight value objects
- flat or shallow data structures
- small payloads

### Avoid
- deep mutable object graphs
- circular references
- implicit object identity dependencies
- runtime-only object handles
- framework-heavy object types in contracts

### Good Examples
- `MaterialId`
- `EquipmentId`
- `RoutePlan`
- `DispatchTask`
- `AutomationJobView`
- `RemoteCommandRequest`

### Bad Examples
- returning a live aggregate with rich mutable navigation graph
- storing direct object references to other process-owned modules
- passing UI view models into non-UI layers

---

## Rule 6: Separate Domain Objects, Scheduler Context, and FA Views

FusionCore intentionally distinguishes three kinds of models:

### 1. Domain Truth
Owned by `FusionDomain`
Examples:
- `Equipment`
- `Material`
- `Carrier`
- `Substrate`
- `ControlJob`
- `ProcessJob`
- `Recipe`
- `Alarm`

### 2. Scheduler Context
Owned by `FusionScheduler`
Examples:
- `MaterialContext`
- `ProductionJobContext`
- `RoutePlan`
- `DispatchTask`
- `RecoveryPlan`

### 3. Automation Projection
Owned by `FusionFA`
Examples:
- `EquipmentAutomationSnapshot`
- `AutomationJobView`
- `AutomationMaterialView`
- `AutomationAlarmView`
- `AutomationRecipeView`

### Do
- preserve the distinction
- reuse domain semantics where appropriate
- keep scheduler models orchestration-focused
- keep FA models projection-focused

### Do Not
- duplicate domain truth in FA
- put scheduling models into Domain
- let UI invent business objects already owned elsewhere

---

## Rule 7: Process-Private vs Shared Runtime Files Must Stay Explicit

Because FusionCore is multi-process, file placement matters.

### Shared Runtime Artifacts
Examples:
- config instances
- selected databases
- selected snapshots
- backup files

These need ownership rules.

### Process-Private Runtime Artifacts
Examples:
- process logs
- temp files
- pid files
- lock files
- pipe/socket endpoints
- health status files

These must remain isolated by process.

### Do
- keep shared vs process-private explicit
- name files and directories by ownership
- avoid accidental cross-process file contention

### Do Not
- let multiple processes write the same log file
- let temp files become business truth
- let uncontrolled file sharing become an informal API

---

## Rule 8: Scheduler Must Consume Facts, Not Own Everything

`FusionScheduler` is an internal orchestration layer, not the whole platform brain.

### Scheduler Should Consume
- domain events
- queries
- snapshots
- module abstractions
- route and recovery policies

### Scheduler Should Not Own
- protocol runtime
- hardware execution
- direct UI state
- global mutable business truth outside its orchestration scope

### Do
- let Scheduler react to domain facts
- let Scheduler maintain orchestration context
- keep scheduler contracts focused

### Do Not
- let Scheduler become the source of equipment semantic truth
- let Scheduler directly control hardware drivers
- let Scheduler publish factory automation directly

---

## Rule 9: FA Must Project, Not Orchestrate

`FusionFA` exists to project internal semantics outward.

### FusionFA Owns
- automation-facing contracts
- views and snapshots
- mapping semantics
- remote command boundary definitions

### FusionFA Does Not Own
- scheduling truth
- hardware execution
- deep domain lifecycle logic
- full protocol runtime in early phases

### Do
- use domain semantics as source input
- keep FA models lightweight and external-facing

### Do Not
- add internal orchestration logic to FA
- duplicate domain entities as if they were the truth source
- turn FA into a process coordinator

---

## Rule 10: UI Must Remain Presentation-Oriented

`FusionUI` may be process-local, but it must still behave as a client of the platform.

### Do
- use views, snapshots, queries, commands, and operator actions
- keep UI models presentation-oriented
- align with E95 information architecture

### Do Not
- store business truth in UI state
- directly mutate scheduler or FA internals
- bypass contracts for convenience

---

## Rule 11: Avoid “Convenience Coupling”

A common failure mode is introducing single-process shortcuts because they are easy locally.

Examples:
- directly injecting a concrete scheduler implementation into unrelated modules
- directly mutating another module’s context object
- sharing a singleton state cache between multiple module layers
- reading another process-owned file format as if it were a stable API

### Ask Before Doing It
1. Is this still valid if the collaborator becomes a different process later?
2. Does this leak ownership?
3. Does this assume shared memory?
4. Does this bypass an existing boundary?

If the answer is risky, stop and redesign.

---

## Rule 12: Design for Delay, Failure, and Reordering

Multi-process systems must tolerate:
- delay
- temporary unavailability
- partial failure
- retries
- reordering
- duplicate delivery in future IPC/message designs

Even if Phase 1 is still mostly local skeleton work, avoid assumptions that later break under multi-process conditions.

### Do
- design commands and events so they are understandable on their own
- use IDs instead of object references
- keep context update behavior explicit

### Do Not
- assume all interactions are immediate
- assume no duplication can occur
- assume one callback equals one final state transition

---

## Rule 13: Keep Early-Phase Code Small and Explicit

During Phase 1 and early Phase 2:
- prefer clear contracts
- prefer small payloads
- prefer explicit ownership
- prefer stable naming
- prefer simple tests

Do not:
- build speculative infrastructure
- add message brokers early
- add process managers before contracts are stable
- over-abstract future IPC runtime before actual needs are clear

---

## Rule 14: Review Checklist

When reviewing a change, ask:

1. Does this assume everything runs in one process?
2. Does this rely on shared mutable state?
3. Is the truth owner still clear?
4. Can this contract survive later IPC/RPC proxying?
5. Are domain events still just semantic facts?
6. Are scheduler models still orchestration-focused?
7. Are FA models still projection-focused?
8. Is UI still only presentation-oriented?
9. Is file ownership explicit?
10. Would this still make sense if modules were split into separate executables?

If several answers are “no”, the change is not multi-process safe.

---

## Rule 15: Practical Examples

### Good
- `MaterialLocationChangedEvent` carries `MaterialId` and a lightweight `LocationReference`
- `PublishEquipmentStateCommand` carries explicit state values
- `AutomationJobView` is a projection model, not a domain entity
- `RoutePlan` expresses orchestration intent, not hardware instructions

### Bad
- returning a mutable `Equipment` aggregate from a long-lived shared singleton
- letting UI directly mutate scheduler context
- letting FA publish based on hidden internal state rather than explicit domain facts
- passing live module implementation objects across layer boundaries

---

## Summary

FusionCore must remain:
- multi-process friendly
- ownership-driven
- serialization-friendly
- boundary-aware
- explicit about truth vs view vs context

The architecture is safe when:
- domain truth is stable
- scheduler context is internal and bounded
- FA projection is external-facing
- UI remains presentation-only
- hidden in-process assumptions are avoided
