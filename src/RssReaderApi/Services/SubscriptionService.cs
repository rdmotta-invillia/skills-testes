using RssReaderApi.Models;

namespace RssReaderApi.Services;

/// <summary>
/// Service for managing RSS feed subscriptions in memory.
/// Subscriptions are stored for the duration of the application session.
/// </summary>
public class SubscriptionService
{
    private readonly List<Subscription> _subscriptions = new();
    private readonly object _lock = new();

    /// <summary>
    /// Gets all current subscriptions.
    /// </summary>
    /// <returns>List of subscriptions.</returns>
    public List<Subscription> GetAll()
    {
        lock (_lock)
        {
            return new List<Subscription>(_subscriptions);
        }
    }

    /// <summary>
    /// Adds a new subscription with the given feed URL.
    /// </summary>
    /// <param name="url">The URL of the RSS/Atom feed.</param>
    /// <returns>The newly created subscription.</returns>
    public Subscription AddSubscription(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be empty", nameof(url));
        }

        var subscription = new Subscription { Url = url.Trim() };

        lock (_lock)
        {
            _subscriptions.Add(subscription);
        }

        return subscription;
    }
}
