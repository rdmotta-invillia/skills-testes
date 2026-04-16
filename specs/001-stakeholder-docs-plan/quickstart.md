# Quickstart - RSS Subscription Management MVP

## Prerequisites

- .NET SDK 8.0+ (aligned with `global.json`)
- Node/npm optional (only if frontend tooling requires it)
- Local ports available for backend and frontend launch profiles

## 1. Create/restore solution projects

```bash
dotnet restore
```

If projects are not created yet, scaffold them according to this plan structure:

- `backend/RSSFeedReader.Api`
- `frontend/RSSFeedReader.UI`
- `shared/RSSFeedReader.Shared`
- test projects under `tests/`

## 2. Run backend

```bash
dotnet run --project backend/RSSFeedReader.Api
```

Expected:

- API starts successfully on configured localhost port.
- CORS policy allows only configured frontend origin(s).

## 3. Run frontend

```bash
dotnet run --project frontend/RSSFeedReader.UI
```

Expected:

- Subscriptions page loads.
- API base URL resolves to backend URL from `wwwroot/appsettings.json`.

## 4. Verify MVP scenarios manually

1. Add a non-empty URL (example: `https://devblogs.microsoft.com/dotnet/feed/`) and confirm it appears in list.
2. Add the same URL again and confirm duplicate is rejected with clear feedback.
3. Refresh page and verify list reloads from backend in-memory store.
4. Restart backend and verify list resets (in-memory behavior).

## 5. Execute tests

```bash
dotnet test
```

Required coverage:

- Unit tests: repository/service happy path + edge/failure cases.
- Integration tests: add/list API contracts using `WebApplicationFactory<Program>`.
- Component tests: Blazor add/list interactions using bUnit.
- Service-layer coverage gate >= 80%.

## 6. Security and quality checks

- Confirm no wildcard CORS policy.
- Confirm nullable reference types enabled.
- Confirm no network feed-fetch/parsing endpoints exist in MVP implementation.
- Confirm no TODO/FIXME without issue reference in changed files.
