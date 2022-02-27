using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using Application;

namespace Mc2.CrudTest.Presentation.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplication();

            services.AddControllersWithViews();

            //services.AddControllersWithViews()
            //    .AddJsonOptions(options =>
            //    {
            //        //options.JsonSerializerOptions.Converters.Add(new NullableStructSerializerFactory());
            //        //options.JsonSerializerOptions.Converters.Add(new PersonConverterWithTypeDiscriminator());
            //        //options.JsonSerializerOptions.IgnoreNullValues = true;
            //        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //        //options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //        //options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            //    });

            services.AddRazorPages();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext(connectionString);

            

            //services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
