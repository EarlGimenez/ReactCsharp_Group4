using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using ASI.Basecode.WebApp.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace ASI.Basecode.WebApp
{
    // Other services configuration
    internal partial class StartupConfigurer
    {
        /// 
        /// Configures the other services.
        /// 
        private void ConfigureOtherServices()
        {
            // Framework
            this._services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            this._services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Common
            this._services.AddScoped<TokenProvider>();
            this._services.TryAddSingleton<TokenProviderOptionsFactory>();
            this._services.TryAddSingleton<TokenValidationParametersFactory>();
            this._services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            this._services.TryAddSingleton<TokenValidationParametersFactory>();
            this._services.AddScoped<IUserService, UserService>();
            this._services.AddScoped<IRoomService, RoomService>();
            this._services.AddScoped<IBookingService, BookingService>();

            // Repositories
            this._services.AddScoped<IUserRepository, UserRepository>();
            this._services.AddScoped<IRoomRepository, RoomRepository>();
            this._services.AddScoped<IBookingRepository, BookingRepository>();

            // Manager Class
            this._services.AddScoped<SignInManager>();

            this._services.AddHttpClient();

            // CORS Configuration - Permissive for development
            this._services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder => builder
                        .SetIsOriginAllowed(origin => 
                        {
                            // Allow any localhost or 127.0.0.1 origin for development
                            if (string.IsNullOrEmpty(origin)) return false;
                            var uri = new Uri(origin);
                            return uri.Host == "localhost" || uri.Host == "127.0.0.1";
                        })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
            this._services.AddControllers();
        }
    }
}
