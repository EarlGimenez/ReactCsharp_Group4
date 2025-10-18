using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ASI.Basecode.WebApp
{
    // AutoMapper configuration
    internal partial class StartupConfigurer
    {
        /// 
        /// Configure auto mapper
        /// 
        private void ConfigureAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProfileConfiguration());
            });

            this._services.AddSingleton<IMapper>(sp => mapperConfiguration.CreateMapper());
        }

        private class AutoMapperProfileConfiguration : Profile
        {
            public AutoMapperProfileConfiguration()
            {
                CreateMap<UserViewModel, User>();
            }
        }
    }
}
