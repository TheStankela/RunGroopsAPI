using Newtonsoft.Json.Converters;
using RunGroops.Domain.Enum;
using System.Text.Json.Serialization;

namespace RunGroops.Domain.EFModels
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int AddressId { get; set; }
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Address Address { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ClubCategory ClubCategory { get; set; }
    }
}
