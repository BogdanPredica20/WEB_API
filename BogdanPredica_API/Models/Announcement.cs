using System.ComponentModel.DataAnnotations;

namespace BogdanPredica_API.Models
{
    public class Announcement
    {
        [Key]
        public Guid IdAnnouncement { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        [StringLength(250, MinimumLength = 5, ErrorMessage = "Campul title poate sa contina minim 5 caractere si maxim 250 caractere")]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Campul text poate sa contina minim 5 caractere si maxim 1000 caractere")]
        public string? Text { get; set; }

        public DateTime? EventDate { get; set; }

        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Campul tags poate sa contina minim 5 caractere si maxim 1000 caractere")]
        public string? Tags { get; set; }
    }
}
