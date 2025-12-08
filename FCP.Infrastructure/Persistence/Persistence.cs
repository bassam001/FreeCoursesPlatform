using FCP.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FCP.Infrastructure.Persistence;

public class Persistence : IdentityDbContext
{
    public Persistence(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseItem> CourseItems => Set<CourseItem>();
    public DbSet<Provider> Providers => Set<Provider>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Course>()
            .HasOne(c => c.Provider)
            .WithMany(p => p.Courses)
            .HasForeignKey(c => c.ProviderId);

        builder.Entity<CourseItem>()
            .HasOne(ci => ci.Course)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CourseId);
    }
}
