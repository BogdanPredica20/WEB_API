using BogdanPredica_API.DataContext;
using BogdanPredica_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.Tests.DbContextHelpers
{
    public class DbContextHelper
    {
        public static ClubLibraDataContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ClubLibraDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // permite configurarea si utilizarea unei baze de date in memorie
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var databaseContext = new ClubLibraDataContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        public static Announcement AddAnnouncement(ClubLibraDataContext dbContext, Announcement model)
        {
            dbContext.Add(model);
            dbContext.SaveChanges();
            dbContext.Entry(model).State = EntityState.Detached;
            return model;
        }

        /*
        public static Member AddMember(ClubLibraDbContext dbContext, MemberModel model)
        {
            dbContext.Add(model);
            dbContext.SaveChanges();
            dbContext.Entry(model).State = EntityState.Detached;
            return model;
        }*/
    }
}
