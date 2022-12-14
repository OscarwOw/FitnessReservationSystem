using AutoMapper;
using AutoMapper.Features;
using FitnessReservationSystem.Dto.ReservationDtos;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using FitnessReservationSystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger, IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]
        public IActionResult GetAll()
        {
            var reservations = _reservationRepository.GetAll();
            List<ReservationDTO> reservationdtos = new List<ReservationDTO>();
            foreach (var reservation in reservations)
            {
                reservationdtos.Add(_mapper.Map<ReservationDTO>(reservation));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] GetReservations Returned: 200 Ok");
            return Ok(reservationdtos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var reservation = _mapper.Map<ReservationDTO>(_reservationRepository.GetReservation(id));
            if (reservation == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(reservation);
        }
        [HttpGet("/FindByName/{name}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetByName(string name)
        {
            var reservation = _mapper.Map<ReservationDTO>(_reservationRepository.GetReservationByName(name));
            if (reservation == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(reservation);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReservation([FromQuery] int lectureId,[FromBody] ReservationDTO reservationDto)
        {
            if(reservationDto == null)
            {
                return BadRequest();
            }
            if(_reservationRepository.CheckIfExists(lectureId, reservationDto.Mail))
            {
                ModelState.AddModelError("", "reservation with this mail to this lecture already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var reservation = _mapper.Map<Reservation>(reservationDto);
            if (!_reservationRepository.Add(lectureId, reservation))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            _logger.LogInformation("[" + DateTime.Now.ToString() + "] CreateReservation Returned: NoContent Created");
            return NoContent();
        }     
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateReservation(int id,[FromBody] ReservationDTO reservationdto)
        {
            if (reservationdto == null)
            {
                return BadRequest(ModelState);
            }
            if (_reservationRepository.GetReservation(reservationdto.Id) == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reservationMap = _mapper.Map<Reservation>(reservationdto);
            if (!_reservationRepository.Update(reservationMap))
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
        public IActionResult DeleteReservation(int id)
        {
            var reservationToDelete = _reservationRepository.GetReservation(id);
            if (reservationToDelete == null)
            {
                return NotFound();
            }         
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reservationRepository.Delete(reservationToDelete))
            {
                ModelState.AddModelError("", "something went wrong");
            }
            return NoContent();
        }
    }
}
