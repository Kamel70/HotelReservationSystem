using HotelReservation.DataAccess.Models;
using HotelReservationSystem.BL.DTOs.Hotel;
using HotelReservationSystem.BL.DTOs.Room;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IBaseRepository<Room> _roomRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoomController(IBaseRepository<Room> roomRepository,UserManager<ApplicationUser> userManager)
        {
            _roomRepository = roomRepository;
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllAsync(new[] { "RoomImages" });
            if (rooms == null || rooms.Count() == 0)
            {
                return NotFound("No rooms found.");
            }
            return Ok(rooms);
        }

        [HttpGet("GetById{id:int}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepository.FindAsync(r => r.Id == id, new[] { "RoomImages"});
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }
            return Ok(room);
        }

        [HttpGet("GetByHotelId{id:int}")]
        public async Task<IActionResult> GetRoomByHotelId(int id)
        {
            var rooms = await _roomRepository.FindAllAsync(r => r.HotelId == id, new[] { "RoomImages" });
            if (rooms == null)
            {
                return NotFound($"This Hotel doesn't have rooms");
            }
            return Ok(rooms);
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetRoomByUserId()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var room = await _roomRepository.FindAsync(r => r.UserId == user.Id);
            if (room == null)
            {
                return NotFound($"you doesn't have any rooms");
            }
            return Ok(room);
        }

        [HttpPost("Create")]
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

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateRoom(int id, AddRoomDto roomDto)
        {
            var existingRoom = await _roomRepository.FindAsync(r => r.Id == id);
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
            Room updatedRoom=_roomRepository.Update(existingRoom);
            if (updatedRoom == null)
            {
                return BadRequest("Failed to update room.");
            }
            await _roomRepository.SaveAsync();
            return Ok($"The Room {existingRoom.roomNumber} Updated Successfully");
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _roomRepository.FindAsync(r => r.Id == id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }
            _roomRepository.Delete(room);
            await _roomRepository.SaveAsync();
            return Ok($"The room {room.roomNumber} Deleted Successfully");
        }
    }
}
