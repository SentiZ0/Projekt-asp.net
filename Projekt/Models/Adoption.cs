using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Adoption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Gatunek")]
        [MaxLength(20)]
        public string Species { get; set; }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Imię pupila")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Rasa")]
        [MaxLength(20)]
        public string Breed { get; set; }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Wiek")]
        [Range(1,100, ErrorMessage ="Wymagana liczba z zakresu 1-100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Opis")]
        [MaxLength(385)]
        public string Description { get; set; }

        public DateTime AdoptionDate { get; set; }

        public List<AdoptionFileEntity> FilePaths { get; set; }

    }
}

