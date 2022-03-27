using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using QuestingEngine.API.Filters;
using QuestingEngine.API.Mappers;
using QuestingEngine.Repository;
using QuestingEngine.Service;
using QuestingEngine.Service.Commands;
using System.Reflection;

namespace QuestingEngine.API
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
            var mongoSection = Configuration.GetSection("Mongo");
            var connectionString = mongoSection.GetValue<string>("ConnectionString");
            var databaseName = mongoSection.GetValue<string>("DatabaseName");

            services.AddScoped<IMongoClient>(_ => new MongoClient(connectionString))
                .AddScoped<IMongoDatabase>(sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();
                    return client.GetDatabase(databaseName);
                });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .AddMediatR(typeof(UpdateProgressCommand).GetTypeInfo().Assembly)
                .AddScoped<IMilestoneRepository, MilestoneRepository>()
                .AddScoped<IQuestRepository, QuestRepository>()
                .AddScoped<IPlayerRepository, PlayerRepository>()
                .AddScoped<ISeedDataService, SeedDataService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuestingEngine.API", Version = "v1" });
            });

            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuestingEngine.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
