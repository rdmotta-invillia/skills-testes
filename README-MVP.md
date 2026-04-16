# RSS Feed Reader MVP

A simple proof-of-concept RSS/Atom feed reader application for managing feed subscriptions. This MVP demonstrates basic subscription management with an in-memory storage system.

## Overview

The RSS Feed Reader MVP is a web application built with:

- **Backend**: ASP.NET Core Web API with in-memory subscription storage
- **Frontend**: Blazor WebAssembly with interactive UI components
- **Storage**: Session-based in-memory storage (subscriptions cleared on app restart)

### MVP Features

- ✅ **Add Feed Subscriptions**: Users can add new RSS/Atom feed subscriptions by entering a feed URL
- ✅ **View Subscriptions**: Display a list of all added subscriptions in the UI
- ✅ **In-Memory Storage**: Subscriptions persist for the current session

### Out of Scope for MVP

- Feed fetching and parsing
- RSS/Atom feed content display
- Feed item management
- Persistent storage (database)
- User authentication
- Feed validation or error handling
- URL validation

## Architecture

### Project Structure

```
src/
├── RssReaderApi/              # ASP.NET Core Web API backend
│   ├── Models/
│   │   └── Subscription.cs     # Feed subscription data model
│   ├── Services/
│   │   └── SubscriptionService.cs  # In-memory subscription storage
│   ├── Controllers/
│   │   └── SubscriptionsController.cs  # REST API endpoints
│   ├── Program.cs             # Application configuration
│   └── RssReaderApi.csproj
│
└── RssReaderClient/           # Blazor WebAssembly frontend
    ├── Models/
    │   └── Subscription.cs     # Subscription model (client-side)
    ├── Components/
    │   ├── SubscriptionForm.razor   # Add subscription form
    │   └── SubscriptionList.razor   # Display subscriptions
    ├── Pages/
    │   └── Index.razor         # Main application page
    ├── Program.cs              # WASM app initialization
    └── RssReaderClient.csproj
```

### API Endpoints

#### Get All Subscriptions

```http
GET /api/subscriptions
```

**Response**: `200 OK`

```json
[
  { "id": "1", "url": "https://example.com/feed.rss" },
  { "id": "2", "url": "https://blog.example.org/feed.atom" }
]
```

#### Add a Subscription

```http
POST /api/subscriptions
Content-Type: application/json

{ "url": "https://example.com/feed.rss" }
```

**Response**: `201 Created`

```json
{ "id": "3", "url": "https://example.com/feed.rss" }
```

## Getting Started

### Prerequisites

- .NET SDK 8.0 or later
- A terminal or command prompt

### Building the Application

1. **Clone the repository** (if applicable)

   ```bash
   cd skills-testes
   ```

2. **Build the solution**

   ```bash
   cd src
   dotnet build
   ```

   This will:
   - Compile the ASP.NET Core API backend
   - Compile the Blazor WebAssembly frontend and generate static assets

### Running the Application

#### Option 1: Run Both Services from IDE/Terminal

1. **Start the Backend API** (in Terminal 1)

   ```bash
   cd src/RssReaderApi
   dotnet run
   ```

   The API will start on `https://localhost:7215` (HTTPS) or `http://localhost:5215` (HTTP)

2. **Start the Frontend** (in Terminal 2)

   ```bash
   cd src/RssReaderClient
   dotnet run
   ```

   The frontend will start on `https://localhost:7296` (HTTPS) or `http://localhost:5296` (HTTP)

3. **Open in Browser**
   Navigate to `https://localhost:7296` or `http://localhost:5296` (depending on which port your WASM app is running on)

#### Option 2: Run from Solution

```bash
cd src
dotnet run --project RssReaderApi/RssReaderApi.csproj &
dotnet run --project RssReaderClient/RssReaderClient.csproj
```

## Using the Application

### Adding a Subscription

1. Open the application in your web browser
2. Enter a feed URL in the text input field (e.g., `https://devblogs.microsoft.com/dotnet/feed/`)
3. Click the "Add Subscription" button
4. The subscription will be added to your list

### Viewing Subscriptions

- The "My Subscriptions" section displays all added subscriptions
- Each subscription shows its feed URL
- A count of total subscriptions is displayed at the bottom

### Testing In-Memory Storage

1. Add a few subscriptions
2. Verify they appear in the list
3. Refresh the browser page (F5 or Cmd+R)
4. Notice that subscriptions are cleared (demonstrating in-memory storage)

This is expected behavior for the MVP - subscriptions are only stored for the current session.

## In-Memory Storage Behavior

The MVP uses in-memory storage for subscriptions. This means:

- **Subscriptions persist**: During the current application session, all subscriptions remain in memory
- **Subscriptions are shared**: All users/sessions share the same in-memory storage
- **Subscriptions are lost**: When the backend API stops, all subscriptions are cleared
- **Browser refresh does NOT clear subscriptions** from the backend (the backend still has them), but the "session" in this context refers to the backend process, not the browser session

### Why In-Memory Storage for MVP?

- **Simplicity**: Avoids database setup and complexity
- **Rapid Development**: Focus on core functionality without persistence overhead
- **MVP Requirements**: Sufficient for demonstrating subscription management capability
- **Extended MVP**: Future versions can add database persistence

## Integration Testing

### Manual Test Scenario 1: Add Multiple Subscriptions

1. Start the backend and frontend
2. Add 3-4 subscriptions with different URLs
3. Verify all subscriptions appear in the list
4. Verify the subscription count updates

**Expected Result**: All subscriptions appear in the UI ✅

### Manual Test Scenario 2: Verify In-Memory Persistence

1. Add several subscriptions
2. Get a subscription list count
3. Stop the backend API (`Ctrl+C`)
4. Restart the backend API
5. Refresh the browser
6. Check the subscription list

**Expected Result**: Subscriptions are gone (cleared after restart) ✅

### Manual Test Scenario 3: Form Feedback

1. Try to submit an empty URL (clear the input field on initial load since it may have placeholder text then submit")
2. Verify an error message appears
3. Enter a valid URL and submit
4. Verify the form clears and a success message appears

**Expected Result**: Form shows appropriate feedback ✅

## Development Notes

### Key Components

- **SubscriptionService**: Thread-safe in-memory storage with lock mechanism
- **SubscriptionsController**: REST API controller with error handling
- **SubscriptionForm**: Blazor component with form validation and feedback
- **SubscriptionList**: Blazor component with dynamic list rendering and empty state
- **Index.razor**: Parent component orchestrating form and list refresh

### Code Structure

- Models follow POCO (Plain Old CLR Object) pattern
- Services use dependency injection
- Components use parameter passing and EventCallbacks for communication
- CORS is configured to allow the frontend to communicate with the backend

## Troubleshooting

### "Connection refused" Error

If you see connection errors:

1. Verify the backend API is running: `https://localhost:7215`
2. Verify the frontend is running: `https://localhost:7296`
3. Check that the base address in `RssReaderClient/Program.cs` matches your backend URL

### Build Errors

If the build fails:

1. Ensure .NET SDK 8.0 or later is installed: `dotnet --version`
2. Restore NuGet packages: `dotnet restore`
3. Clean and rebuild: `dotnet clean && dotnet build`

### CORS Errors

If you see CORS errors in the browser console:

1. Ensure the frontend URL is added to the CORS policy in `RssReaderApi/Program.cs`
2. The CORS policy should allow the Blazor frontend's origin

## Next Steps / Extended MVP

After the MVP is working, future enhancements could include:

1. **Persistent Storage**: Replace in-memory storage with database (Entity Framework Core)
2. **Feed Fetching**: Implement actual RSS/Atom feed retrieval
3. **Content Display**: Show feed items and articles
4. **Feed Refresh**: Manual and automatic feed update capabilities
5. **Error Handling**: Proper validation and error messages
6. **User Interface**: Enhanced styling and UX polish
7. **Testing**: Automated unit and integration tests

## References

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Blazor WebAssembly Documentation](https://docs.microsoft.com/aspnet/core/blazor)
- [RSS Specification](https://tools.ietf.org/html/rfc4287)
- [Atom Specification](https://www.w3.org/2005/Atom)

## License

See LICENSE file for details.
