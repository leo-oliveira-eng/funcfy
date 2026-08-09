# Tasks: Maybe Helper Methods

**Input**: Design documents from `/specs/001-maybe-helper-methods/`
**Prerequisites**: [plan.md](./plan.md), [spec.md](./spec.md), [research.md](./research.md), [data-model.md](./data-model.md), [quickstart.md](./quickstart.md), [contracts/maybe-public-api.md](./contracts/maybe-public-api.md)

**Tests**: Add or update automated tests for every new `Maybe<TValue>` helper and for the observable `Match` guard-clause behavior change before implementation is considered complete.

**Organization**: Tasks are grouped by user story to preserve independent implementation and verification of composition, recovery, and observation behavior.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel when the task touches different files and has no incomplete dependency
- **[Story]**: Maps a task to a user story from `spec.md` (`[US1]`, `[US2]`, `[US3]`)
- Every task includes the exact file path that should be changed or reviewed

## Phase 1: Setup (Shared Context)

**Purpose**: Confirm the existing code, test, and documentation baselines before edits begin

- [ ] T001 [P] Review helper parity targets in `src/Funcfy/Monads/Maybe.cs`, `src/Funcfy/Monads/Either.cs`, and `specs/001-maybe-helper-methods/contracts/maybe-public-api.md`
- [ ] T002 [P] Review current `Maybe<TValue>` test and documentation baselines in `tests/Funcfy.Tests/MonadsTests/MaybeTests/MatchUnitTests.cs`, `docs/maybe.md`, and `docs/README.md`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Lock down shared guard-clause behavior before story-specific helpers are added

**CRITICAL**: No user story implementation should start until this phase is complete

- [ ] T003 [P] Add null-delegate regression coverage for `Maybe<TValue>.Match` in `tests/Funcfy.Tests/MonadsTests/MaybeTests/GuardUnitTests.cs`
- [ ] T004 Update `src/Funcfy/Monads/Maybe.cs` to validate delegates in both `Match` overloads with explicit guard clauses

**Checkpoint**: Shared `Maybe<TValue>` guard behavior is explicit and test-covered

---

## Phase 3: User Story 1 - Compose Optional Values (Priority: P1) MVP

**Goal**: Enable `Maybe<TValue>` composition through `Map` and `Bind` while preserving full/empty semantics

**Independent Test**: Chain full and empty `Maybe<TValue>` values through `Map` and `Bind` and verify full-path transformation, empty-path short-circuiting, and invalid null binder results

### Tests for User Story 1

- [ ] T005 [P] [US1] Add `Map` behavior coverage for full and empty instances in `tests/Funcfy.Tests/MonadsTests/MaybeTests/MapUnitTests.cs`
- [ ] T006 [P] [US1] Add `Bind` behavior and null-result coverage in `tests/Funcfy.Tests/MonadsTests/MaybeTests/BindUnitTests.cs`

### Implementation for User Story 1

- [ ] T007 [US1] Implement `Map` and `Bind` with XML comments and delegate guards in `src/Funcfy/Monads/Maybe.cs`

**Checkpoint**: User Story 1 is independently functional and testable

---

## Phase 4: User Story 2 - Recover From Absence (Priority: P2)

**Goal**: Provide raw-value and replacement-`Maybe<TValue>` recovery helpers that stay lazy on full instances

**Independent Test**: Exercise `GetOrElse` and `OrElse` against empty and full instances, verifying returned values, lazy fallback execution, same-instance preservation for full `OrElse`, and invalid null fallback results

### Tests for User Story 2

- [ ] T008 [US2] Add `GetOrElse` and `OrElse` recovery coverage in `tests/Funcfy.Tests/MonadsTests/MaybeTests/RecoveryUnitTests.cs`

### Implementation for User Story 2

- [ ] T009 [US2] Implement `GetOrElse` overloads and `OrElse` with XML comments in `src/Funcfy/Monads/Maybe.cs`

**Checkpoint**: User Stories 1 and 2 are independently functional and testable

---

## Phase 5: User Story 3 - Observe Optional Pipelines (Priority: P3)

**Goal**: Let consumers observe full values with `Tap` without altering the optional pipeline

**Independent Test**: Verify `Tap` executes only for full instances, preserves the wrapped state, and returns the original instance for both branches

### Tests for User Story 3

- [ ] T010 [US3] Add `Tap` observation coverage in `tests/Funcfy.Tests/MonadsTests/MaybeTests/TapUnitTests.cs`

### Implementation for User Story 3

- [ ] T011 [US3] Implement `Tap` with XML comments in `src/Funcfy/Monads/Maybe.cs`

**Checkpoint**: All user stories are independently functional and testable

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Finalize consumer-facing documentation, release notes, and end-to-end validation

- [ ] T012 [P] Update `Maybe<TValue>` helper guidance, examples, and semantic rationale in `docs/maybe.md`
- [ ] T013 [P] Update the current API surface summary for `Maybe<T>` in `docs/README.md`
- [ ] T014 [P] Add an `Unreleased` entry for the new helpers and `Match` guard behavior in `CHANGELOG.md`
- [ ] T015 Run the quickstart validation commands from `specs/001-maybe-helper-methods/quickstart.md` against `tests/Funcfy.Tests/Funcfy.Tests.csproj`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1: Setup**: No dependencies and can start immediately
- **Phase 2: Foundational**: Depends on Phase 1 and blocks all user story work
- **Phase 3: User Story 1**: Depends on Phase 2
- **Phase 4: User Story 2**: Depends on Phase 2; semantically independent from US1, but its implementation task shares `src/Funcfy/Monads/Maybe.cs`
- **Phase 5: User Story 3**: Depends on Phase 2; semantically independent from US1 and US2, but its implementation task shares `src/Funcfy/Monads/Maybe.cs`
- **Phase 6: Polish**: Depends on the user stories you intend to ship being complete

### User Story Dependencies

- **User Story 1 (P1)**: No dependency on other stories after the foundational phase; this is the MVP slice
- **User Story 2 (P2)**: No semantic dependency on US1 after the foundational phase, but the shared `Maybe.cs` file means implementation should be serialized with T007
- **User Story 3 (P3)**: No semantic dependency on US1 or US2 after the foundational phase, but the shared `Maybe.cs` file means implementation should be serialized with T007 and T009

### Within Each User Story

- Story-specific tests should be added before marking implementation complete
- Story implementation in `src/Funcfy/Monads/Maybe.cs` should follow its corresponding test task
- A story is complete only when its tests pass independently

### Parallel Opportunities

- T001 and T002 can run in parallel
- T003 can run while T004 is being planned, but T004 should not be considered complete until T003 exists
- T005 and T006 can run in parallel because they touch different test files
- T012, T013, and T014 can run in parallel because they touch different documentation files

---

## Parallel Example: User Story 1

```text
T005 [US1] Add `Map` behavior coverage in tests/Funcfy.Tests/MonadsTests/MaybeTests/MapUnitTests.cs
T006 [US1] Add `Bind` behavior and null-result coverage in tests/Funcfy.Tests/MonadsTests/MaybeTests/BindUnitTests.cs
```

## Parallel Example: Polish

```text
T012 Update docs/maybe.md
T013 Update docs/README.md
T014 Update CHANGELOG.md
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1
2. Complete Phase 2
3. Complete Phase 3
4. Run the targeted `MaybeTests` validation from `quickstart.md`
5. Stop and review the MVP before moving to recovery and observation helpers

### Incremental Delivery

1. Finish shared setup and foundational guard behavior
2. Deliver `Map` and `Bind` for composition
3. Deliver `GetOrElse` and `OrElse` for recovery
4. Deliver `Tap` for observation
5. Refresh docs and changelog, then run validation commands

### Parallel Team Strategy

1. One contributor handles foundational `Match` guard coverage and implementation
2. After Phase 2, test tasks for US1, US2, and US3 can be drafted in parallel
3. Because `src/Funcfy/Monads/Maybe.cs` is shared, helper implementation tasks T007, T009, and T011 should be merged sequentially

---

## Notes

- The feature has 15 tasks total
- All task lines follow the required checklist format: checkbox, task ID, optional `[P]`, optional story label, action, and exact file path
- The contract in `specs/001-maybe-helper-methods/contracts/maybe-public-api.md` is satisfied through `src/Funcfy/Monads/Maybe.cs` and the corresponding `MaybeTests` files
- No external API contract tests are needed because this feature changes a public in-process library type rather than a network or CLI interface
