using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace DotNetProject.Models
{
    public class Movies
    {
        [Key]
        public int MovieId { get; set; }
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public string Cast { get; set; }

        public  string Genre { get; set; }
        public int Duration { get; set; }
        [ForeignKey("PricingDetails")]
        public int PriceId { get; set; }
        [Display(Name ="Price")]
        public PricingDetails PricingDetails { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; } = "";
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public ICollection<ShowTiming> showTimings { get; set; }

    }
    public class PricingDetails
    {
        [Key]
        public int PriceId { get; set; }
        public int Price { get; set; }
        public ICollection<Movies> Movies { get; set; }
    }
}
