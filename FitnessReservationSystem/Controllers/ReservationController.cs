using AutoMapper;
using AutoMapper.Features;
using FitnessReservationSystem.Dto;
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

        public ReservationController(IReservationRepository reservationRepository,IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
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

        // POST api/<ReservationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReservationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
