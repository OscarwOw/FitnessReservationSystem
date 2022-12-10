using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Dto.ReservationDtos
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
