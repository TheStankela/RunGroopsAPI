using Microsoft.EntityFrameworkCore;
using RunGroops.Application.Helpers;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.Interfaces;
using RunGroops.Infrastructure.Context;
using RunGroops.Infrastructure.Repositories;
using RunGroops.Infrastructure.Repository;
using FluentValidation;
using RunGroops.Application.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using RunGroops.Domain.EFModels;
using RunGroops.Application.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RunGroopsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //IoC
            builder.Services.AddScoped<IClubRepository, ClubRepository>();
            builder.Services.AddScoped<IRaceRepository, RaceRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();

            builder.Services.AddScoped<IClubMapper, ClubMapper>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            //Fluent Validation
          //  builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            //builder.Services.AddValidatorsFromAssemblyContaining<ClubRequestValidator>();

            //Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("RunGroopsConnection")));

            //Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {

                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,
                    };
                })
                .AddCookie()
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                });

            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, option =>
            {
                option.Cookie.SameSite = SameSiteMode.None; // change cookie name
            });

            builder.Services.ConfigureApplicationCookie(o =>
            {
                o.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }

                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            //MediatR
            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining<GetAllClubsQuery>();
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(options =>
                {
                    options.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            //SwaggerUI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}