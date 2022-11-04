using FitnessReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitnessReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly ILectureRepository _lectureRepository;
        public LectureController(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        // GET: api/<LectureController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LectureController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LectureController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LectureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LectureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
