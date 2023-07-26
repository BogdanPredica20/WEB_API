using System.ComponentModel.DataAnnotations;

namespace BogdanPredica_API.Models
{
    public class Announcement
    {
        [Key]
        public Guid IdAnnouncement { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        public DateTime ValidTo { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Campul title poate sa contina minim 5 caractere si maxim 250 caractere")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Campul text poate sa contina minim 5 caractere si maxim 1000 caractere")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Campul tags poate sa contina minim 5 caractere si maxim 1000 caractere")]
        public string Tags { get; set; }
    }
}
