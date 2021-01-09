using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UsersService.Application;
using UsersService.Application.Repositories;
using UsersService.Repository.InMemory;
using UsersService.TestData;

namespace UsersService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // To avoid performance penalties
            // while executing the Comparison
            services.AddLogging(logging => logging.ClearProviders());

            services
                .AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "UsersService.Api",
                    Version = "v1"
                }));

            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<IUsersService, Application.UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersService.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            lifetime.ApplicationStarted.Register(() => ApplicationStartedAction(app.ApplicationServices));
        }

        private static void ApplicationStartedAction(IServiceProvider serviceProvider)
        {
            var usersService = serviceProvider.GetRequiredService<IUsersService>();

            usersService.InsertUserAsync(UsersData.GetUser());
            usersService.InsertUsersAsync(UsersData.GetUsers());
        }
    }
}
