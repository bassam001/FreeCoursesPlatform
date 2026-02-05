using FCP.Domain.Enums;

namespace FCP.Api.Contracts.Courses;

public record CreateCourseRequest(
    string Title,
    string? Description,
    Guid ProviderId,
    CourseLevel Level
);
