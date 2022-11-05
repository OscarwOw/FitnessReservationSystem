using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lecture>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var lectures = _lectureRepository.GetAll();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(lectures == null)
            {
                return NotFound();
            }
            return Ok(lectures);
        }

        // GET api/<LectureController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Lecture))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetLecture(int id)
        {
            var lecture = _lectureRepository.GetLecture(id);
            if (lecture == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();    
            }
            return Ok(lecture);
        }
        [HttpGet("{id}/ReservationCount")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetLectureRegistrationsCount(int id)
        {
            int count = _lectureRepository.GetRegistrationCount(id);
            if (count == -1)
            {
                return NotFound();
            }
            return Ok(count);
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
