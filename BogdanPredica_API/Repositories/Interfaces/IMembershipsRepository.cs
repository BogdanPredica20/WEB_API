using BogdanPredica_API.Models;

namespace BogdanPredica_API.Repositories.Interfaces
{
    public interface IMembershipsRepository
    {
        public Task<IEnumerable<Membership>> GetMembershipsAsync();
        Task<Membership> GetMembershipByIdAsync(Guid id);
        Task CreateMembershipAsync(Membership membership);
        Task<Membership> UpdateMembershipAsync(Guid id, Membership membership);
        Task<bool> DeleteMembershipAsync(Guid id);
    }
}
