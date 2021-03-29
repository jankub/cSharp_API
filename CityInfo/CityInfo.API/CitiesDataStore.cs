using System.Collections.Generic;
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
                    PointsOfInterest = new List<PointOfInterestDto> ()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "Park on Manhattan"
                        },

                        new PointOfInterestDto()
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
                    PointsOfInterest = new List<PointOfInterestDto> ()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Cathedral",
                            Description = "Unfinished church"
                        },

                        new PointOfInterestDto()
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
                    PointsOfInterest = new List<PointOfInterestDto> ()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Eifell Tower",
                            Description = "Symbol of Paris"
                        },

                        new PointOfInterestDto()
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
