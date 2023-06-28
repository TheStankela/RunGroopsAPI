using RunGroops.Domain.EFModels;

namespace RunGroops.Domain.Interfaces
{
    public interface IAddressRepository
    {
        public Task<bool> AddressExists(int addressId);
        public Task<bool> AddAddress(Address address);
        public Task<bool> DeleteAddress(Address address);
        public Task<bool> UpdateAddress(Address address);
        public Task<Address?> GetAddressById(int id);
    }
}
