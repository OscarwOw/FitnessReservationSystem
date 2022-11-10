using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ITagRepository
    {
        public bool Add(Tag tag);
        public Tag GetTag(int id);
        public ICollection<Tag> GetAll();
        public ICollection<Course> GetTagsCourses(int id);
        public bool Update(Tag tag);
        public bool Delete(Tag tag);
    }
}
