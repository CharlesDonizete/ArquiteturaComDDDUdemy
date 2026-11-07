using Api.CrossCutting.Mappings;
using AutoMapper;
using System;

namespace Api.Service.Test
{
    public abstract class BaseTesteService
    {
        public IMapper Mapper { get; set; }
        protected BaseTesteService()
        {
            
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DtoToModelProfile());
                    cfg.AddProfile(new EntityToDtoProfile());
                    cfg.AddProfile(new ModelToEntityProfile());
                });
                return config.CreateMapper();
            }

            public void Dispose()
            {
                // Implement any cleanup logic here if needed
            }
        }
    }
}
