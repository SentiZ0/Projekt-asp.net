using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        public int AnimalsId { get; set; }

        [Display(Name="Dodaj komentarz")]
        public string Content { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime PostDate { get; set; }
    }
}
