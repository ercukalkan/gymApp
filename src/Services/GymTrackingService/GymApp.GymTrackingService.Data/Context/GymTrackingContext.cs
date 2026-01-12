using GymApp.GymTrackingService.Data.Entities;
using GymApp.GymTrackingService.Data.Entities.JunctionEntities;
using Microsoft.EntityFrameworkCore;

namespace GymApp.GymTrackingService.Data.Context;

public class GymTrackingContext(DbContextOptions<GymTrackingContext> options) : DbContext(options)
{
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<MuscleGroup> MuscleGroups { get; set; } = null!;
    public DbSet<Workout> Workouts { get; set; } = null!;

    public DbSet<WorkoutExercise> WorkoutExercises { get; set; } = null!;
    public DbSet<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; } = null!;

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Trainer> Trainers { get; set; } = null!;
}