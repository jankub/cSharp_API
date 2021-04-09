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
using Xunit.Abstractions;
using CityInfo.UnitTests;

namespace CityInfo.Tests
{
    public class CityControllerShould : IClassFixture<TestFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestFixture _testFixture;

        public CityControllerShould(TestFixture fixture,
            ITestOutputHelper outHelper)
        {
            _testFixture = fixture;
            _output = outHelper;
        }

        [Fact(Skip = "skip testing")]
        [Trait("Method", "GetCities")]
        public void ReturnHttpCode200AndCitiesInBodyWhenGetCitiesCalled()
        {
            //Arrange       
            var mockRepo = new Mock<ICityInfoRepository>();
            
            mockRepo.Setup(repo => repo.GetCities()).Returns(GetCities());
            var controlerUT = new CitiesController(mockRepo.Object, _testFixture.Mapper);
            
            //Act
            var result = controlerUT.GetCities();

            OkObjectResult resultOk = (OkObjectResult)result;

            //Assert
            Assert.Equal(200, resultOk.StatusCode);
            Assert.IsType<List<CityWithoutPointsOfInterestDto>>(resultOk.Value);
            Assert.Equal(3, ((List<CityWithoutPointsOfInterestDto>) resultOk.Value).Count);

        }

        
        [Fact]
        [Trait("Method", "GetCity")]
        public void ReturnHttpNoFoundWhenNoCityFound()
        {
            //Arrange
            _output.WriteLine(_testFixture.Mapper.GetHashCode().ToString());
            var mockRepo = new Mock<ICityInfoRepository>();
            City city = null;
            mockRepo.Setup(repo => repo.GetCity(It.IsAny<int>(), It.IsAny<bool>())).Returns(city);

            var controlerUT = new CitiesController(mockRepo.Object, _testFixture.Mapper);

            //Act
            var result = controlerUT.GetCity(4, true);
            
            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("Method", "GetCity")]
        public void ReturnOkResultAndOneCityWithPoI()
        {
            //Arrange
            _output.WriteLine(_testFixture.Mapper.GetHashCode().ToString());        
            var mockRepo = new Mock<ICityInfoRepository>();
            City city = _testFixture.Mapper.Map<City>(CitiesDataStore.Current.Cities[0]);
            mockRepo.Setup(repo => repo.GetCity(It.IsAny<int>(), true)).Returns(city);

            var controlerUT = new CitiesController(mockRepo.Object, _testFixture.Mapper);

            //Act
            var result = controlerUT.GetCity(1, true);
            OkObjectResult resultOk = (OkObjectResult)result;
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(((CityDto) resultOk.Value).PointsOfInterest);

        }

        [Fact]
        [Trait("Method", "GetCity")]
        public void ReturnOkResultAndOneCityWithoutPoI()
        {
            //Arrange
            _output.WriteLine(_testFixture.Mapper.GetHashCode().ToString());
            var mockRepo = new Mock<ICityInfoRepository>();
            City city = _testFixture.Mapper.Map<City>(CitiesDataStore.Current.Cities[0]);
            mockRepo.Setup(repo => repo.GetCity(It.IsAny<int>(), false)).Returns(city);

            var controlerUT = new CitiesController(mockRepo.Object, _testFixture.Mapper);

            //Act
            var result = controlerUT.GetCity(1, false);
            OkObjectResult resultOk = (OkObjectResult)result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<CityWithoutPointsOfInterestDto>(resultOk.Value);

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


        private City GetCity(int id, bool includePointsOfInterest)
        {
            var configMapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<City, CityDto>();
            });
            IMapper mapper = configMapper.CreateMapper();

            var cityFromStore = CitiesDataStore.Current.Cities.Find(c => c.Id == id);

            if (cityFromStore == null)
            {
                return null;
            }

            if (includePointsOfInterest)
            {
                return mapper.Map<City>(cityFromStore);
            }

            cityFromStore.PointsOfInterest = null;
            return mapper.Map<City>(cityFromStore);
        }

    }
}
