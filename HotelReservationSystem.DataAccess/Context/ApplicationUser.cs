using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HotelReservation.DataAccess.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public Hotel hotel { get; set; }

        public ApplicationUser()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
