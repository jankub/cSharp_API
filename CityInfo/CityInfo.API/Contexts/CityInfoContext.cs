using System.Collections.Generic;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Contexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                 new City()
                 {
                     Id = 1,
                     Name = "New York City",
                     Description = "Big city in USA"
                 },

                 new City()
                 {
                     Id = 2,
                     Name = "Antwerp",
                     Description = "City in Belgium"
                 },

                 new City()
                 {
                     Id = 3,
                     Name = "Paris",
                     Description = "With Eifell Tower"
                 });

            modelBuilder.Entity<PointOfInterest>()
              .HasData(
                new PointOfInterest()
                {
                    Id = 1,
                    CityId = 1,
                    Name = "Central Park",
                    Description = "Park on Manhattan"
                },

                new PointOfInterest()
                {
                    Id = 2,
                    CityId = 1,
                    Name = "Empire State Building",
                    Description = "Very tall building"
                },

                new PointOfInterest()
                {
                    Id = 3,
                    CityId = 2,
                    Name = "Cathedral",
                    Description = "Unfinished church"
                },

                new PointOfInterest()
                {
                    Id = 4,
                    CityId = 2,
                    Name = "Main square",
                    Description = "Old city center",
                },
                new PointOfInterest()
                {
                    Id = 5,
                    CityId = 3,
                    Name = "Eifell Tower",
                    Description = "Symbol of Paris"
                },

                new PointOfInterest()
                {
                    Id = 6,
                    CityId = 3,
                    Name = "Chanse Lisee",
                    Description = "Famous street"
                });

            base.OnModelCreating(modelBuilder);
        }
        //way of conneccting DB 
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connectionstring");
            base.OnConfiguring(optionsBuilder);
        }*/


    }
}
