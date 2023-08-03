using BogdanPredica_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.DataContext
{
    public class ClubLibraDataContext : DbContext
    {
        public ClubLibraDataContext(DbContextOptions<ClubLibraDataContext> options) : base(options) { }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        public DbSet<MembershipType> MembershipTypes { get; set; }

        public DbSet<CodeSnippet> CodeSnippets { get; set; }
    }
}
