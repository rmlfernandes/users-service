using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UsersService.Application;
using UsersService.Application.Repositories;
using UsersService.Repository.InMemory;
using UsersService.TestData;

namespace UsersService.Grpc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // To avoid performance penalties
            // while executing the Comparison
            services.AddLogging(logging => logging.ClearProviders());

            services.AddGrpc(options => options.MaxReceiveMessageSize = null);

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
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Services.UsersService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

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
