using System;
using Xunit;
using FluentAssertions;
using Moq;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API;
using System.Collections.Generic;
using CityInfo.API.Models;
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Controllers;

namespace CityInfo.Tests
{
    public class CityControllerShould : ControllerBase
    {
        [Fact]
        public void ReturnHttpCode200AndCitiesInBodyWhenGetCitiesCalled()
        {
            //Arrange
            var configMapper = new MapperConfiguration(cfg => {          
                cfg.CreateMap<City, CityWithoutPointsOfInterestDto> ();
            });
            IMapper mapper = configMapper.CreateMapper();
            var mockRepo = new Mock<ICityInfoRepository>();
            
            mockRepo.Setup(repo => repo.GetCities()).Returns(GetCities());
            var controlerUT = new CitiesController(mockRepo.Object, mapper);


            //Act
            var result = controlerUT.GetCities();

            OkObjectResult resultOk = (OkObjectResult)result;

            //Assert
            Assert.Equal(200, resultOk.StatusCode);
            Assert.IsType<List<CityWithoutPointsOfInterestDto>>(resultOk.Value);
            Assert.Equal(3, ((List<CityWithoutPointsOfInterestDto>) resultOk.Value).Count);

        }

        
        private IEnumerable<City> GetCities()
        {
            var citiesWithoutPointsOfInterst = new List<City>();

            foreach (var city in CitiesDataStore.Current.Cities)
            {
                citiesWithoutPointsOfInterst.Add(
                    new City()
                    {
                        Id = city.Id,
                        Name = city.Name,
                        Description = city.Description
                    });
            }

            return citiesWithoutPointsOfInterst;
        }

    }
}
