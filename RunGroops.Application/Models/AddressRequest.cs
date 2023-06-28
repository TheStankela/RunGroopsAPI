namespace RunGroops.Application.Models
{
    public record AddressRequest(
        string Country,
        string City,
        string Street,
        int Zip);
}
