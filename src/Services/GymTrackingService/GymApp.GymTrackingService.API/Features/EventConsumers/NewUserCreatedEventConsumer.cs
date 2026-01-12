using GymApp.GymTrackingService.Data.Context;
using GymApp.Shared.MessageQueues.Events;
using MassTransit;
using GymApp.GymTrackingService.Data.Entities;

namespace GymApp.GymTrackingService.API.Features.EventConsumers;

public class NewUserCreatedEventConsumer(ILogger<NewUserCreatedEventConsumer> logger, GymTrackingContext gymTrackingContext) : IConsumer<NewUserCreatedEvent>
{
    public async Task Consume(ConsumeContext<NewUserCreatedEvent> context)
    {
        var @event = context.Message;

        logger.LogInformation(
            "NewUserCreated event consumed: User Id: {UserId}, Username: {Username}, Created At: {CreatedAt}",
            @event.UserId,
            @event.Username,
            @event.CreatedAt
        );

        var newStudent = new Student
        {
            UserId = @event.UserId!,
            Username = @event.Username!,
            EnrollmentDate = @event.CreatedAt
        };

        logger.LogCritical("Adding new student to GymTrackingContext: {@NewStudent}", newStudent);

        gymTrackingContext.Students.Add(newStudent);
        await gymTrackingContext.SaveChangesAsync();

        logger.LogCritical("New student added to GymTrackingContext with User Id: {UserId}", newStudent.UserId);

        await Task.CompletedTask;
    }
}