using Microsoft.Extensions.DependencyInjection;
using Fyo.Interfaces;

using Fyo.Services;

namespace Fyo.IoC {
    public static class ServicesModule {
        public static void AddServices(IServiceCollection services){
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<ISoftwareService, SoftwareService>();
            services.AddScoped<ISoftwareVersionService, SoftwareVersionService>();
            services.AddScoped<IDeviceSoftwareVersionService, DeviceSoftwareVersionService>();
            services.AddScoped<ICodeService, CodeService>();
        }
    }
}