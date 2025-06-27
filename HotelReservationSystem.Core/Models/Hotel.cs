using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelReservation.DataAccess.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string phonenumber { get; set; }
        public string Email { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public string managerId { get; set; }
        public Hotel()
        {
            Rooms = new HashSet<Room>();
        }
    }
}
