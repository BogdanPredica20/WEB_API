using BogdanPredica_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.DataContext
{
    public class ClubLibraDataContext : DbContext
    {
        public ClubLibraDataContext(DbContextOptions<ClubLibraDataContext> options) : base(options) { }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Member> Members { get; set; }

        //public DbSet<MembershipModel> Memberships { get; set; }

        //public DbSet<MembershipTypeModel> MembershipTypes { get; set; }

        public DbSet<CodeSnippet> CodeSnippets { get; set; }
    }
}
