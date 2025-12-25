namespace FCP.Api.Contracts.Courses;

public record CreateCourseRequest(
    string Title,
    string? Description,
    Guid ProviderId
);
