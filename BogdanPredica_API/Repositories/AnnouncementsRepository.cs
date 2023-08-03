using BogdanPredica_API.DataContext;
using BogdanPredica_API.Exceptions;
using BogdanPredica_API.Helpers.Enums;
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

            bool title = await TitleExists(announcement.Title);
            if(title)
            {
                throw new ModelValidationException(ErrorMessagesEnum.Announcement.TitleExistsError);
            }

            ValidationFunctions.ThrowExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);

            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task<Announcement> UpdateAnnouncementAsync(Guid id, Announcement announcement)
        {
            if(!await ExistAnnouncementAsync(id))
            {
                return null;
            }

            if (announcement != null)
            {
                ValidationFunctions.ThrowExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);
                _context.Announcements.Update(announcement);
                await _context.SaveChangesAsync();
            }

            return announcement;
        }

        public async Task<Announcement> UpdatePartiallyAnnouncementAsync(Guid id, Announcement announcement)
        {
            var announcementFromDatabase = await GetAnnouncementByIdAsync(id);
            bool announcementIsChanged = false;

            if(announcementFromDatabase == null)
            {
                return null;
            }

            if(!string.IsNullOrEmpty(announcement.Tags) && announcementFromDatabase.Tags != announcement.Tags)
            {
                announcementFromDatabase.Tags = announcement.Tags;
                announcementIsChanged = true;
            }

            if (!string.IsNullOrEmpty(announcement.Text) && announcementFromDatabase.Text != announcement.Text)
            {
                announcementFromDatabase.Text = announcement.Text;
                announcementIsChanged = true;
            }

            if (!string.IsNullOrEmpty(announcement.Title) && announcementFromDatabase.Title != announcement.Title)
            {
                announcementFromDatabase.Title = announcement.Title;
                announcementIsChanged = true;
            }

            if(announcement.ValidFrom != null && announcementFromDatabase.ValidFrom != announcement.ValidFrom)
            {
                announcementFromDatabase.ValidFrom = announcement.ValidFrom;
                announcementIsChanged = true;
            }

            if (announcement.ValidTo != null && announcementFromDatabase.ValidTo != announcement.ValidTo)
            {
                announcementFromDatabase.ValidTo = announcement.ValidTo;
                announcementIsChanged = true;
            }

            if (announcement.EventDate != null && announcementFromDatabase.EventDate != announcement.EventDate)
            {
                announcementFromDatabase.EventDate = announcement.EventDate;
                announcementIsChanged = true;
            }

            if(!announcementIsChanged)
            {
                throw new ModelValidationException(ErrorMessagesEnum.Announcement.ZeroUpdatesToSave);
            }

            ValidationFunctions.ThrowExceptionWhenDateIsNotValid(announcementFromDatabase.ValidFrom, announcementFromDatabase.ValidTo);
            _context.Update(announcementFromDatabase);
            await _context.SaveChangesAsync();
            return announcementFromDatabase;
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            if(!await ExistAnnouncementAsync(id))
            {
                return false;
            }

            _context.Announcements.Remove(new Announcement { IdAnnouncement = id });
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ExistAnnouncementAsync(Guid id)
        {
            return await _context.Announcements.CountAsync(a => a.IdAnnouncement == id) > 0;
        }

        private async Task<bool> TitleExists(string title)
        {
            return await _context.Announcements.CountAsync(a => a.Title == title) > 0;
        }
    }
}
