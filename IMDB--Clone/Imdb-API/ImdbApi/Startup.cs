using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImdbApi.Repository;
using ImdbApi.Repository.Interfaces;
using ImdbApi.Services;
using ImdbApi.Services.Interfaces;
using ImdbApi.Migration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using ImdbApi.Filters;
using ImdbApi.Common;

namespace ImdbApi
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
            services.AddSingleton<DatabaseInitializer>();
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddControllers(options =>
            {
                options.Filters.Add<ResponseWrappingResultFilter>();
            });
            services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ImdbApi", Version = "v1" });
                        });
            services.AddAutoMapper(_ => { }, typeof(Startup).Assembly);
            services.AddCors(p => p.AddPolicy("corspolicy", build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImdbApi v1"));
            }

            // Redirects HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            //Enable the routing 
            app.UseRouting();

            // Controls cross-origin request access for browser-based clients.
            app.UseCors("corspolicy");

            // “Who are you?” - proving identity.
            // 401 Unauthorized - actually means unauthenticated (bad or missing token)
            app.UseAuthentication();

            // “What are you allowed to do?” - checking permissions.
            // 403 Forbidden - authenticated but not authorized (valid token, wrong permissions)
            app.UseAuthorization();

            // Custom Middleware
            app.UseRequestLoggingMiddleWare();

            // Does the mapping of url to the resource
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
