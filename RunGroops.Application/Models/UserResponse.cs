namespace RunGroops.Application.Models
{
    public record UserResponse (
        string? Id,
        string? UserName,
        int? Mileage,
        int? Pace,
        string? ImageURL);
}
