using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarcusOneDbTest.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Country Name")]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}