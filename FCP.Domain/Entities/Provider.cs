namespace FCP.Domain.Entities;

public class Provider
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string? Website { get; set; }
    public string? LogoUrl { get; set; }
    public string? ContactEmail { get; set; }

    public List<Course> Courses { get; set; } = new();
}