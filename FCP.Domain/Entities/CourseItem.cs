using FCP.Domain.Enums;

namespace FCP.Domain.Entities;

public class CourseItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public string Title { get; set; } = "";
    public string? ContentUrl { get; set; }

    public CourseItemType Type { get; set; } = CourseItemType.Video;

    public int Order { get; set; }
    public int? DurationMinutes { get; set; }
}
