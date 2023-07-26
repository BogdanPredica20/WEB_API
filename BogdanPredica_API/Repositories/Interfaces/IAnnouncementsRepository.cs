using BogdanPredica_API.Models;

namespace BogdanPredica_API.Repositories.Interfaces
{
    public interface IAnnouncementsRepository
    {
        public Task<IEnumerable<Announcement>> GetAnnouncementsAsync();
        Task<Announcement> GetAnnouncementByIdAsync(Guid id);
        Task CreateAnnouncementAsync(Announcement announcement);
    }
}
