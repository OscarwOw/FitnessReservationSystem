using AutoMapper;
using FitnessReservationSystem.Dto;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using FitnessReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitnessReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        public TagController(ITagRepository tagRepository,IMapper mapper) 
        {
            _tagRepository = tagRepository;
            _mapper = mapper; 
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        public IActionResult GetTags()
        {
            var tags = _tagRepository.GetAll();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(tags);
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Tag))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var tag = _mapper.Map<TagDTO>(_tagRepository.GetTag(id));
            if (tag == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(tag);
        }
        [HttpGet("{id}/courses")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCourses(int id)
        {
            var courses = _tagRepository.GetTagsCourses(id);
            if (courses == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(courses);
        }

        // POST api/<TagController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
