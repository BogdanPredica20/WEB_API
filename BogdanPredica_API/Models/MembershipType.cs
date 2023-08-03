using System.ComponentModel.DataAnnotations;

namespace BogdanPredica_API.Models
{
    public class MembershipType
    {
        [Key]
        public Guid IdMembershipType { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Campul name poate sa contina minim 3 caractere si maxim 100 caractere")]
        public string Name { get; set; }

        [StringLength(250, MinimumLength = 5, ErrorMessage = "Campul description poate sa contina minim 5 caractere si maxim 250 caractere")]
        public string Description { get; set; }

        [Range(1, 12, ErrorMessage = "SubscriptionLengthInMonths poate fi intre 1 si 12")]
        public int SubscriptionLengthInMonths { get; set; }
    }
}
