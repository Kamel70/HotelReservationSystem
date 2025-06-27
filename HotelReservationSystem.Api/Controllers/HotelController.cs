using HotelReservation.DataAccess.Models;
using HotelReservationSystem.BL.DTOs.Hotel;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IBaseRepository<Hotel> _hotelRepository;
        public HotelController(IBaseRepository<Hotel> hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            if (hotels == null || hotels.Count == 0)
            {
                return NotFound("No hotels found.");
            }
            return Ok(hotels);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelRepository.FindAsync(h=>h.Id==id);
            if (hotel == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel(addHotelDto hotelDto)
        {
            Hotel hotel = new Hotel
            {
                Name = hotelDto.Name,
                Address = hotelDto.Address,
                City = hotelDto.City,
                County = hotelDto.Country,
                phonenumber = hotelDto.PhoneNumber,
                Email = hotelDto.Email
            };
            if (hotel == null)
            {
                return BadRequest("Hotel data is null.");
            }
            await _hotelRepository.AddAsync(hotel);
            await _hotelRepository.SaveAsync();
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHotel(int id, addHotelDto hotelDto)
        {
            var existingHotel = await _hotelRepository.FindAsync(h => h.Id == id);
            if (existingHotel == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }
            existingHotel.Name = hotelDto.Name;
            existingHotel.Address = hotelDto.Address;
            existingHotel.City = hotelDto.City;
            existingHotel.County = hotelDto.Country;
            existingHotel.phonenumber = hotelDto.PhoneNumber;
            existingHotel.Email = hotelDto.Email;
            Hotel updatedHotel=_hotelRepository.Update(existingHotel);
            if(updatedHotel == null)
            {
                return BadRequest("Failed to update the hotel.");
            }
            await _hotelRepository.SaveAsync();
            return Ok($"The Hotel {existingHotel.Name} Updated Successfully");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.FindAsync(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }
            _hotelRepository.Delete(hotel);
            await _hotelRepository.SaveAsync();
            return Ok($"The Hotel {hotel.Name} Deleted Successfully");
        }
    }
}
