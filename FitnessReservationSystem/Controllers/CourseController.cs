using AutoMapper;
using FitnessReservationSystem.Dto;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;



namespace FitnessReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
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
            return Ok(coursesdtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            var course = _mapper.Map<CourseDTO>(_courseRepository.GetById(id));
            if (course == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
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
            return Ok(tagsdtos);
        }
        [HttpGet("{id}/Lectures")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lecture>))]
        [ProducesResponseType(400)]
        public IActionResult GetLectures(int id)
        {
            var lectures = _courseRepository.GetLectures(id);
            List<LectureDTO> lecturesdtos = new List<LectureDTO>();
            foreach(var lecture in lectures)
            {
                lecturesdtos.Add(_mapper.Map<LectureDTO>(lecture));
            }
            if (lectures == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(lecturesdtos);
        }

        // POST api/<CourseController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
