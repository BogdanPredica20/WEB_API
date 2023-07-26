using BogdanPredica_API.DataContext;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.Repositories
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        private readonly ClubLibraDataContext _context;

        public AnnouncementsRepository(ClubLibraDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(Guid id)
        {
            return await _context.Announcements.SingleOrDefaultAsync(a => a.IdAnnouncement == id);
        }

        public async Task CreateAnnouncementAsync(Announcement announcement)
        {
            announcement.IdAnnouncement = Guid.NewGuid();

            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
        }
    }
}
