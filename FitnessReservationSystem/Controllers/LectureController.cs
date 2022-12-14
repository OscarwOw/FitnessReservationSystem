using AutoMapper;
using FitnessReservationSystem.Dto.LectureDtos;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using FitnessReservationSystem.Repositories;
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
        private readonly ILogger<LectureController> _logger;
        public LectureController(ILogger<LectureController> logger,ILectureRepository lectureRepository, IMapper mapper)
        {
            _lectureRepository = lectureRepository;
            _mapper = mapper;
            _logger = logger;
        }

        
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
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] GetLectures Returned: 200 Ok");
            return Ok(lecturesdtos);
        }

        [HttpGet("NextWeek")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lecture>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNextWeekLectures()
        {
            var lectures = _lectureRepository.GetNextWeekLectures();
            List<LectureDTOWithCapacity> lecturesdtos = new List<LectureDTOWithCapacity>();
            foreach (var lecture in lectures)
            {
                //lecturesdtos.Add(_mapper.Map<LectureDTOWithCapacity>(lecture));

                LectureDTOWithCapacity lecturedto = _mapper.Map<LectureDTOWithCapacity>(lecture);
                lecturedto.RegistredCount = _lectureRepository.GetRegistrationCount(lecturedto.Id);
                lecturesdtos.Add(lecturedto);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (lectures == null)
            {
                _logger.LogError("[" + DateTime.Now.ToString() + "] GetLecturesForNextWeek Returned: 404 NotFound");
                return NotFound();
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] GetLecturesForNextWeek Returned: 200 Ok");
            return Ok(lecturesdtos);
        }


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
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] GetLecture Returned: 200 Ok");
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
        

        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLecture([FromQuery] int CourseId,[FromBody] LectureDTO lecturedto)
        {
            if(lecturedto == null)
            {
                return BadRequest();
            }
            var lectures = _lectureRepository.GetAll().Where(e => e.Name.Trim().ToUpper() == lecturedto.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (lectures != null)
            {
                ModelState.AddModelError("", "Instance already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var lecture = _mapper.Map<Lecture>(lecturedto);
            if (!_lectureRepository.Add(CourseId, lecture))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] CreateLecture Returned: 200 Ok");
            return Ok("Success");
        }

        
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateLecture(int id,[FromBody] LectureDTO lecturedto)
        {
            if (lecturedto == null)
            {
                return BadRequest(ModelState);
            }
            if (_lectureRepository.GetLecture(lecturedto.Id) == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var lectureMap = _mapper.Map<Lecture>(lecturedto);
            if (!_lectureRepository.Update(lectureMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLecture(int id)
        {
            var lectureToDelete = _lectureRepository.GetLecture(id);
            if (lectureToDelete == null)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_lectureRepository.Delete(lectureToDelete))
            {
                ModelState.AddModelError("", "something went wrong");
            }
            return NoContent();
        }
    }
}
