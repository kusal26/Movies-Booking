using System.ComponentModel.DataAnnotations;

namespace DotNetProject.Models
{
    public class MovieHall
    {
        [Key]
        public int HallId { get; set; }
        public string HallName { get; set; }
        public string HallLocation { get; set; }
        public int TotalSeat { get; set; }
       
       public ICollection<ShowTiming> Timing { get; set; }
       
    }
}
