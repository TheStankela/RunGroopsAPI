using RunGroops.Domain.Enum;

namespace RunGroops.Domain.EFModels
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
