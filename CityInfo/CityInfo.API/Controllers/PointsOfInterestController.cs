using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);

        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            //fint point of interst
            var pointOfInterst = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterst == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterst);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, 
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            //to be improved
            var maxPointOfInterstId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterset = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterstId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterset);
            return CreatedAtRoute("GetPointOfInterst", new { cityId, id = finalPointOfInterset.Id}, finalPointOfInterset);
        }

    }
}
