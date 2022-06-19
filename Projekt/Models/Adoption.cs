using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Adoption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Gatunek")]
        [MaxLength(20)]
        public string Species { get; set; }

        [Required]
        [Display(Name = "Imię pupila")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Rasa")]
        [MaxLength(20)]
        public string Breed { get; set; }

        [Required]
        [Display(Name = "Wiek")]
        [MaxLength(2)]
        public string Age { get; set; }

        [Required]
        [Display(Name = "Opis")]
        [MaxLength(385)]
        public string Description { get; set; }

        public DateTime AdoptionDate { get; set; }

        public List<AdoptionFileEntity> FilePaths { get; set; }

    }
}

