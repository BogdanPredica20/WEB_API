using BogdanPredica_API.DataContext;
using BogdanPredica_API.Exceptions;
using BogdanPredica_API.Helpers.Enums;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.Repositories
{
    public class MembershipTypesRepository : IMembershipTypesRepository
    {

        private readonly ClubLibraDataContext _context;

        public MembershipTypesRepository(ClubLibraDataContext context)
        {
            _context = context;
        }

        public async Task<MembershipType> GetMembershipTypeByIdAsync(Guid id)
        {
            return await _context.MembershipTypes.SingleOrDefaultAsync(a => a.IdMembershipType == id);
        }

        public async Task<IEnumerable<MembershipType>> GetMembershipTypesAsync()
        {
            return await _context.MembershipTypes.ToListAsync();
        }

        public async Task CreateMembershipTypeAsync(MembershipType membershipType)
        {
            membershipType.IdMembershipType = Guid.NewGuid();

            _context.MembershipTypes.Add(membershipType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMembershipTypeAsync(Guid id)
        {
            if (!await ExistMembershipTypeAsync(id))
            {
                return false;
            }

            _context.MembershipTypes.Remove(new MembershipType { IdMembershipType = id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MembershipType> UpdateMembershipTypeAsync(Guid id, MembershipType membershipType)
        {
            if (!await ExistMembershipTypeAsync(id))
            {
                return null;
            }

            if (membershipType != null)
            {
                _context.MembershipTypes.Update(membershipType);
                await _context.SaveChangesAsync();
            }

            return membershipType;
        }

        private async Task<bool> ExistMembershipTypeAsync(Guid id)
        {
            return await _context.MembershipTypes.CountAsync(a => a.IdMembershipType == id) > 0;
        }
    }
}
