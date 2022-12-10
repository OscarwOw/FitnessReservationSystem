using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessReservationSystem.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly DatabaseContext _databaseContext;
        public LectureRepository(DatabaseContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public bool Add(int courseId,Lecture lecture)
        {
            var course = _databaseContext.Courses.Where(c => c.Id== courseId).FirstOrDefault();
            if (course == null)
            {
                return false;
            }
            lecture.Course = course;
            _databaseContext.Add(lecture);
            _databaseContext.SaveChanges();
            return true;
        }
        public IEnumerable<Lecture> GetAll()
        {
            return _databaseContext.Lectures.ToList();
        }

        public IEnumerable<Lecture> GetNextWeekLectures()
        {
            IEnumerable<Lecture> lectures =  _databaseContext.Lectures.Where(lecture => DateTime.Compare(lecture.Date, DateTime.Today) >= 0 ? true : false);
            return lectures.Where(item => DateTime.Compare(item.Date, DateTime.Today.AddDays(7)) < 0 ? true : false);
        }

        public Lecture GetLecture(int id)
        {
            return _databaseContext.Lectures.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();
        }
        public int GetRegistrationCount(int id)
        {
            if(this.GetLecture(id)== null) { return -1; }
            var count = _databaseContext.Reservations.Where(e => e.Lecture.Id == id).Count();
            if(count <= 0)
            {
                return 0;
            }
            return count;
        }
        public bool Update(Lecture lecture)
        {
            _databaseContext.Update(lecture);
            _databaseContext.SaveChanges();
            return true;
        }
        public bool Delete(Lecture lecture)
        {
            _databaseContext.Remove(lecture);
            _databaseContext.SaveChanges();
            return true;
        }
        
    }
}
