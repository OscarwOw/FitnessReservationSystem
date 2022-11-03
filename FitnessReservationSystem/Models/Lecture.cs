using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessReservationSystem.Models
{
    public class Lecture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Couch { get; set; }
        public int Capacity { get; set; }
        public Course Course { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
