using GymApp.GymTrackingService.Data.Entities;
using GymApp.GymTrackingService.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymApp.GymTrackingService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MuscleGroupController(GymTrackingContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MuscleGroup>>> GetMuscleGroups()
    {
        return Ok(await context.MuscleGroups.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MuscleGroup>> GetMuscleGroupById(Guid id)
    {
        var muscleGroup = await context.MuscleGroups.FindAsync(id);
        if (muscleGroup == null) return NotFound();

        return Ok(muscleGroup);
    }

    [HttpPost]
    public async Task<ActionResult<MuscleGroup>> CreateMuscleGroup(MuscleGroup muscleGroup)
    {
        context.MuscleGroups.Add(muscleGroup);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMuscleGroupById), new { id = muscleGroup.Id }, muscleGroup);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMuscleGroup(Guid id, MuscleGroup muscleGroup)
    {
        if (id != muscleGroup.Id) return BadRequest();

        context.Entry(muscleGroup).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.MuscleGroups.Any(mg => mg.Id == id))
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
    public async Task<IActionResult> DeleteMuscleGroup(Guid id)
    {
        var muscleGroup = await context.MuscleGroups.FindAsync(id);
        if (muscleGroup == null) return NotFound();

        context.MuscleGroups.Remove(muscleGroup);
        await context.SaveChangesAsync();

        return NoContent();
    }
}