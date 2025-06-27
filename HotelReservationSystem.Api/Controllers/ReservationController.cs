using HotelReservation.DataAccess.Models;
using HotelReservationSystem.BL.DTOs.Hotel;
using HotelReservationSystem.BL.DTOs.Reservation;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IBaseRepository<Reservation> _reservationRepository;
        public ReservationController(IBaseRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            if (reservations == null || reservations.Count == 0)
            {
                return NotFound("No hotels found.");
            }
            return Ok(reservations);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.GetByIDAsync(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(AddReservationDto reservationDto)
        {
            Reservation reservation = new Reservation
            {
                TotalPrice = reservationDto.TotalPrice,
                Status = reservationDto.Status,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                CheckOutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                CreatedAt = DateTime.Now,
            };
            if (reservation == null)
            {
                return BadRequest("reservation data is null.");
            }
            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveAsync();
            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateReservation(int id, AddReservationDto reservationDto)
        {
            var existingReservation = await _reservationRepository.GetByIDAsync(id);
            if (existingReservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }
            existingReservation.TotalPrice = reservationDto.TotalPrice;
            existingReservation.Status = reservationDto.Status;
            existingReservation.CheckInDate = DateOnly.FromDateTime(DateTime.Now);
            existingReservation.CheckOutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            existingReservation.CreatedAt = DateTime.Now;
            await _reservationRepository.UpdateAsync(existingReservation);
            await _reservationRepository.SaveAsync();
            return Ok($"{existingReservation.Id} Updated Successfully");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIDAsync(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }
            await _reservationRepository.DeleteAsync(reservation);
            await _reservationRepository.SaveAsync();
            return Ok($"{reservation.Id} Deleted Successfully");
        }
    }
}
