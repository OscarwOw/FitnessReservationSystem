using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ILectureRepository
    {
        public bool Add(int courseId,Lecture lecture);
        public Lecture GetLecture(int id);
        public IEnumerable<Lecture> GetAll();
        public IEnumerable<Lecture> GetNextWeekLectures();
        public int GetRegistrationCount(int id);
        public bool Update(Lecture lecture);
        public bool Delete(Lecture lecture);
    }
}
