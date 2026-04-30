using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crm_backend.Data;
using crm_backend.Models;
using crm_backend.Dtos;
using crm_backend.Enums;

namespace crm_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActivitiesController(AppDbContext context)
    {
        _context = context;
    }

    // 
    // CREATE =========================
[HttpPost]
public async Task<ActionResult<ActivityReadDto>> CreateActivity(ActivityCreateDto dto)
{
    var activity = new Activity
    {
        CustomerId = dto.CustomerId,
        Type = dto.Type,
        Description = dto.Description,
        CreatedAt = DateTime.UtcNow
    };

    _context.Activities.Add(activity);
    await _context.SaveChangesAsync();

        var result = await _context.Activities
            .Where(a => a.Id == activity.Id)
            .Select(a => new ActivityReadDto
            {
                Id = a.Id,
                CustomerId = a.CustomerId,
                CustomerName = a.Customer.Name,
                Type = a.Type,
                Description = a.Description,
                CreatedAt = a.CreatedAt
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, result);
}
    // 
    // READ ALL =========================
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityReadDto>>> GetActivities()
    {
        var activities = await _context.Activities
            .Select(a => new ActivityReadDto
            {
                Id = a.Id,
                CustomerId = a.CustomerId,
                CustomerName = a.Customer.Name,
                Type = a.Type,
                Description = a.Description,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(activities);
    }

    // 
    // READ BY ID =========================

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityReadDto>> GetActivity(int id)
    {
        var activity = await _context.Activities
            .Where(a => a.Id == id)
            .Select(a => new ActivityReadDto
            {
                Id = a.Id,
                CustomerId = a.CustomerId,
                CustomerName = a.Customer.Name,
                Type = a.Type,
                Description = a.Description,
                CreatedAt = a.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (activity == null)
            return NotFound();

        return Ok(activity);
    }

    // 
    // UPDATE (partial update style) =========================
 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(int id, ActivityUpdateDto dto)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
            return NotFound();

        activity.Type = dto.Type;
        activity.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // 
    // DELETE =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
            return NotFound();

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}