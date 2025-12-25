namespace FCP.Api.Contracts.Courses;

public record CourseResponse(
    Guid Id,
    string Title,
    string? Description,
    Guid ProviderId,
    string ProviderName
);
