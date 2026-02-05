using FCP.Domain.Enums;

namespace FCP.Api.Contracts.Courses;

public record UpdateCourseRequest(
    string Title,
    string? Description,
    Guid ProviderId,
    CourseLevel Level
);
