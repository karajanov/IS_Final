using AutoMapper;
using System.Reflection;

namespace BankApplicationTests.Internal
{
    public static class AutoMapperModule
    {
        private static MapperConfiguration configuration;
        private static IMapper mapper;

        public static IMapper CreateMapper()
        {
            if (mapper == null)
            {
                var mc = CreateMapperConfiguration();
                mapper = new Mapper(mc);
            }

            return mapper;
        }

        public static MapperConfiguration CreateMapperConfiguration()
        {
            if (configuration == null)
            {
                configuration = new MapperConfiguration(config =>
                {
                    config.AddMaps(Assembly.Load("BankApplication.Data"));
                });
            }

            return configuration;
        }
    }
}
