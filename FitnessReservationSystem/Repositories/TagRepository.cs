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
        public bool Add(Tag tag)
        {
            _databaseContext.Add(tag);
            _databaseContext.SaveChanges();
            return true;
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
        public bool Update(Tag tag)
        {
            _databaseContext.Update(tag);
            _databaseContext.SaveChanges();
            return true;
        }
    }
}
