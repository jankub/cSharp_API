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
                    Description = "Big city in USA"
                }
            };

            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "City in Belgium"
                }
            };

            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "With Eifell Tower"
                }
            };
        }
    }
}
