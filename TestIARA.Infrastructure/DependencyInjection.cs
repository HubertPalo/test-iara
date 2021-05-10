using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestIARA.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using TestIARA.Application.Common.Interfaces;
using TestIARA.Infrastructure.Services;

namespace TestIARA.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICEPService, CEPService>();

            return services;
        }
    }
}
