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
            CreateMap<Models.User, Users>();
            CreateMap<Models.Car, Cars>().ForMember(destination => destination.CarType, opts => opts.MapFrom(source => (short)source.CarType));
            CreateMap<Models.Booking, Bookings>().ForMember(destination => destination.Status, opts => opts.MapFrom(source => (short)source.Status));
            CreateMap<Models.Ride, Rides>().ForMember(destination => destination.ViaPlaces,
               opts => opts.MapFrom(source => JsonConvert.SerializeObject(source.ViaPlaces).ToString()));
            CreateMap<Users, Models.User>();
            CreateMap<Rides, Models.Ride>().ForMember(destination => destination.ViaPlaces, opts => opts.MapFrom(source => JsonConvert.DeserializeObject<List<string>>(source.ViaPlaces)));
            CreateMap<Bookings, Models.Booking>().ForMember(destination => destination.Status, opts => opts.MapFrom(source => Enum.ToObject(typeof(BookingStatus), source.Status)));
            CreateMap<Cars, Models.Car>().ForMember(destination => destination.CarType, opts => opts.MapFrom(source => Enum.ToObject(typeof(CarType), source.CarType)));
        }
    }
}
