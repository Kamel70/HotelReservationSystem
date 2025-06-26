using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DataAccess.Models
{
    public class RoomImages
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
