using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            //init dummy data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "Big city in USA",
                    PointsOfInterest = new List<PointsOfInterestDto> ()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "Park on Manhattan"
                        },

                        new PointsOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "Very tall building"
                        }
                    }
                    
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "City in Belgium",
                    PointsOfInterest = new List<PointsOfInterestDto> ()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 3,
                            Name = "Cathedral",
                            Description = "Unfinished church"
                        },

                        new PointsOfInterestDto()
                        {
                            Id = 4,
                            Name = "Main square",
                            Description = "Old city center"
                        }
                    }
                },

                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "With Eifell Tower",
                    PointsOfInterest = new List<PointsOfInterestDto> ()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 5,
                            Name = "Eifell Tower",
                            Description = "Symbol of Paris"
                        },

                        new PointsOfInterestDto()
                        {
                            Id = 6,
                            Name = "Chanse Lisee",
                            Description = "Famous street"
                        }
                    }
                }

            };
        }
    }
}
