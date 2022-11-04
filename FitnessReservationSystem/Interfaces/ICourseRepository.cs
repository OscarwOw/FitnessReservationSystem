using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ICourseRepository
    {
        public void Add(Course course);
        public Course Get(int id);
        public IEnumerable<Course> Get();
        public void Update(Course course);
        public void Delete(int id);
    }
}
