using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ICourseRepository
    {
        public bool Add(Course course);
        public Course GetById(int id);
        public Course GetByName(string name);
        public ICollection<Course> GetAll();
        public ICollection<Tag> GetTags(int id);
        public ICollection<Lecture> GetLectures(int id);
        public bool Update(Course course);
        public void Delete(int id);
    }
}
