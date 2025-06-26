using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DataAccess.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string roomNumber { get; set; }
        public string roomType { get; set; }
        public string? Description { get; set; }
        public int Beds { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<RoomImages> RoomImages { get; set; }
        public Room()
        {
            Reservations = new HashSet<Reservation>();
            RoomImages = new HashSet<RoomImages>();
        }
    }
}
