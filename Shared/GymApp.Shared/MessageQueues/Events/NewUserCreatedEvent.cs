namespace GymApp.Shared.MessageQueues.Events;

public record NewUserCreatedEvent
{
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public DateTime CreatedAt { get; set; }
}