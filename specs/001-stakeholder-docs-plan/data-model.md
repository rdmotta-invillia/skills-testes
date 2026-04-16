# Data Model - RSS Subscription Management MVP

## Entity: Subscription

Purpose: Represents one RSS/Atom feed URL tracked by the user.

Fields:

- `id` (string, required): Unique identifier (GUID string) created by backend.
- `url` (string, required): Feed URL provided by user; trimmed, non-empty.
- `createdAtUtc` (string datetime, required): UTC timestamp set at insertion.

Validation rules:

- `url` MUST be non-null, non-empty, and not whitespace.
- `url` SHOULD be stored in trimmed form.
- `url` uniqueness is enforced on normalized value (`Trim()`, case-insensitive) for MVP duplicate prevention.

State transitions:

- `Created`: Subscription accepted and stored in memory.
- No update/delete transitions in MVP scope.

## Entity: AddSubscriptionRequest

Purpose: Request DTO for creating a new subscription.

Fields:

- `url` (string, required): Candidate feed URL entered by user.

Validation rules:

- `url` MUST be non-empty after trim.

## Entity: AddSubscriptionResponse

Purpose: Response DTO returned after successful creation.

Fields:

- `id` (string, required)
- `url` (string, required)
- `createdAtUtc` (string datetime, required)

## Entity: ListSubscriptionsResponse

Purpose: API list response for current subscriptions.

Fields:

- `subscriptions` (array of `Subscription`, required)

## Relationships

- `ListSubscriptionsResponse.subscriptions` contains zero or more `Subscription` records.
- `AddSubscriptionRequest` produces one `AddSubscriptionResponse` when successful.

## In-Memory Repository Contract

`ISubscriptionRepository` operations:

- `IReadOnlyList<Subscription> GetAll()`
- `Subscription Add(string url)`
- `bool Exists(string normalizedUrl)`

Invariants:

- Repository operations are deterministic for normalized URL input.
- Returned lists preserve insertion order for MVP simplicity.
