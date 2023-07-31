using BogdanPredica_API.DataContext;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories;
using BogdanPredica_API.Tests.DbContextHelpers;
using Newtonsoft.Json;

namespace BogdanPredica_API.Tests.Repository.Tests
{
    public class AnnouncementRepositoryTests
    {
        private readonly AnnouncementsRepository _repository;
        private readonly ClubLibraDataContext _context;

        public AnnouncementRepositoryTests()
        {
            _context = DbContextHelpers.DbContextHelper.GetDatabaseContext();
            _repository = new AnnouncementsRepository(_context);
        }

        [Fact]
        public async Task GetAllAnnouncements_ExistsAnnouncements()
        {
            //Arrange -> voi crea cateva anunturi fake in memorie
            Announcement announcement1 = CreateAnnouncement(Guid.NewGuid(), "Anunt1");
            Announcement announcement2 = CreateAnnouncement(Guid.NewGuid(), "Anunt2");
            DbContextHelpers.DbContextHelper.AddAnnouncement(_context, announcement1);
            DbContextHelpers.DbContextHelper.AddAnnouncement(_context, announcement2);

            //Act -> chem metoda pe care vreau sa o testez
            var dbAnnouncements = await _repository.GetAnnouncementsAsync();

            //Assert -> verificam rezultatul
            Assert.Equal(2, dbAnnouncements.Count());
        }

        [Fact]
        public async Task GetAllAnnouncements_WithoutDataInDatabase()
        {
            //Act
            var dbAnnouncements = await _repository.GetAnnouncementsAsync();

            //Assert
            Assert.Empty(dbAnnouncements);
        }

        [Fact]
        public async Task GetAnnouncementById_WithData()
        {
            //Arrange -> creez un anunt fals
            Guid id = Guid.NewGuid();
            Announcement announcement = CreateAnnouncement(id, "Anunt1");
            DbContextHelper.AddAnnouncement(_context, announcement);

            //Act
            var result = await _repository.GetAnnouncementByIdAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.IdAnnouncement);
            Assert.Equal(announcement.Title, result.Title);
            var serializedAnnouncement = JsonConvert.SerializeObject(announcement);
            var serializedResult = JsonConvert.SerializeObject(result);
            Assert.Equal(serializedResult, serializedAnnouncement);
        }

        [Fact]
        public async Task GetAnnouncementById_WithoutData()
        {
            Guid id = Guid.NewGuid();

            //Act 
            var result = await _repository.GetAnnouncementByIdAsync(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAnnouncement_WhenExists()
        {
            //Arrange -> creez un anunt pe care il voi sterge
            Guid id = Guid.NewGuid();
            Announcement announcement = CreateAnnouncement(id, "anunt de sters");
            DbContextHelper.AddAnnouncement(_context, announcement);

            //Act
            bool result = await _repository.DeleteAnnouncementAsync(id);
            var get = await _repository.GetAnnouncementByIdAsync(id);

            //Assert
            Assert.True(result);
            Assert.Null(get);
        }

        [Fact]
        public async Task DeleteAnnouncement_WhenNoExists()
        {
            Guid id = Guid.NewGuid();

            bool result = await _repository.DeleteAnnouncementAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePartially_WhenExists()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Announcement announcement = CreateAnnouncement(id, "anunt");

            //Act
            announcement.EventDate = DateTime.Now.Date;
            var result = await _repository.UpdatePartiallyAnnouncementAsync(id, announcement);

            //Assert
            Assert.Equal(announcement.EventDate, DateTime.Now.Date);
        }

        private Announcement CreateAnnouncement(Guid id, string title)
        {
            Announcement announcement = new Announcement()
            { 
                IdAnnouncement = id,
                Title = title,
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow.AddDays(10),
                EventDate = DateTime.UtcNow.AddDays(5),
                Tags = "tags",
                Text = "text"
            };
            return announcement;
        }

    }
}
