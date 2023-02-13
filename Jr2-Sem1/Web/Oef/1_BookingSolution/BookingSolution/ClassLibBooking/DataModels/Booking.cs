using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibBooking.DataModels
{
    public class Booking
    {
        public int BookingId { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        public string? Remarks { get; set; }
        public Student? Student { get; set; }
    }
}
