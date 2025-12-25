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
                c.Provider.Name
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
                c.Provider.Name
            ))
            .FirstOrDefaultAsync(ct);

        return course is null ? NotFound() : Ok(course);
    }

    // POST: api/courses
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseRequest request, CancellationToken ct)
    {
        var providerExists = await _db.Providers.AnyAsync(p => p.Id == request.ProviderId, ct);
        if (!providerExists)
            return BadRequest($"ProviderId={request.ProviderId} does not exist.");

        var course = new FCP.Domain.Entities.Course
        {
            Title = request.Title,
            Description = request.Description,
            ProviderId = request.ProviderId
        };

        _db.Courses.Add(course);
        await _db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    // PUT: api/courses/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseRequest request, CancellationToken ct)
    {
        var course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (course is null) return NotFound();

        var providerExists = await _db.Providers.AnyAsync(p => p.Id == request.ProviderId, ct);
        if (!providerExists)
            return BadRequest($"ProviderId={request.ProviderId} does not exist.");

        course.Title = request.Title;
        course.Description = request.Description;
        course.ProviderId = request.ProviderId;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    // DELETE: api/courses/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (course is null) return NotFound();

        _db.Courses.Remove(course);
        await _db.SaveChangesAsync(ct);

        return NoContent();
    }
}
