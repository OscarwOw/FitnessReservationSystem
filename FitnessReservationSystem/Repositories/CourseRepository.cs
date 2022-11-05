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
        public Course Get(int id)
        {
            return _databaseContext.Courses.Where(e => e.Id == id).FirstOrDefault();   
        }

        public Course Get(string name)
        {
            return _databaseContext.Courses.Where(e => e.Name == name).FirstOrDefault();
        }

        public ICollection<Course> Get()
        {
            return _databaseContext.Courses.OrderBy(e => e.Id).ToList(); //could be Ienumerable context.course 
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
