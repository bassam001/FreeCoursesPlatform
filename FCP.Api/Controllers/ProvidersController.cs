using FCP.Api.Contracts.Providers;
using FCP.Domain.Entities;
using FCP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FCP.Api.Controllers;

[ApiController]
[Route("api/providers")]
public class ProvidersController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ProvidersController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await _db.Providers.AsNoTracking().ToListAsync(ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateProviderRequest request,
        CancellationToken ct)
    {
        var provider = new Provider
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        _db.Providers.Add(provider);
        await _db.SaveChangesAsync(ct);

        return Ok(provider);
    }
}
