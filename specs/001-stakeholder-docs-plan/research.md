# Phase 0 Research - RSS Subscription Management MVP

## Decision 1: Keep MVP persistence in memory behind repository abstraction

- Decision: Implement `ISubscriptionRepository` backed by an in-memory `List<Subscription>` for MVP.
- Rationale: Satisfies MVP speed/YAGNI goals while preserving clean upgrade path to EF Core + SQLite later.
- Alternatives considered:
  - Direct `List<T>` usage in controller (rejected: violates separation of concerns).
  - SQLite in MVP (rejected: unnecessary complexity and constitution violation for MVP phase).

## Decision 2: Input validation level for subscription URL

- Decision: Validate only non-empty/whitespace input in MVP; do not perform network-level or feed-format validation.
- Rationale: Matches stakeholder scope and avoids introducing feed fetching/parsing in MVP.
- Alternatives considered:
  - Full URL format + HTTP reachability validation (rejected: adds network behavior and non-MVP complexity).
  - Accept any raw string including empty (rejected: poor UX and avoidable invalid state).

## Decision 3: Duplicate subscription behavior

- Decision: Prevent exact duplicate URLs (case-insensitive, trimmed) and return HTTP 409 conflict for duplicates.
- Rationale: Improves UX with minimal complexity and deterministic behavior for list management.
- Alternatives considered:
  - Allow duplicates (rejected: confusing list state with little product value).
  - Global canonical URL normalization rules (rejected: too complex for MVP).

## Decision 4: API contract style

- Decision: Expose REST endpoints under `/api/subscriptions` with JSON request/response DTOs.
- Rationale: Native fit for ASP.NET Core Web API and straightforward consumption by Blazor HttpClient.
- Alternatives considered:
  - gRPC (rejected: unnecessary setup overhead for simple local MVP).
  - Minimal APIs only (deferred: controller-based organization aligns better with expected growth and tests).

## Decision 5: Test strategy and tooling

- Decision: Use xUnit for unit/integration tests, bUnit for Blazor component tests, `WebApplicationFactory<Program>` for API integration, and CI gate for >=80% service-layer coverage.
- Rationale: Directly satisfies constitution quality gates and is standard for .NET 8 stack.
- Alternatives considered:
  - Unit tests only (rejected: misses required integration/component coverage).
  - End-to-end browser tests only (rejected: too heavy for MVP and weaker service isolation).

## Decision 6: CORS policy for local dev

- Decision: Explicitly allow frontend origin(s) from configured launch profiles; forbid wildcard origin.
- Rationale: Required by constitution security-first principle and avoids accidental overexposure.
- Alternatives considered:
  - `AllowAnyOrigin()` (rejected: explicit constitution violation).
  - Disable CORS during local dev (rejected: hides integration issues and breaks browser flow).

## Decision 7: Frontend template cleanup timing

- Decision: Remove default Blazor template pages (`Home`, `Counter`, `Weather`) before implementing feature pages.
- Rationale: Prevents ambiguous route runtime failures and keeps UI scope focused.
- Alternatives considered:
  - Keep template pages and add feature routes later (rejected: route conflict risk and extra noise).
