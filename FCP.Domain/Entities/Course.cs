namespace FCP.Domain.Entities;

public class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Level { get; set; } = "Beginner";
    public Guid ProviderId { get; set; }
    public Provider? Provider { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public List<CourseItem> Items { get; set; } = new();
}
