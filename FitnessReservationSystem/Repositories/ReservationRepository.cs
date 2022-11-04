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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Reservation Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> Get()
        {
            throw new NotImplementedException();
        }

        public void Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
