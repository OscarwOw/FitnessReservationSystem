using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly DatabaseContext _databaseContext;
        public LectureRepository(DatabaseContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public void Add(Lecture lecture)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Lecture Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lecture> Get()
        {
            throw new NotImplementedException();
        }

        public void Update(Lecture lecture)
        {
            throw new NotImplementedException();
        }
    }
}
