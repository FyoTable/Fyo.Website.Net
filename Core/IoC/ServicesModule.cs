using Microsoft.Extensions.DependencyInjection;
using Fyo.Interfaces;

using Fyo.Services;

namespace Fyo.IoC {
    public static class ServicesModule {
        public static void AddServices(IServiceCollection services){
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceService, DeviceService>();
        }
    }
}