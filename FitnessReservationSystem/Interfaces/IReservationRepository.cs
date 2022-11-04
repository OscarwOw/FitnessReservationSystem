using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface IReservationRepository
    {
        public void Add(Reservation reservation);
        public Reservation Get(int id);
        public IEnumerable<Reservation> Get();
        public void Update(Reservation reservation);
        public void Delete(int id);

    }
}
