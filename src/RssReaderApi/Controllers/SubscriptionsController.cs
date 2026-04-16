using Microsoft.AspNetCore.Mvc;
using RssReaderApi.Models;
using RssReaderApi.Services;

namespace RssReaderApi.Controllers;

/// <summary>
/// API endpoints for managing RSS feed subscriptions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly SubscriptionService _subscriptionService;

    public SubscriptionsController(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    /// <summary>
    /// Gets all current subscriptions.
    /// </summary>
    /// <returns>List of subscriptions.</returns>
    [HttpGet]
    public ActionResult<List<Subscription>> GetAll()
    {
        var subscriptions = _subscriptionService.GetAll();
        return Ok(subscriptions);
    }

    /// <summary>
    /// Adds a new subscription with the given feed URL.
    /// </summary>
    /// <param name="request">Request containing the feed URL.</param>
    /// <returns>The newly created subscription.</returns>
    [HttpPost]
    public ActionResult<Subscription> AddSubscription([FromBody] AddSubscriptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Url))
        {
            return BadRequest(new { error = "URL is required" });
        }

        try
        {
            var subscription = _subscriptionService.AddSubscription(request.Url);
            return CreatedAtAction(nameof(GetAll), new { id = subscription.Id }, subscription);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

/// <summary>
/// Request model for adding a subscription.
/// </summary>
public class AddSubscriptionRequest
{
    /// <summary>
    /// URL of the RSS/Atom feed.
    /// </summary>
    public string Url { get; set; } = string.Empty;
}
