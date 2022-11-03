using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessReservationSystem.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int Price { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
