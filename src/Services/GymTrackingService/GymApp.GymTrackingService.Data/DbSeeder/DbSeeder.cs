using GymApp.GymTrackingService.Data.Context;
using GymApp.GymTrackingService.Data.Entities;

namespace GymApp.GymTrackingService.Data.DbSeeder;

public static class DbSeeder
{
    public static async Task SeedAsync(GymTrackingContext context)
    {
        context.Database.EnsureCreated();

        if (context.Exercises.Any() || context.MuscleGroups.Any() || context.Workouts.Any()) return;

        var muscleGroups = new List<MuscleGroup>
        {
            new() { Name = "Chest" },
            new() { Name = "Back" },
            new() { Name = "Legs" },
            new() { Name = "Arms" },
            new() { Name = "Shoulders" }
        };

        var exercises = new List<Exercise>
        {
            new() { Name = "Bench Press" },
            new() { Name = "Deadlift" },
            new() { Name = "Squat" },
            new() { Name = "Bicep Curl" },
            new() { Name = "Shoulder Press" }
        };

        var workouts = new List<Workout>
        {
            new() { Date = DateTime.Now.AddDays(-7).ToUniversalTime() },
            new() { Date = DateTime.Now.AddDays(-3).ToUniversalTime() },
            new() { Date = DateTime.Now.ToUniversalTime() }
        };

        context.MuscleGroups.AddRange(muscleGroups);
        context.Exercises.AddRange(exercises);
        context.Workouts.AddRange(workouts);

        await context.SaveChangesAsync();
    }
}