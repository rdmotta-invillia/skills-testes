namespace RssReaderApi.Models;

/// <summary>
/// Represents an RSS/Atom feed subscription.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Unique identifier for the subscription.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// URL of the RSS/Atom feed.
    /// </summary>
    public string Url { get; set; } = string.Empty;
}
