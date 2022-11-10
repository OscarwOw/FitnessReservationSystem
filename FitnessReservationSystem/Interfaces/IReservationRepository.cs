using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface IReservationRepository
    {
        public bool Add(int LectureId,Reservation reservation);
        public Reservation GetReservation(int id);
        public Reservation GetReservationByName(string name);
        public ICollection<Reservation> GetAll();
        public bool Update(Reservation reservation);
        public bool Delete(Reservation reservation);
        public bool CheckIfExists(int LectureId, string mail);

    }
}
