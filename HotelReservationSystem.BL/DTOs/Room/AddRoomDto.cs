using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.BL.DTOs.Room
{
    public class AddRoomDto
    {
        public int Id { get; set; }
        public string roomNumber { get; set; }
        public string roomType { get; set; }
        public string? Description { get; set; }
        public int Beds { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
