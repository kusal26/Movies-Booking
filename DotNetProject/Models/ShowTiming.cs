using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetProject.Models
{
    public class ShowTiming
    {
        [Key]
        public int TimingId { get; set; }
        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        [ForeignKey("MovieHall")]
        public int HallId { get; set; }
        public DateTime show_datetime { get; set; }
      // public ICollection<ShowTiming> Timing { get; set; }
        public int? available_seats { get; set; }
        public Movies Movies { get; set; }
        public MovieHall MovieHall { get; set; }
        

        public ICollection<Booking> bookings { get; set; }

            
    }
}
