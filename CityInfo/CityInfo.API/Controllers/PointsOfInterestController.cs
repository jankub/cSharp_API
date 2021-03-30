using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {

            if (!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var pointsOfInterest = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));
        }


        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {

            if (!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

        }


        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError(
                    "Descroption",
                    "The provided description should be  differetnt from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            _cityInfoRepository.Save();

            var createdPointOfInterst = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterst", new { cityId, id = createdPointOfInterst.Id }, createdPointOfInterst);
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError(
                    "Descroption",
                    "The provided description should be  differetnt from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterstEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterstEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterstEntity);
            _cityInfoRepository.UpdatePointOfInterestForCity(cityId, pointOfInterstEntity);
            _cityInfoRepository.Save();

            return NoContent();

        }


        [HttpPatch("{id}")]
        public IActionResult PatchPointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
           
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterstEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterstEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterstEntity);
     
            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The provided description should be different than the name."
                    );
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterstEntity);
            _cityInfoRepository.UpdatePointOfInterestForCity(cityId, pointOfInterstEntity);
            _cityInfoRepository.Save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterstEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterstEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.RemovePointOfInterest(pointOfInterstEntity);
            _cityInfoRepository.Save();
                
            _mailService.Send("Point of interest deleted",
                $"Point of interest {pointOfInterstEntity.Name} with id {pointOfInterstEntity.Id} was removed.");

            return NoContent();
        }

    }
}
