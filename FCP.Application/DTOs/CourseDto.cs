namespace FCP.Application.Courses.DTOs;

public record CourseDto(Guid Id, string Title, string Description, Guid ProviderId, bool IsPublished);
