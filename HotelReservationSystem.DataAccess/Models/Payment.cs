using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.DataAccess.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Precision(18, 4)]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
