using System.ComponentModel.DataAnnotations;
 
namespace lostinthewoods.Models
{
    public abstract class BaseEntity {}
    public class Trail : BaseEntity
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage="Name field is required")]
        public string name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage="Description must be at least 10 characters")]
        public string description { get; set; }

        [Required(ErrorMessage = "Length field is required")]
        public float length { get; set; }
 
        [Required(ErrorMessage = "Elevation change field is required")]
        public int elevation { get; set; }
 
        [Required(ErrorMessage = "Longitude field is required")]
        public string longitude { get; set; }

        [Required(ErrorMessage = "Latitude field is required")]
        public string latitude { get; set; }
    }
}