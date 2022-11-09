using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface IReservationRepository
    {
        public bool Add(int LectureId,Reservation reservation);
        public Reservation GetReservation(int id);
        public Reservation GetReservationByName(string name);
        public ICollection<Reservation> GetAll();
        public void Update(Reservation reservation);
        public void Delete(int id);
        public bool CheckIfExists(int LectureId, string mail);

    }
}
