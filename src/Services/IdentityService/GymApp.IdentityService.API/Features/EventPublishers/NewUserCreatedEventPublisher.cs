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
            CreatedAt = DateTime.Now,
            Username = username
        };

        logger.LogCritical("NewUserCreatedEvent was published {time}", DateTime.Now);

        await publishEndpoint.Publish(@event);
    }
}