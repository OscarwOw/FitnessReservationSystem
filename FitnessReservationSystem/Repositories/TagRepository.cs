using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;

namespace FitnessReservationSystem.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _databaseContext;
        public TagRepository(DatabaseContext databaseContext) 
        { 
            _databaseContext = databaseContext;
        }
        public void Add(Tag tag)
        {
            throw new NotImplementedException();
        }
        public ICollection<Tag> GetAll()
        {
            return _databaseContext.Tags.ToList();
        }
        public Tag GetTag(int id)
        {
            return _databaseContext.Tags.Where(e => e.Id == id).FirstOrDefault();
        }
        public ICollection<Course> GetTagsCourses(int id)
        {
            return _databaseContext.Tags.Where(e => e.Id == id).SelectMany(e => e.Courses).ToList();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
