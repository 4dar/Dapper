using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
 
namespace dojoleague.Models
{
    public abstract class BaseEntity {}
    public class Dojo : BaseEntity
    {
        [Key]
        public long iddojo { get; set; }


        [Required]
        [MinLength(2, ErrorMessage="Dojo name must be at least 2 characters long")]
        public string name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage="Location must be at least 3 characters long")]
        public string location { get; set; }


        [Required(ErrorMessage = "Length field is required")]
        public string info { get; set; }
        
        public ICollection<Ninja> ninjas { get; set; }
 
    }
}