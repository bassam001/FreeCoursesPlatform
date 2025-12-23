namespace FCP.Domain.Entities;

public class CourseItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public string Title { get; set; } = "";
    public string? ContentUrl { get; set; }
    public string Type { get; set; } = "Video"; // Video/Text/Quiz
    public int Order { get; set; }
    public int? DurationMinutes { get; set; }
}