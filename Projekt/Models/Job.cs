using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Projekt.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime JobStartDate { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime JobEndDate { get; set; }
        
        public string Responsibility {get;set;}

        public string ?WorkerMail { get; set; }

        public bool ?JobAccepted {get; set;}
    }
}
