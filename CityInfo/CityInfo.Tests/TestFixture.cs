using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.UnitTests
{
    public class TestFixture
    {
        private IMapper _mapper;
        public IMapper Mapper
        { 
            get { return _mapper;}
        }


        public TestFixture()
        {
            var configMapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CityDto, City>();
                cfg.CreateMap<City, CityDto>();
                cfg.CreateMap<City, CityWithoutPointsOfInterestDto>();
                cfg.CreateMap<CityWithoutPointsOfInterestDto, City>();
                cfg.CreateMap<PointOfInterest, PointOfInterestDto>();
                cfg.CreateMap<PointOfInterestDto, PointOfInterest>();
            });
            _mapper = configMapper.CreateMapper();
        }
    }
}
