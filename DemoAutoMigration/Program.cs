using AutoMapper;
using DemoAutoMigration.IRepositories;
using DemoAutoMigration.IRepository;
using DemoAutoMigration.IService;
using DemoAutoMigration.Models;
using DemoAutoMigration.Repository;
using DemoAutoMigration.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DemoAutoMigration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var allowFE = "_AllowFrontEndClient";

            //add Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: allowFE,
                                  policy =>
                                  {
                                      policy.WithOrigins(builder.Configuration.GetValue<string>("FE_Port"))
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //add auto mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<JobContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("JobConstr")));

            //add injection
            configurationInterfce(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(allowFE);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void configurationInterfce(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IJobService, JobService>();
            builder.Services.AddTransient<IJobRepository, JobRepository>();

            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
        }
    }
}