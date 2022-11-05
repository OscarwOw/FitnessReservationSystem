using AutoMapper;
using FitnessReservationSystem.Dto;
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
        private readonly IMapper _mapper;
        public LectureController(ILectureRepository lectureRepository, IMapper mapper)
        {
            _lectureRepository = lectureRepository;
            _mapper = mapper;
        }

        // GET: api/<LectureController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lecture>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var lectures = _lectureRepository.GetAll();
            List<LectureDTO> lecturesdtos = new List<LectureDTO>();
            foreach (var lecture in lectures)
            {
                lecturesdtos.Add(_mapper.Map<LectureDTO>(lecture));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(lectures == null)
            {
                return NotFound();
            }
            return Ok(lecturesdtos);
        }

        // GET api/<LectureController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(LectureDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetLecture(int id)
        {
            var lecture = _mapper.Map<LectureDTO>(_lectureRepository.GetLecture(id));
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
