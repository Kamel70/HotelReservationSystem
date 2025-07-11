﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DataAccess.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }
        public string Status {  get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public Room Room { get; set; }
        public Payment Payment { get; set; }
    }
}
