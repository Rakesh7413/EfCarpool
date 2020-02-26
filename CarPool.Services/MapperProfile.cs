using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarPool.Models;
using Carpool.Data.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarPool.Service
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Models.User, >();
            CreateMap<Models.Car, Data.Models.Car>().ForMember(destination => destination.CarType, opts => opts.MapFrom(source => (short)source.CarType));
            CreateMap<Models.Booking, Data.Models.Booking>().ForMember(destination => destination.Status, opts => opts.MapFrom(source => (short)source.Status));
            CreateMap<Models.Ride, Data.Models.Ride>().ForMember(destination => destination.ViaPlaces,
               opts => opts.MapFrom(source => JsonConvert.SerializeObject(source.ViaPlaces).ToString()));
            CreateMap<Data.Models.User, Models.User>();
            CreateMap<Data.Models.Ride, Models.Ride>().ForMember(destination => destination.ViaPlaces, opts => opts.MapFrom(source => JsonConvert.DeserializeObject<List<string>>(source.ViaPlaces)));
            CreateMap<Data.Models.Booking, Models.Booking>().ForMember(destination => destination.Status, opts => opts.MapFrom(source => Enum.ToObject(typeof(BookingStatus), source.Status)));
            CreateMap<Data.Models.Car, Models.Car>().ForMember(destination => destination.CarType, opts => opts.MapFrom(source => Enum.ToObject(typeof(CarType), source.CarType)));
        }
    }
}
