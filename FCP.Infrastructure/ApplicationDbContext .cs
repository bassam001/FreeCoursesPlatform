using FCP.Application.Common.Interfaces;
using FCP.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FCP.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseItem> CourseItems => Set<CourseItem>();
    public DbSet<Provider> Providers => Set<Provider>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Course>()
            .HasOne(c => c.Provider)
            .WithMany(p => p.Courses)
            .HasForeignKey(c => c.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CourseItem>()
            .HasOne(i => i.Course)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CourseId);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}
