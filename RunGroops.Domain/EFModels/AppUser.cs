using Microsoft.AspNetCore.Identity;
using RunGroops.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RunGroops.Domain.EFModels
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            SentFriendRequests = new List<Friend>();
            ReceievedFriendRequests = new List<Friend>();
        }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserCategory UserCategory { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
        public virtual ICollection<Friend> SentFriendRequests { get; set; }

        public virtual ICollection<Friend> ReceievedFriendRequests { get; set; }

        [NotMapped]
        public virtual ICollection<Friend> Friends
        {
            get
            {
                var friends = SentFriendRequests.Where(x => x.Approved).ToList();
                friends.AddRange(ReceievedFriendRequests.Where(x => x.Approved));
                return friends;
            }
        }
    }
}
