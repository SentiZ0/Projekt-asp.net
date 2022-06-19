using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Animals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Imię pupila")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Display(Name = "Opis")]
        [MaxLength(385)]
        public string Description { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime ReportDate { get; set; }

        public bool Accepted { get; set; }

        [Display(Name = "Zdjęcia")]
        public List<FileEntity> FilePaths { get; set; }

        public List<Post> Posts { get; set; }
    }
}
