
using Microsoft.EntityFrameworkCore;
using RunGroops.Application.Helpers;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.Interfaces;
using RunGroops.Infrastructure.Context;
using RunGroops.Infrastructure.Repositories;
using RunGroops.Infrastructure.Repository;
using System.Configuration;

namespace RunGroopsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IClubRepository, ClubRepository>();
            builder.Services.AddScoped<IClubMapper, ClubMapper>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresRunGroops")));

            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining<GetAllClubsQuery>();
            });
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}