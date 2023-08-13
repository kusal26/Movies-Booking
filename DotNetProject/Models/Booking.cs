using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetProject.Models
{
    public class Booking
    {
        [Key]
        public int Booking_Id { get; set; }
        [ForeignKey("ShowTiming")]
        public int Timing_Id { get; set; }
        [Display(Name ="total Seats Booked")]
        public int NoOfBookedSeats { get; set; }
        public int Totalprice { get; set; }
        public int PricePerEach { get; set; }
        public DateTime BookedDate { get; set; }
        public ShowTiming ShowTiming { get; set; }
           
    }
}
