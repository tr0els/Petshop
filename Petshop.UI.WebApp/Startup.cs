using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Petshop.Core.ApplicationServices;
using Petshop.Core.ApplicationServices.Impl;
using Petshop.Core.DomainServices;
using Petshop.Infrastructure.DBInit;
using Petshop.Infrastructure.SQLite.Data;
using Petshop.Infrastructure.SQLite.Data.Repositories;

namespace Petshop.UI.WebApp
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
            // Generate keys for signing JWT tokens.
            // Using random byte array instead of a static key
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "TodoApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "TodoApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            // SQLite database context
            services.AddDbContext<PetshopSQLiteContext>(opt =>
            {
                opt.UseSqlite("Data Source = Petshop.db");
            });

            // Register repositories for dependency injection using service collection
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IUserService, UserService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Setup for Developer mode environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Setup scope and context using the Sqlite database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetshopSQLiteContext>(); // get db context
                    ctx.Database.EnsureDeleted(); // delete any existing db in case context has changed
                    ctx.Database.EnsureCreated(); // create db context if it does not already exist
                    var petRepository = scope.ServiceProvider.GetService<IPetRepository>();
                    var ownerRepository = scope.ServiceProvider.GetService<IOwnerRepository>();
                    var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                    new DBInit(petRepository, ownerRepository, userRepository).InitData();
                }
            }

            // Setup for Production environment
            if (env.IsProduction())
            {
                app.UseExceptionHandler("/Error");

                // Use Sqlite database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetshopSQLiteContext>();
                    //ctx.Database.EnsureDeleted(); // delete any existing db in case context has changed (do not use with mssql)
                    ctx.Database.EnsureCreated(); // create db context if it does not already exist
                    var petRepository = scope.ServiceProvider.GetService<IPetRepository>();
                    var ownerRepository = scope.ServiceProvider.GetService<IOwnerRepository>();
                    var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                    new DBInit(petRepository, ownerRepository, userRepository).InitData(); // only inits when db is empty
                }
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
