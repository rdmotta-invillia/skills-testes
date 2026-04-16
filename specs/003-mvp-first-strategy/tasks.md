---
description: "Task list for RSS Feed Reader MVP feature implementation"
---

# Tasks: RSS Feed Reader MVP

**Input**: Design documents from `specs/002-rss-reader-mvp/`  
**Plan**: `plan.md` | **Specification**: `spec.md`

**Dependencies**: All Setup and Foundational tasks must complete before starting user story tasks.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[ID]**: Task identifier (T001, T002, etc.) in execution order
- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (US1, US2, US3)
- Include exact file paths in descriptions

## Implementation Strategy

**MVP Scope**: All three user stories (US1, US2, US3) are Priority P1 and must be completed for the MVP to demonstrate value. However, they can be developed in phases:

1. **Foundation First**: Setup phase and backend foundation enable all user stories
2. **Backend Complete**: User Story 3 (In-Memory Storage) is actually infrastructure supporting US1 and US2
3. **Feature Delivery Order**: US3 → US1 → US2 (storage enables adding/displaying subscriptions)
4. **Parallel Opportunities**: Frontend components (US2) can be built in parallel with backend (US1)

**Suggested MVP Scope**: Complete all three user stories and their phases to achieve a functional MVP application.

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic solution structure

- [X] T001 Create ASP.NET Core solution with two projects in `src/RssReaderApi.sln`
- [X] T002 [P] Create ASP.NET Core Web API project at `src/RssReaderApi/RssReaderApi.csproj` with required dependencies
- [X] T003 [P] Create Blazor WebAssembly project at `src/RssReaderClient/RssReaderClient.csproj` with required dependencies
- [X] T004 Create main entry point and host configuration in `src/RssReaderApi/Program.cs`
- [X] T005 Configure Blazor WASM app initialization in `src/RssReaderClient/Program.cs`

---

## Phase 2: Foundational (Infrastructure & Storage)

**Purpose**: Backend foundation that enables all user stories

**Blocking Dependencies**: All Phase 1 tasks

- [X] T006 Create `Subscription` model class in `src/RssReaderApi/Models/Subscription.cs` with Id and Url properties
- [X] T007 Create `SubscriptionService` in-memory service in `src/RssReaderApi/Services/SubscriptionService.cs` with methods: GetAll(), AddSubscription(url)
- [X] T008 Register `SubscriptionService` in dependency injection container in `src/RssReaderApi/Program.cs`
- [X] T009 Create `SubscriptionsController` in `src/RssReaderApi/Controllers/SubscriptionsController.cs` with GET and POST endpoints
- [X] T010 Implement `GET /api/subscriptions` endpoint to return all subscriptions in `src/RssReaderApi/Controllers/SubscriptionsController.cs`
- [X] T011 Implement `POST /api/subscriptions` endpoint to add a new subscription in `src/RssReaderApi/Controllers/SubscriptionsController.cs`
- [X] T012 Configure CORS policy in `src/RssReaderApi/Program.cs` to allow Blazor frontend requests

---

## Phase 3: User Story 1 - Add Feed Subscription (P1)

**Purpose**: Enable users to add RSS/Atom feed subscriptions

**Goal**: Users can enter a feed URL and add it as a subscription

**Independent Test Criteria**:

- User can submit a subscription URL via the frontend form
- Backend receives the POST request and stores the subscription
- Subscription appears in the GetAll() response
- Form clears after successful submission

**Blocking Dependencies**: All Phase 2 tasks

- [X] T013 [P] [US1] Create `SubscriptionForm.razor` component in `src/RssReaderClient/Components/SubscriptionForm.razor` with URL input field and Add button
- [X] T014 [US1] Implement form submission logic in `src/RssReaderClient/Components/SubscriptionForm.razor` to POST to `/api/subscriptions`
- [X] T015 [P] [US1] Implement HTTP client injection and API call method in `src/RssReaderClient/Components/SubscriptionForm.razor`
- [X] T016 [US1] Add form validation feedback in `src/RssReaderClient/Components/SubscriptionForm.razor` (e.g., error messages on failed submission)
- [X] T017 [US1] Emit event or callback from `SubscriptionForm.razor` to notify parent when subscription is successfully added

---

## Phase 4: User Story 2 - Display Subscription List (P1)

**Purpose**: Show users all their current subscriptions

**Goal**: Users can see a list of all subscriptions they have added

**Independent Test Criteria**:

- Subscriptions are fetched from the backend API
- Each subscription URL is displayed in the list
- Empty state is shown when no subscriptions exist
- List updates when new subscriptions are added

**Blocking Dependencies**: All Phase 2 tasks; Phase 3 tasks (can run in parallel with Phase 3)

- [X] T018 [P] [US2] Create `SubscriptionList.razor` component in `src/RssReaderClient/Components/SubscriptionList.razor` to display subscriptions
- [X] T019 [P] [US2] Implement HTTP GET call to `/api/subscriptions` in `src/RssReaderClient/Components/SubscriptionList.razor`
- [X] T020 [P] [US2] Add markup to display subscription list with foreach loop in `src/RssReaderClient/Components/SubscriptionList.razor`
- [X] T021 [US2] Implement empty state display in `src/RssReaderClient/Components/SubscriptionList.razor` when no subscriptions exist
- [X] T022 [P] [US2] Create `Index.razor` page in `src/RssReaderClient/Pages/Index.razor` to orchestrate SubscriptionForm and SubscriptionList components
- [X] T023 [US2] Implement data refresh mechanism in `Index.razor` to reload subscriptions after new subscription is added (subscribe to SubscriptionForm events)

---

## Phase 5: User Story 3 - In-Memory Storage (P1)

**Purpose**: Verify persistence behavior meets MVP requirements

**Goal**: Subscriptions persist for the duration of the session and are cleared on app restart

**Independent Test Criteria**:

- Multiple subscriptions can be added and all persist in memory
- Page refresh clears subscriptions (demonstrating in-memory storage is tied to backend session)
- Subscriptions remain available until browser/backend session ends

**Blocking Dependencies**: All Phase 2, 3, and 4 tasks

**Note**: This is mainly a validation phase; the storage mechanism is built into Phase 2. This phase documents the behavior and performs integration testing.

- [X] T024 [US3] Perform manual integration test: Add multiple subscriptions via the UI and verify all are displayed
- [X] T025 [US3] Perform manual integration test: Refresh the browser page and verify subscriptions are gone (confirming in-memory storage)
- [X] T026 [US3] Verify `SubscriptionService` maintains state across multiple API calls within a session in `src/RssReaderApi/Services/SubscriptionService.cs`
- [X] T027 [US3] Document expected behavior of in-memory storage in code comments and README

---

## Phase 6: Polish & Integration

**Purpose**: Verification, documentation, and final cleanup

- [X] T028 Create `README.md` with instructions to build and run the application
- [X] T029 [P] Add basic styling to `Index.razor` for improved UI appearance
- [X] T030 [P] Add input validation in `SubscriptionForm.razor` to prevent empty URLs
- [X] T031 Test application end-to-end: start backend, start frontend, add subscription, refresh, verify behavior
- [X] T032 Clean up any console logs or debug code from production builds

---

## Dependency Graph

```
Phase 1: Setup
  ↓
Phase 2: Foundational
  ├→ Phase 3: User Story 1 (Add Subscription) [Frontend form + Backend endpoint]
  ├→ Phase 4: User Story 2 (Display List) [Frontend list + API integration]
  └→ Phase 5: User Story 3 (In-Memory Storage) [Integration testing & validation]
  ↓
Phase 6: Polish & Integration
```

**Critical Path**: T001 → T004 → T006-T012 → (T013-T017 and T018-T023 in parallel) → T024-T027 → T028-T032

**Parallel Execution Examples**:

- **After T005**: T006 and T007 can run in parallel (model and service, don't depend on each other)
- **After T012**: T013-T015, T018-T021 can run in parallel (frontend components don't depend on each other until T023)
- **After T017**: T018-T020 and T022 can proceed in parallel with reviewing Phase 3
- **In Phase 6**: T029 and T030 can run in parallel (independent frontend improvements)

---

## Completion Checklist

- [X] All Setup tasks (T001-T005) are green
- [X] All Foundational tasks (T006-T012) are green
- [X] All US1 tasks (T013-T017) are green
- [X] All US2 tasks (T018-T023) are green
- [X] All US3 tasks (T024-T027) are green
- [X] All Polish tasks (T028-T032) are green
- [X] Application builds successfully without errors
- [X] Application runs on localhost (backend and frontend)
- [X] Manual integration test passes: add subscription → view in list → refresh → list clears
- [X] MVP requirements from plan.md are satisfied
