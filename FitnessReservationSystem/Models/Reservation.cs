using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessReservationSystem.Models
{
    public class Reservation 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public DateTime CreatedDate { get; set; }
        public Lecture Lecture { get; set; }
        public ApplicationUser User { get; set; }
    }
}
