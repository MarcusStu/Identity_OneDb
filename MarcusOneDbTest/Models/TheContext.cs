using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MarcusOneDbTest.Models
{
    public class TheContext : DbContext
    {
        public TheContext() : base("DefaultConnection")
        { }


        public static TheContext Create()
        {
            return new TheContext();
        }


        public DbSet<Person> Persons { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}