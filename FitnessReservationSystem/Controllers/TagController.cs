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
            List<TagDTO> tagsdtos = new List<TagDTO>();
            foreach (var tag in tags)
            {
                tagsdtos.Add(_mapper.Map<TagDTO>(tag));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(tagsdtos);
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
            List<CourseDTO> coursesdtos = new List<CourseDTO>();
            foreach (var course in courses)
            {
                coursesdtos.Add(_mapper.Map<CourseDTO>(course));
            }
            if (courses == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(coursesdtos);
        }

        
        [HttpPost]
        public IActionResult CreateTag([FromBody] TagDTO tagDto)
        {
            if (tagDto== null)
            {
                return BadRequest();
            }
            var tag = _tagRepository.GetAll().Where(e => e.Name.Trim().ToUpper() == tagDto.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (tag != null)
            {
                ModelState.AddModelError("", "Tag already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tagmap = _mapper.Map<Tag>(tagDto);
            if (!_tagRepository.Add(tagmap))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTag([FromBody] TagDTO tagdto)
        {
            if (tagdto == null)
            {
                return BadRequest(ModelState);
            }
            if (_tagRepository.GetTag(tagdto.Id) == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tagMap = _mapper.Map<Tag>(tagdto);
            if (!_tagRepository.Update(tagMap))
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
        public IActionResult DeleteTag([FromQuery]int id)
        {
            var tagToDelete = _tagRepository.GetTag(id);
            if (tagToDelete == null)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_tagRepository.Delete(tagToDelete))
            {
                ModelState.AddModelError("", "something went wrong");
            }
            return NoContent();
        }
    }
}
