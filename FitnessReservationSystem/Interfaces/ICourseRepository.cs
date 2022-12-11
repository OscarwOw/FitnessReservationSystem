using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ICourseRepository
    {
        public bool Add(Course course);
        public Course GetCourse(int id);
        public Course GetByName(string name);
        public ICollection<Course> GetAll();
        public ICollection<Tag> GetTags(int id);
        public ICollection<Lecture> GetLectures(int id);
        public IEnumerable<Lecture> GetLecturesNextWeek(int id);
        public bool Update(Course course);
        public bool Delete(Course course);
    }
}
