using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ReservationRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Reservation reservation)
        {
            throw new NotImplementedException();
        }
        public ICollection<Reservation> GetAll()
        {
            return _databaseContext.Reservations.ToList();
        }
        public Reservation GetReservation(int id)
        {
            return _databaseContext.Reservations.Where(e => e.Id == id).FirstOrDefault();
        }
        public Reservation GetReservationByName(string name)
        {
            return _databaseContext.Reservations.Where(e => e.Name == name).FirstOrDefault();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
