using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
 
namespace dojoleague.Models
{
    public class Ninja : BaseEntity
    {
        [Key]
        public long idninja { get; set; }


        [Required]
        [MinLength(2, ErrorMessage="Ninja name must be at least 2 characters long")]
        public string name { get; set; }

        [Required(ErrorMessage="Level required")]
        public int level { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        public string description { get; set; }
        public Dojo dojo { get; set; }
    }
}