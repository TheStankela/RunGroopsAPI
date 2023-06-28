using Microsoft.EntityFrameworkCore;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using RunGroops.Infrastructure.Context;

namespace RunGroops.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAddress(Address address)
        {
            await _context.AddAsync(address);
            return await Save();
        }

        public async Task<bool> AddressExists(int addressId)
        {
            var result = await _context.Addresses.AnyAsync(a => a.Id == addressId);
            return result;
        }

        public async Task<bool> DeleteAddress(Address address)
        {
            _context.Remove(address);
            return await Save();
        }

        public async Task<Address?> GetAddressById(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> UpdateAddress(Address address)
        {
            _context.Update(address);
            return await Save();
        }
        private async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
