using AutoMapper;
using Mapping.MappingProfiles;

namespace Mapping.Configuration
{
    public class AutoMapperConfiguration
    {
        public IMapper Map()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<UserProfile>();
            });

            return config.CreateMapper();
        }
    }
}
