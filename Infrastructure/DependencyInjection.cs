//using Application.Common.Interfaces;
//using Infrastructure.Files;
//using Infrastructure.Identity;
//using Infrastructure.Persistence;
//using Infrastructure.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Infrastructure
//{

//    public static class StartupSetup
//    {
//        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
//            services.AddDbContext<Context>(options =>
//                options.UseSqlServer(connectionString)); // will be created in web project root
//    }

//    public static class DependencyInjection
//    {
//        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
//        {
//            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
//            {
//                services.AddDbContext<Context>(options =>
//                    options.UseInMemoryDatabase("CleanArchitectureDb"));
//            }
//            else
//            {
//                services.AddDbContext<Context>(options =>
//                    options.UseSqlServer(
//                        configuration.GetConnectionString("DefaultConnection"),
//                        b => b.MigrationsAssembly(typeof(Context).Assembly.FullName)));
//            }

//            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<Context>());

//            services.AddScoped<IDomainEventService, DomainEventService>();

//            services
//                .AddDefaultIdentity<ApplicationUser>()
//                .AddRoles<IdentityRole>()
//                .AddEntityFrameworkStores<ApplicationDbContext>();

//            services.AddIdentityServer()
//                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

//            services.AddTransient<IDateTime, DateTimeService>();
//            services.AddTransient<IIdentityService, IdentityService>();
//            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

//            services.AddAuthentication()
//                .AddIdentityServerJwt();

//            services.AddAuthorization(options =>
//                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

//            return services;
//        }
//    }
//}