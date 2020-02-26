using AutoMapper;
using CarPool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public static class MapperHelper
    {
        static IMapper Mapper;
        public static T Map<T>(this object obj)
        {
            return Mapper.Map<T>(obj);
        }

        public static IEnumerable<TDest> MapCollection<TSrc, TDest>(this IEnumerable<TSrc> srcs)
        {
            return Mapper.Map<IEnumerable<TSrc>, IEnumerable<TDest>>(srcs);
        }

        public static void InitialiseMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperProfile>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
