using System.ComponentModel.DataAnnotations;

namespace FitnessReservationSystem.Dto.CourseDtos
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int Price { get; set; }
    }
}
