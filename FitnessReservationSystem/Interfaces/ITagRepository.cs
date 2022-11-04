using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Interfaces
{
    public interface ITagRepository
    {
        public void Add(Tag tag);
        public Tag Get(int id);
        public IEnumerable<Tag> Get();
        public void Update(Tag tag);
        public void Delete(int id);
    }
}
