using HotelReservation.DataAccess.Models;
using HotelReservationSystem.BL.DTOs;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomImagesController : ControllerBase
    {
        private readonly IBaseRepository<RoomImages> _roomImageRepository;

        public RoomImagesController(IBaseRepository<RoomImages> roomImageRepository)
        {
            _roomImageRepository = roomImageRepository;
        }

        [HttpGet("GetAllByRoomId/{id:int}")]
        public async Task<IActionResult> GetAllRoomImagesByRoomId(int id)
        {
            var roomImages = await _roomImageRepository.FindAllAsync(r => r.RoomId == id);
            if (roomImages == null || !roomImages.Any())
            {
                return NotFound($"No images found for Image ID {id}.");
            }
            return Ok(roomImages);
        }
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetRoomImageById(int id)
        {
            var roomImage = await _roomImageRepository.FindAsync(r => r.Id == id);
            if (roomImage == null)
            {
                return NotFound($"Room Image with ID {id} not found.");
            }
            return Ok(roomImage);
        }

        [HttpPost("Create/{roomId:int}")]
        public async Task<IActionResult> CreateRoomImage(int roomId,addRoomImageDto roomImageDto)
        {
            RoomImages roomImage = new RoomImages
            {
                ImageUrl = roomImageDto.Url,
                RoomId = roomId,
                Caption = roomImageDto.Caption,
            };
            if (roomImage == null)
            {
                return BadRequest("Room image data is null.");
            }
            await _roomImageRepository.AddAsync(roomImage);
            await _roomImageRepository.SaveAsync();
            return CreatedAtAction(nameof(GetRoomImageById), new { id = roomImage.Id }, roomImage);
        }

        [HttpPost("Update/{roomId:int}")]
        public async Task<IActionResult> UpdateRoomImage(int roomId, addRoomImageDto roomImageDto)
        {
            RoomImages roomImage = new RoomImages
            {
                ImageUrl = roomImageDto.Url,
                RoomId = roomId,
                Caption = roomImageDto.Caption,
            };
            if (roomImage == null)
            {
                return BadRequest("Room image data is null.");
            }
            RoomImages newRoomImage=_roomImageRepository.Update(roomImage);
            if (newRoomImage == null)
            {
                return BadRequest("Failed to update room Image.");
            }
            await _roomImageRepository.SaveAsync();
            return Ok($"The Room Image with ID: {newRoomImage.Id} Updated Successfully");
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteRoomImage(int id)
        {
            var roomImage = await _roomImageRepository.FindAsync(r => r.Id == id);
            if (roomImage == null)
            {
                return NotFound($"Room Image with ID {id} not found.");
            }
            _roomImageRepository.Delete(roomImage);
            await _roomImageRepository.SaveAsync();
            return Ok($"The Room Image with ID: {id} Deleted Successfully");
        }

    }
}
