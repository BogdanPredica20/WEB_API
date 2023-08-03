using BogdanPredica_API.Models;

namespace BogdanPredica_API.Repositories.Interfaces
{
    public interface IMembershipTypesRepository
    {
        public Task<IEnumerable<MembershipType>> GetMembershipTypesAsync();
        Task<MembershipType> GetMembershipTypeByIdAsync(Guid id);
        Task CreateMembershipTypeAsync(MembershipType membershipType);
        Task<MembershipType> UpdateMembershipTypeAsync(Guid id, MembershipType membershipType);
        Task<bool> DeleteMembershipTypeAsync(Guid id);
    }
}
