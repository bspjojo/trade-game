using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Server.Hubs;
using Game.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Game.Server
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
            services.AddSingleton<IGameDataService, InMemoryGameDataService>();
            services.AddSingleton<IGameFlowService, GameFlowService>();
            services.AddSingleton<IGameScoreService, GameScoreService>();
            services.AddSingleton<IGameHubService, GameHubService>();

            services.AddCors(o =>
                o.AddPolicy("All",
                    b => b
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                    )
                );

            services.AddMvc();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("All");

            app.UseSignalR(routes =>
            {
                routes.MapHub<GameHub>("/hubs/game");
            });

            app.UseMvc();
        }
    }
}
