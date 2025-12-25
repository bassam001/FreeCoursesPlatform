namespace FCP.Api.Contracts.Courses;

public record UpdateCourseRequest(
    string Title,
    string? Description,
    int ProviderId
);
