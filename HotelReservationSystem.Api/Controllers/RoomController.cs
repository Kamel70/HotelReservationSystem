using HotelReservation.DataAccess.Models;
using HotelReservationSystem.BL.DTOs.Hotel;
using HotelReservationSystem.BL.DTOs.Room;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IBaseRepository<Room> _roomRepository;
        public RoomController(IBaseRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllAsync();
            if (rooms == null || rooms.Count == 0)
            {
                return NotFound("No rooms found.");
            }
            return Ok(rooms);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepository.GetByIDAsync(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(AddRoomDto roomDto)
        {
            Room room = new Room
            {
                roomNumber = roomDto.roomNumber,
                roomType = roomDto.roomType,
                Description = roomDto.Description,
                Beds = roomDto.Beds,
                PricePerNight = roomDto.PricePerNight,
                IsAvailable = roomDto.IsAvailable
            };
            if (room == null)
            {
                return BadRequest("Room data is null.");
            }
            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveAsync();
            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRoom(int id, AddRoomDto roomDto)
        {
            var existingRoom = await _roomRepository.GetByIDAsync(id);
            if (existingRoom == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }
            existingRoom.roomNumber = roomDto.roomNumber;
            existingRoom.roomType = roomDto.roomType;
            existingRoom.Description = roomDto.Description;
            existingRoom.Beds = roomDto.Beds;
            existingRoom.PricePerNight = roomDto.PricePerNight;
            existingRoom.IsAvailable = roomDto.IsAvailable;
            await _roomRepository.UpdateAsync(existingRoom);
            await _roomRepository.SaveAsync();
            return Ok($"The Room {existingRoom.roomNumber} Updated Successfully");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _roomRepository.GetByIDAsync(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }
            await _roomRepository.DeleteAsync(room);
            await _roomRepository.SaveAsync();
            return Ok($"The room {room.roomNumber} Deleted Successfully");
        }
    }
}
