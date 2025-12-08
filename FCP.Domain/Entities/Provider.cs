namespace FCP.Domain.Entities;

public class Provider
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? LogoUrl { get; set; }
    public string? WebsiteUrl { get; set; }

    public List<Course> Courses { get; set; } = new();
}
