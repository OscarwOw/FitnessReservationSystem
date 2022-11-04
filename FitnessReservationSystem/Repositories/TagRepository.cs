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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Tag Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> Get()
        {
            throw new NotImplementedException();
        }

        public void Update(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
