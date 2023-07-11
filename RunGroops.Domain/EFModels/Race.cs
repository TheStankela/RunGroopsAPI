using RunGroops.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RunGroops.Domain.EFModels
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RaceCategory RaceCategory { get; set; }
    }
}
