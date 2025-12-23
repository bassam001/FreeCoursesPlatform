using Microsoft.EntityFrameworkCore;
using FCP.Domain.Entities;


namespace FCP.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; }
        DbSet<CourseItem> CourseItems { get; }
        DbSet<Provider> Providers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
