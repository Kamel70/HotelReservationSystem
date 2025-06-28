using HotelReservation.DataAccess.Models;
using HotelReservationSystem.BL.DTOs.Hotel;
using HotelReservationSystem.BL.DTOs.Reservation;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IBaseRepository<Reservation> _reservationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReservationController(IBaseRepository<Reservation> reservationRepository,UserManager<ApplicationUser> userManager)
        {
            _reservationRepository = reservationRepository;
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetAllAsync(new[] { "Room", "Payment" });
            if (reservations == null || reservations.Count() == 0)
            {
                return NotFound("No Reservation found.");
            }
            return Ok(reservations);
        }
        [HttpGet("GetByID{id:int}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.FindAsync(r => r.Id == id, new[] { "Room", "Payment" });
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }
            return Ok(reservation);
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetReservationByUserId()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var reservation = await _reservationRepository.FindAllAsync(r => r.UserId==user.Id, new[] { "Room", "Payment" });
            if (reservation == null)
            {
                return NotFound($"You don't have Reservation");
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
            var existingReservation = await _reservationRepository.FindAsync(r => r.Id == id);
            if (existingReservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }
            existingReservation.TotalPrice = reservationDto.TotalPrice;
            existingReservation.Status = reservationDto.Status;
            existingReservation.CheckInDate = DateOnly.FromDateTime(DateTime.Now);
            existingReservation.CheckOutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            existingReservation.CreatedAt = DateTime.Now;
            Reservation updatedReservation=_reservationRepository.Update(existingReservation);
            if (updatedReservation == null)
            {
                return BadRequest("Failed to update reservation.");
            }
            await _reservationRepository.SaveAsync();
            return Ok($"{existingReservation.Id} Updated Successfully");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationRepository.FindAsync(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }
            _reservationRepository.Delete(reservation);
            await _reservationRepository.SaveAsync();
            return Ok($"{reservation.Id} Deleted Successfully");
        }
    }
}
