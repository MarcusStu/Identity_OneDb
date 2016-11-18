using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarcusOneDbTest.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required]
        [Display(Name = "City Name")]
        public int? CityID { get; set; }

        public virtual City City { get; set; }

        public string AddedById { get; set; }

        [Display(Name = "Added by")]
        public string AddedByUserName { get; set; }
    }
}