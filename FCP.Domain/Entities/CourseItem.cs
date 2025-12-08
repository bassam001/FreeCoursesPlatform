namespace FCP.Domain.Entities;

public class CourseItem
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string ExternalUrl { get; set; } = default!;
    public int OrderIndex { get; set; }
}
