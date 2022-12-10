using AutoMapper;
using FitnessReservationSystem.Dto.CourseDtos;
using FitnessReservationSystem.Dto.LectureDtos;
using FitnessReservationSystem.Dto.TagDtos;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using FitnessReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace FitnessReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseController> _logger;
        public CourseController(ILogger<CourseController> logger, ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetCourses()
        {
            var courses = _courseRepository.GetAll();
            List<CourseDTO> coursesdtos = new List<CourseDTO>();
            foreach(var course in courses)
            {
                coursesdtos.Add(_mapper.Map<CourseDTO>(course));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _logger.LogInformation("["+DateTime.Now.ToString()+"] GetCourses Returned: 200 Ok");
            return Ok(coursesdtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            var course = _mapper.Map<CourseDTO>(_courseRepository.GetCourse(id));
            if (course == null)
            {
                _logger.LogError("[" + DateTime.Now.ToString() + "] GetCourse Returned: 404 NotFound");
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] GetCourse Returned: 200 Ok");
            return Ok(course);
        }
        [HttpGet("{id}/tags")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(400)]
        public IActionResult GetTags(int id)
        {
            var tags = _courseRepository.GetTags(id);
            List<TagDTO> tagsdtos = new List<TagDTO>();
            foreach(var tag in tags)
            {
                tagsdtos.Add(_mapper.Map<TagDTO>(tag));
            }
            if (tags == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] GetCourseTags Returned: 200 Ok");
            return Ok(tagsdtos);
        }
        
        [HttpGet("{id}/Lectures")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lecture>))]
        [ProducesResponseType(400)]
        public IActionResult GetLectures(int id)
        {
            var lectures = _courseRepository.GetLectures(id);
            List<LectureDTOWithCapacity> lecturesdtos = new List<LectureDTOWithCapacity>();
            foreach(var lecture in lectures)
            {
                lecturesdtos.Add(_mapper.Map<LectureDTOWithCapacity>(lecture));
            }
            if (lectures == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] GetLecturesofCourse Returned: 200 Ok");
            return Ok(lecturesdtos);           
        }

        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromBody] CourseDTO courseDTO)
        {
            if (courseDTO == null)
            {
                return BadRequest();
            }
            var course = _courseRepository.GetAll().Where(e => e.Name.Trim().ToUpper() == courseDTO.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if(course != null)
            {
                ModelState.AddModelError("", "Course already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var coursemap =  _mapper.Map<Course>(courseDTO);
            if (!_courseRepository.Add(coursemap))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(500, ModelState);
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] CreateLecture Returned: 200 Ok");
            return Ok("success");

        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int id,[FromBody] CourseDTO coursedto)
        {
            if(coursedto == null)
            {
                return BadRequest(ModelState);
            }
            if (_courseRepository.GetCourse(coursedto.Id)==null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var courseMap = _mapper.Map<Course>(coursedto);
            if (!_courseRepository.Update(courseMap))
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
        public IActionResult DeleteCourse(int id)
        {
            var itemToDelete = _courseRepository.GetCourse(id);
            if (itemToDelete == null)
            {
                return NotFound();
            }
            ;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_courseRepository.Delete(itemToDelete))
            {
                ModelState.AddModelError("", "something went wrong");
            }
            return NoContent();
        }
    }
}
