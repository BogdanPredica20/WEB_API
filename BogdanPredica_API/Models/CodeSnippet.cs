using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BogdanPredica_API.Models
{
    public class CodeSnippet
    {
        [Key]
        public Guid IdCodeSnippet { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Campul title poate sa contina minim 5 caractere si maxim 100 caractere")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        public string ContentCode { get; set; }

        [ForeignKey("IdMember")]
        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        public Guid IdMember { get; set; }

        [Range(1, 100, ErrorMessage = "Revision poate fi intre 1 si 100")]
        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        public int Revision { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!!")]
        public DateTime DateTimeAdded { get; set; }

        public bool IsPublished { get; set; }
    }
}
