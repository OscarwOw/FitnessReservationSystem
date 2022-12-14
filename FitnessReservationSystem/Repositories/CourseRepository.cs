using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessReservationSystem.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CourseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(Course course)
        {
            _databaseContext.Add(course);
            _databaseContext.SaveChanges();
            return true;
        }
        public Course GetCourse(int id)
        {
            return _databaseContext.Courses.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();
        }

        public Course GetByName(string name)
        {
            return _databaseContext.Courses.AsNoTracking().Where(e => e.Name == name).FirstOrDefault();
        }
        public ICollection<Course> GetAll()
        {
            return _databaseContext.Courses.ToList();
        }
        public ICollection<Tag> GetTags(int id)
        {
            return _databaseContext.Courses.Where(e => e.Id == id).SelectMany(e => e.Tags).ToList(); // m:n relationship
        }
        public ICollection<Lecture> GetLectures(int id)
        {
            return _databaseContext.Lectures.Where(e => e.Course.Id == id).ToList(); //1:m relationship
        }

        public bool Update(Course course)
        {
            _databaseContext.Update(course);
            _databaseContext.SaveChanges();
            return true;
        }
        public bool Delete(Course course)
        {
            _databaseContext.Remove(course);
            _databaseContext.SaveChanges();
            return true;
        }
        public IEnumerable<Lecture> GetLecturesNextWeek(int id)
        {
            IEnumerable<Lecture> lectures = _databaseContext.Lectures.Where(e => e.Course.Id == id).ToList();
            lectures = lectures.Where(lecture => DateTime.Compare(lecture.Date, DateTime.Today) >= 0 ? true : false);
            return lectures.Where(item => DateTime.Compare(item.Date, DateTime.Today.AddDays(7)) < 0 ? true : false);

        }
    }
}
