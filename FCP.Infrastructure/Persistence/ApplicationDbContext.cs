using FCP.Application.Common.Interfaces;
using FCP.Domain.Entities;
using FCP.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FCP.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseItem> CourseItems => Set<CourseItem>();
    public DbSet<Provider> Providers => Set<Provider>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var levelConverter = new EnumToStringConverter<CourseLevel>();
        var itemTypeConverter = new EnumToStringConverter<CourseItemType>();

        builder.Entity<Course>()
         .Property(c => c.Level)
         .HasConversion(levelConverter)
         .HasColumnType("nvarchar(max)");

        builder.Entity<CourseItem>()
            .Property(i => i.Type)
            .HasConversion(itemTypeConverter)
            .HasColumnType("nvarchar(max)");

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
}
