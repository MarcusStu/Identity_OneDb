using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarcusOneDbTest.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "City Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public int? CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}