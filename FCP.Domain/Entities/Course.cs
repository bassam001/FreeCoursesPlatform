namespace FCP.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? ShortDescription { get; set; }
    public string? Language { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string Source { get; set; } = "YouTube";
    public Guid ProviderId { get; set; }
    public Provider? Provider { get; set; }

    public List<CourseItem> Items { get; set; } = new();
}
