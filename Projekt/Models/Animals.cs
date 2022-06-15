using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Animals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię pupila")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Opis")]
        [MaxLength(385)]
        public string Description { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime ReportDate { get; set; }

        public bool Accepted { get; set; }

        public List<FileEntity> FilePaths { get; set; }
    }
}
