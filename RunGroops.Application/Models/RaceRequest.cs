using RunGroops.Domain.Enum;

namespace RunGroops.Application.Models
{
    public record RaceRequest(
        string Name,
        string Description,
        AddressRequest Address,
        RaceCategory RaceCategory);
}
