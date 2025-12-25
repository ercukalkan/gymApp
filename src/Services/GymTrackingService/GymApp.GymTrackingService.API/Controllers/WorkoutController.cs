using GymApp.GymTrackingService.Data.Entities;
using GymApp.GymTrackingService.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymApp.GymTrackingService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutController(GymTrackingContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts()
    {
        return Ok(await context.Workouts.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Workout>> GetWorkoutById(Guid id)
    {
        var workout = await context.Workouts.FindAsync(id);
        if (workout == null) return NotFound();

        return Ok(workout);
    }

    [HttpPost]
    public async Task<ActionResult<Workout>> CreateWorkout(Workout workout)
    {
        context.Workouts.Add(workout);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWorkoutById), new { id = workout.Id }, workout);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkout(Guid id, Workout workout)
    {
        if (id != workout.Id) return BadRequest();

        context.Entry(workout).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Workouts.Any(w => w.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
}