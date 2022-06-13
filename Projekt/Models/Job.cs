using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Projekt.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime JobStartDate { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime JobEndDate { get; set; }
        
        public String Responsibility {get;set;}

        public int WorkerId { get; set; }

        public bool JobAccepted {get; set;}
    }
}
