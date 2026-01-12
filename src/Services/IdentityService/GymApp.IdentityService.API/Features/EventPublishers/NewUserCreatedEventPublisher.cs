using MassTransit;
using GymApp.Shared.MessageQueues.Events;

namespace GymApp.IdentityService.API.Features.EventPublishers;

public class NewUserCreatedEventPublisher(ILogger<NewUserCreatedEventPublisher> logger, IPublishEndpoint publishEndpoint)
{
    public async Task PublishNewUserCreated(string userId, string username)
    {
        var @event = new NewUserCreatedEvent
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Username = username
        };

        logger.LogCritical("NewUserCreatedEvent was published");

        await publishEndpoint.Publish(@event);
    }
}