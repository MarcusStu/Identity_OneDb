namespace MarcusOneDbTest.Migrations.TheDbContext
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MarcusOneDbTest.Models.TheContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\TheDbContext";
        }

        protected override void Seed(MarcusOneDbTest.Models.TheContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            context.Countries.AddOrUpdate(
              p => p.Name,
              new Country { Name = "Sweden" },
              new Country { Name = "America" },
              new Country { Name = "England" }
            );

            context.SaveChanges();

            context.Cities.AddOrUpdate(
              p => p.Name,
              new City { Name = "Stockholm", CountryID = context.Countries.Single(l => l.Name == "Sweden").Id },
              new City { Name = "Washington D.C", CountryID = context.Countries.Single(l => l.Name == "America").Id },
              new City { Name = "London", CountryID = context.Countries.Single(l => l.Name == "England").Id }
            );

            context.SaveChanges();

            context.Persons.AddOrUpdate(
              p => p.Name,
              new Person { Name = "Andrew Peters", CityID = context.Cities.Single(l => l.Name == "Stockholm").Id, AddedById = "d1522dc2-5d50-4d3a-906c-32477dd58ef7", AddedByUserName = "test@test.com" },
              new Person { Name = "Brice Lambson", CityID = context.Cities.Single(l => l.Name == "Washington D.C").Id, AddedById = "d1522dc2-5d50-4d3a-906c-32477dd58ef7", AddedByUserName = "test@test.com" },
              new Person { Name = "Rowan Miller", CityID = context.Cities.Single(l => l.Name == "London").Id, AddedById = "d1522dc2-5d50-4d3a-906c-32477dd58ef7", AddedByUserName = "test@test.com" }
            );
        }
    }
}
