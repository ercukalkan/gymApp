using GymApp.GymTrackingService.Data.Entities;
using GymApp.GymTrackingService.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymApp.GymTrackingService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExerciseController(GymTrackingContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises()
    {
        return Ok(await context.Exercises.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Exercise>> GetExerciseById(Guid id)
    {
        var exercise = await context.Exercises.FindAsync(id);
        if (exercise == null) return NotFound();

        return Ok(exercise);
    }

    [HttpPost]
    public async Task<ActionResult<Exercise>> CreateExercise(Exercise exercise)
    {
        context.Exercises.Add(exercise);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.Id }, exercise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExercise(Guid id, Exercise exercise)
    {
        if (id != exercise.Id) return BadRequest();

        context.Entry(exercise).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Exercises.Any(e => e.Id == id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExercise(Guid id)
    {
        var exercise = await context.Exercises.FindAsync(id);
        if (exercise == null) return NotFound();

        context.Exercises.Remove(exercise);
        await context.SaveChangesAsync();

        return NoContent();
    }
}