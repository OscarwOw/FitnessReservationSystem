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
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        // GET: api/<CourseController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetCourses()
        {
            var courses = _courseRepository.GetAll();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(courses);
        }

        // GET api/<CourseController>/5
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
            if (tags == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(tags);
        }
        [HttpGet("{id}/Lectures")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lecture>))]
        [ProducesResponseType(400)]
        public IActionResult GetLectures(int id)
        {
            var lectures = _courseRepository.GetLectures(id);
            if(lectures == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(lectures);
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
