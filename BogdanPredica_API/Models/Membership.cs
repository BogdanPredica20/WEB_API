using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BogdanPredica_API.Models
{
    public class Membership
    {
        [Key]
        public Guid IdMembership { get; set; }

        [ForeignKey("IdMember")]
        public Guid IdMember { get; set; }

        [ForeignKey("IdMembershipType")]
        public Guid IdMembershipType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Range(1, 20, ErrorMessage = "Level poate fi intre 1 si 20")]
        public int Level { get; set; }
    }
}
