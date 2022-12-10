using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Dto.LectureDtos
{
    public class LectureDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Couch { get; set; }
        public int Capacity { get; set; }
    }
}
