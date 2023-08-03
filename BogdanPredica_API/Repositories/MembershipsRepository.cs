using BogdanPredica_API.DataContext;
using BogdanPredica_API.Exceptions;
using BogdanPredica_API.Helpers.Enums;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.Repositories
{
    public class MembershipsRepository : IMembershipsRepository
    {
        private readonly ClubLibraDataContext _context;

        public MembershipsRepository(ClubLibraDataContext context)
        {
            _context = context;
        }

        public async Task<Membership> GetMembershipByIdAsync(Guid id)
        {
            return await _context.Memberships.SingleOrDefaultAsync(a => a.IdMembership == id);
        }

        public async Task<IEnumerable<Membership>> GetMembershipsAsync()
        {
            return await _context.Memberships.ToListAsync();
        }

        public async Task CreateMembershipAsync(Membership membership)
        {
            membership.IdMembership = Guid.NewGuid();

            ValidationFunctions.ThrowExceptionWhenDateIsNotValid(membership.StartDate, membership.EndDate);

            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMembershipAsync(Guid id)
        {
            if (!await ExistMembershipAsync(id))
            {
                return false;
            }

            _context.Memberships.Remove(new Membership { IdMembership = id });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Membership> UpdateMembershipAsync(Guid id, Membership membership)
        {
            if (!await ExistMembershipAsync(id))
            {
                return null;
            }

            if (membership != null)
            {
                ValidationFunctions.ThrowExceptionWhenDateIsNotValid(membership.StartDate, membership.EndDate);
                _context.Memberships.Update(membership);
                await _context.SaveChangesAsync();
            }

            return membership;
        }

        private async Task<bool> ExistMembershipAsync(Guid id)
        {
            return await _context.Memberships.CountAsync(a => a.IdMembership == id) > 0;
        }
    }
}
