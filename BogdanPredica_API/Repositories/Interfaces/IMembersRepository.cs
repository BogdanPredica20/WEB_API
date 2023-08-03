using BogdanPredica_API.Models;

namespace BogdanPredica_API.Repositories.Interfaces
{
    public interface IMembersRepository
    {
        public Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member> GetMemberByIdAsync(Guid id);
        Task CreateMemberAsync(Member member);
        Task<Member> UpdateMemberAsync(Guid id, Member member);
        Task<Member> UpdatePartiallyMemberAsync(Guid id, Member member);
        Task<bool> DeleteMemberAsync(Guid id);
    }
}
