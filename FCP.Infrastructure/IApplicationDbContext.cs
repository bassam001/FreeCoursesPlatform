using FCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCP.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<CourseItem> CourseItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
