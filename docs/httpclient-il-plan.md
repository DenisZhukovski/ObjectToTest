# HttpClient default-state detection via IL (Refined plan)

This document captures the current problem, desired behavior, and an implementation plan to make `ToTest` generate the simplest constructor call (e.g., `new HttpClient()`) when an object is in its default state, even if internal constructor chaining initializes default arguments. The plan is IL-driven and applies to BCL types as well.

## Current problem

- `HttpClientToTest` unit test expects:
  - `new HttpClient().ToTest(output, false)` → `new HttpClient()`
- Today, `ToTest` emits property initializers for `HttpClient` (e.g., `Timeout = ...`), so the test fails.
- Behavior around `HttpClient` that already works (must remain true):
  - No shared arguments (`SharedObjectsTests`, `ObjectSharedArgumentsTests`)
  - No circular references (`ObjectExtensionsTests`, `SharedCircularPropertiesTests`)
  - No multi-used fields (`ObjectsUsageGraphTests`)

Root cause:
- `DefaultConstructor.ToString()` always appends `ObjectProperties`.
- `ObjectProperties` currently treats `HttpClient` as a normal POCO and includes writable properties (e.g., `Timeout`) because default-value detection is insufficient and we don’t special-case framework types.

## Desired behavior

- For objects whose runtime state matches what their canonical constructor produces with its default arguments, generate only the simplest constructor expression:
  - Prefer `new T()` when available.
  - Otherwise `new T(consts...)` if that is the canonical constructor with known constant defaults.
- Suppress property initializers when the object matches the canonical default state.
- Apply IL analysis even to BCL types (including `HttpClient`).
- For framework types like `HttpClient`, treat non-reproducible internal state (e.g., `Timeout`) as ignorable when judging “default state,” so the output remains `new HttpClient()`.

## High-level approach

Use IL/decompiled constructor bodies to build a default-state model per type:
1. Analyze constructor IL/AST (via existing ILSpy integration) to understand ctor chaining and default arguments.
2. Derive a “default state descriptor” of member values that are produced by the canonical ctor using those default arguments.
3. At runtime, compare the current object to that descriptor:
   - If it matches (ignoring unknown/non-reproducible members), emit the simplest ctor form and no property initializers.
   - Otherwise, fall back to existing constructor selection and property initializer logic.

## Detailed steps

### 1) Constructor IL analysis service
- Reuse the ILSpy integration (already in the project).
- Provide a small internal service, e.g., `ConstructorAnalysisService`, that can:
  - Fetch all ctors for a type and their IL/decompiled bodies.
  - Detect `this(...)` or `base(...)` calls and their arguments.
  - Build a ctor graph to identify a canonical ctor (root of `this()` chain or common target).
  - Extract default argument expressions from ctor chaining:
    - Literal/constant arguments → record as known defaults.
    - Complex expressions (method calls, new complex objects) → mark as unknown/non-reproducible.

### 2) Default state descriptor per type
- From the canonical ctor body, trace assignments into fields/properties:
  - If assigned from literals/ctor params/simple value-type constructions → record as **known default**.
  - If assigned from complex expressions/external calls → record as **unknown**.
- Capture this per type in a descriptor (cached).
- Include ctor signature + default argument values (where known).

### 3) Runtime comparison to default state
- On `ToTest(obj)`:
  - Ensure the type has a public ctor we can analyze; use the cached descriptor.
  - Read public fields/properties using existing reflection helpers.
  - Compare actual values against **known defaults**:
    - Primitives/enums/strings/decimals/value types → `Equals`.
    - For reference types, avoid deep recursion; use existing `ContainsDeep`/`HasCircularReference` guards.
  - Ignore members marked **unknown** in the descriptor.
- If comparison succeeds → object is considered in default state.

### 4) Code generation integration
- If object is in default state:
  - Choose the simplest canonical expression:
    - If a public parameterless ctor exists and its body is just `this(consts...)` → emit `new T()`.
    - Else, if canonical ctor with known constant defaults exists → emit `new T(consts...)`.
  - Suppress property initializers (i.e., `ObjectProperties` should return empty for this case).
- If not in default state:
  - Use existing constructor selection (`ParametrizedConstructor`, `DefaultConstructor`, etc.) and existing property initializer logic.
  - For framework/BCL types, continue to avoid property initializers unless confidently reproducible.

### 5) Scope, performance, and fallback
- Apply IL analysis to **all types**, including BCL (per requirement).
- Cache per-type analysis to limit overhead.
- If IL analysis fails or is too complex:
  - Fall back to current behavior (no regression).
  - Optionally log/trace for diagnostics.

### 6) Testing strategy
- Keep the existing suite green:
  - `HttpClient` tests: expect `new HttpClient()` (no properties, no shared args, no circular refs).
  - Other ToTest tests that rely on property initialization must remain unchanged (they are not in default state or not in BCL).
- Add/adjust targeted tests:
  - A type with a secondary ctor that chains to a primary ctor with defaults; ensure simplest ctor is emitted.
  - A BCL sample (including `HttpClient`) to confirm simplification to `new Type()`.
  - Optional: a case where a property is changed to a non-default value to ensure we exit the “default state” fast path and still behave as today (or keep simplifying for framework types if deemed non-reproducible).

## Notes and decisions already made
- It is acceptable that we might emit `new T()` even if some deep internal state (unknown members) differs; unknowns are ignored for default-state matching.
- IL analysis should include BCL types despite potential cost.
- Always prefer the simplest ctor syntax when the state matches the canonical default.
- For framework types like `HttpClient`, non-reproducible properties (e.g., `Timeout`) should not block the default-state simplification.

