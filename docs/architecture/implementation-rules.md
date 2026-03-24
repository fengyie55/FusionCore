# FusionCore Implementation Rules

## 1. Purpose

This document defines the implementation constraints for FusionCore and is binding for:
- Codex-assisted code generation
- manual development
- code review
- architecture review

FusionCore is an architecture-driven semiconductor equipment control platform.
Implementation must follow approved architecture documents before convenience, speed, or local coding preference.

---

## 2. Required Reading Order

Before making non-trivial changes, read in the following order:

1. `AGENTS.md`
2. `docs/architecture/` documents
3. `docs/standards/` repository-safe SEMI notes
4. `project-structure.md`
5. this file

If there is a conflict:
- architecture documents win over implementation convenience
- repository-safe standards notes win over undocumented assumptions
- this file wins over ad hoc coding style decisions

---

## 3. Core Principles

### 3.1 Architecture First
Follow approved architecture boundaries first, then implement.

### 3.2 Domain First
Keep domain semantics stable and independent from UI, transport, persistence, and hardware implementation details.

### 3.3 Standard-Aware, Not Standard-Guessing
Use approved SEMI-aligned repository-safe notes and architecture documents.
Do not invent unapproved protocol behavior.

### 3.4 Abstract Before Implement
Define contracts, responsibilities, and ownership clearly before adding concrete behavior.

### 3.5 Minimal, Evolvable Skeletons
During early phases, prefer small stable skeletons over premature “complete” implementations.

### 3.6 Multi-Process Friendly by Default
FusionCore is designed for a multi-process architecture, not a single large in-process monolith.
Do not assume that module interaction is always in-process method invocation.

---

## 4. Layering Rules

### 4.1 UI Layer
`FusionUI` may depend on:
- `FusionApp`
- `FusionDomain`
- approved UI-facing abstractions

`FusionUI` must not directly depend on:
- hardware drivers
- IO / Motion / Com implementations
- FA protocol implementations
- scheduler execution internals

### 4.2 Domain Layer
`FusionDomain` must not depend on:
- WPF
- database frameworks
- network stacks
- protocol stacks
- driver implementations
- UI frameworks
- event buses / brokers / message middleware

`FusionDomain` owns semantic truth, not transport or infrastructure behavior.

### 4.3 Scheduler Layer
`FusionScheduler` may depend on:
- `FusionDomain`
- `FusionEquipment.Abstractions`
- approved scheduler-local abstractions

`FusionScheduler` must not:
- directly control IO / Motion / Com implementations
- contain concrete PM/TM/CM/BM implementations
- implement factory communication logic
- assume synchronous in-process access to all collaborators

### 4.4 Factory Automation Layer
`FusionFA` may depend on:
- `FusionDomain`
- approved FA-local abstractions

`FusionFA` must not:
- own internal scheduling logic
- directly control equipment hardware
- become the source of domain truth
- implement unapproved protocol behavior in early phases

### 4.5 Equipment Abstraction Layer
`FusionEquipment.Abstractions` contains:
- role-oriented module contracts
- lifecycle contracts
- capability abstractions
- lightweight context abstractions

It must not contain:
- concrete PM/TM/CM/BM implementations
- scheduler logic
- FA logic
- UI logic

### 4.6 Infrastructure and Future Modules
Infrastructure modules must not redefine business truth that belongs to `FusionDomain`.

---

## 5. Module Boundary Rules

### 5.1 FusionDomain
Owns:
- semantic truth
- identifiers
- enums
- value objects
- entities
- aggregates
- domain events

Does not own:
- UI interaction logic
- protocol implementation
- hardware execution
- process/session transport behavior

### 5.2 FusionScheduler
Owns:
- orchestration contracts
- route planning boundaries
- material flow intent
- dispatch intent
- recovery planning boundaries

Does not own:
- hardware driver execution
- FA publication
- protocol runtime
- domain truth persistence

### 5.3 FusionFA
Owns:
- factory-facing projection contracts
- automation-facing views and snapshots
- remote command boundary contracts
- mapping semantics

Does not own:
- internal orchestration truth
- direct hardware actions
- WPF UI behavior
- protocol runtime in early phases unless explicitly approved

### 5.4 FusionUI
Owns:
- presentation
- navigation
- operator / engineer interaction structure
- E95-aligned HMI layout baseline

Does not own:
- domain truth
- hardware control logic
- scheduler algorithms
- FA protocol behavior

### 5.5 PM / TM / CM / BM
Future concrete equipment modules:
- execute module-local behavior
- expose capability and status through approved abstractions

They must not:
- decide global routing
- become the system scheduler
- become the automation publication layer

---

## 6. Multi-Process Architecture Rules

### 6.1 Do Not Assume In-Process Execution
All service contracts must remain process-boundary-friendly.

Do not assume:
- all interfaces are local method calls
- all collaborators share the same memory space
- all calls are low-latency and synchronous
- callbacks always execute immediately

### 6.2 Process-Friendly Contracts
Contracts should be designed so they can later be implemented as:
- local in-process services
- IPC proxies
- RPC facades
- command endpoints
- event/message consumers

### 6.3 Shared Mutable Global State Is Forbidden
Do not treat shared mutable in-memory objects as the only source of truth.

Avoid:
- global mutable singletons
- static runtime state carrying business truth
- hidden cross-module shared state
- direct object mutation across module boundaries

### 6.4 Truth Must Be Owned, Not Implicitly Shared
Runtime truth must have an owning module or owning process.

Examples:
- domain truth belongs to `FusionDomain` semantics
- scheduling truth belongs to scheduler-side orchestration context
- automation projection truth belongs to `FusionFA` views/snapshots

Consumers should use:
- commands
- events
- snapshots
- queries
instead of direct shared object ownership.

### 6.5 Serialization-Friendly Models
Design contracts and models to be serialization-friendly.

Prefer:
- explicit identifiers
- explicit state fields
- lightweight value objects
- stable contract shapes

Avoid:
- passing deep mutable object graphs
- returning internal runtime references
- requiring object identity from shared memory assumptions

### 6.6 Domain Events Are Not Automatically IPC Messages
A `DomainEvent` is an internal semantic fact.
It is not automatically a cross-process integration message.

If cross-process messaging is needed later, define explicit IPC/integration contracts.
Do not assume domain events can be freely exposed as transport contracts unchanged.

---

## 7. Collaboration Rules

### 7.1 Preferred Collaboration Mechanisms
Cross-module collaboration should prefer:
- interfaces
- contracts
- domain events
- commands
- queries
- snapshots / views

### 7.2 Avoid Hidden Coupling
Avoid:
- direct instantiation of concrete cross-module implementations
- circular references
- hidden callbacks that assume in-process behavior
- undocumented state sharing

### 7.3 Dependency Injection Preferred
Prefer dependency injection or explicit factory patterns over direct construction of collaborators.

---

## 8. Domain Event Rules

### 8.1 Events Express Facts, Not Handling Logic
Events should express what happened, not how it must be processed.

### 8.2 Keep Event Payloads Small
Prefer:
- IDs
- state transitions
- minimal location references
- lightweight summaries

Avoid:
- entire aggregates in event payloads
- transport-specific fields
- infrastructure-specific metadata unless explicitly approved

### 8.3 Keep DomainEvent Lightweight
Do not evolve `DomainEvent` into a complex infrastructure base class.

Do not add:
- broker routing metadata
- transport serialization frameworks
- outbox-specific fields
- middleware-specific dependencies
unless explicitly approved later.

---

## 9. Naming Rules

### 9.1 Project Names
Use `FusionXxx` naming consistently.

### 9.2 Interfaces
Interfaces start with `I`.

### 9.3 Structure
Use clear directories and namespaces such as:
- `Contracts`
- `Events`
- `Enums`
- `ValueObjects`
- `Mappings`
- `Models`
- `Queries`
- `Commands`

### 9.4 Semantic Naming
Use names that reflect semantic ownership:

Examples:
- internal truth: `Equipment`, `ControlJob`, `Material`
- scheduler context: `MaterialContext`, `RoutePlan`
- FA projection: `AutomationJobView`, `EquipmentAutomationSnapshot`

Avoid reusing the same concept name for multiple different meanings.

---

## 10. Repository-Safe Standards Rules

Use only repository-safe summaries and approved architecture interpretations when implementing.

Do not:
- copy confidential standards text into code or repo docs
- reproduce licensed PDF contents verbatim
- claim support or compliance not actually implemented

Prefer:
- `docs/standards/semi-mapping-matrix.md`
- `docs/standards/semi-glossary.md`
- `docs/standards/semi-phase1-scope.md`
- quick-notes files

---

## 11. Phase-1 Constraints

During Phase 1, do NOT:
- implement full SECS/GEM/HSMS behavior
- implement production scheduling algorithms
- implement recovery algorithms
- introduce database coupling unless explicitly required
- create concrete PM/TM/CM/BM implementations
- add message bus / broker / IPC runtime without approval
- collapse boundaries for convenience

Phase 1 is for:
- solution structure
- contracts
- skeletons
- mappings
- event catalogs
- minimal tests
- architecture-safe evolution

---

## 12. Testing Rules

Tests in early phases should verify:
- projects compile
- contracts instantiate
- skeletons are structurally correct
- boundary-safe assumptions hold

Do not overbuild integration/runtime tests before architecture boundaries are stable.

Where helpful, add architecture-safe tests that validate:
- project references remain correct
- forbidden dependencies are not introduced

---

## 13. Review Rules

Any pull request or Codex-generated change must be reviewed for:

1. boundary compliance
2. project reference safety
3. semantic consistency with `FusionDomain`
4. process-boundary friendliness
5. repository-safe standards usage
6. avoidance of premature infrastructure complexity

Any new project reference must be justified.

Any cross-layer shortcut should be rejected unless explicitly approved.

---

## 14. Practical Red Flags

Changes should be questioned if they:
- add static shared business state
- assume direct object access across module boundaries
- put scheduling logic into FA
- put publication logic into Scheduler
- put business truth into UI
- couple Domain to infrastructure
- introduce hidden single-process assumptions
- make DomainEvent transport-specific

---

## 15. Definition of Good Early-Phase Code

Good early-phase FusionCore code is:
- small
- explicit
- layered
- naming-consistent
- testable
- serialization-friendly
- process-boundary-friendly
- easy to move into multi-process runtime topology later

---

## 16. Revision History

| Version | Date | Author | Notes |
|---|---|---|---|
| v0.1 | 2026-03-23 | OpenAI / ChatGPT | Initial baseline |
| v0.2 | 2026-03-24 | OpenAI / ChatGPT | Added multi-process architecture rules and process-boundary-friendly constraints |
