using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CourseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Course course)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Course Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> Get()
        {
            throw new NotImplementedException();
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
