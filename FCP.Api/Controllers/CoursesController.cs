using FCP.Api.Contracts.Courses;
using FCP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FCP.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public CoursesController(ApplicationDbContext db) => _db = db;

    // GET api/courses
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var courses = await _db.Courses
            .AsNoTracking()
            .Include(c => c.Provider)
            .Select(c => new CourseResponse(
                c.Id,
                c.Title,
                c.Description,
                c.ProviderId,
                c.Provider!.Name
            ))
            .ToListAsync(ct);

        return Ok(courses);
    }

    // GET api/courses/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var course = await _db.Courses
            .AsNoTracking()
            .Include(c => c.Provider)
            .Where(c => c.Id == id)
            .Select(c => new CourseResponse(
                c.Id,
                c.Title,
                c.Description,
                c.ProviderId,
                c.Provider!.Name
            ))
            .FirstOrDefaultAsync(ct);

        return course is null ? NotFound() : Ok(course);
    }

    // POST api/courses
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseRequest request, CancellationToken ct)
    {
        var provider = await _db.Providers
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.ProviderId, ct);

        if (provider is null)
            return BadRequest($"ProviderId={request.ProviderId} does not exist.");

        var course = new FCP.Domain.Entities.Course
        {
            Title = request.Title,
            Description = request.Description ?? "",
            ProviderId = request.ProviderId,
            Level = request.Level
        };

        _db.Courses.Add(course);
        await _db.SaveChangesAsync(ct);

        var response = new CourseResponse(
            course.Id,
            course.Title,
            course.Description,
            course.ProviderId,
            provider.Name
        );

        return CreatedAtAction(nameof(GetById), new { id = course.Id }, response);
    }

    // PUT api/courses/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseRequest request, CancellationToken ct)
    {
        var course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (course is null) return NotFound();

        var providerExists = await _db.Providers.AnyAsync(p => p.Id == request.ProviderId, ct);
        if (!providerExists)
            return BadRequest($"ProviderId={request.ProviderId} does not exist.");

        course.Title = request.Title;
        course.Description = request.Description ?? "";
        course.ProviderId = request.ProviderId;
        course.Level = request.Level;
        course.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    // DELETE api/courses/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (course is null) return NotFound();

        _db.Courses.Remove(course);
        await _db.SaveChangesAsync(ct);

        return NoContent();
    }
}
