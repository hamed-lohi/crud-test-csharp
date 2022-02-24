using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{

    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString)); // will be created in web project root

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<Context>());
        }
    }
}