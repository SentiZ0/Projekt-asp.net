using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Letterbox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "Zawartość")]
        [MaxLength(385)]
        public string Content { get; set; }

        [Display(Name = "Data przesłania")]
        public DateTime MailDate { get; set; }

        [Display(Name = "Adresat")]
        public string SenderId { get; set; }

        [Required]
        [Display(Name = "Odbiorca")]
        public string ReceiverId { get; set; }
    }
}
