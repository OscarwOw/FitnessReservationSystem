using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ILectureRepository
    {
        public void Add(Lecture lecture);
        public Lecture Get(int id);
        public IEnumerable<Lecture> Get();
        public void Update(Lecture lecture);
        public void Delete(int id);
    }
}
