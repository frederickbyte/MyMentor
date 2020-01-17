using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyMentor.API.Authorization;
using MyMentor.API.Utilities;
using MyMentor.API.ViewModels;
using MyMentor.DataAccessLayer;
using MyMentor.DataAccessLayer.Auth;
using MyMentor.DataAccessLayer.Auth.Interfaces;
using MyMentor.DataAccessLayer.Models;
using MyMentor.DataAccessLayer.Repository;
using MyMentor.DataAccessLayer.Repository.Database;
using MyMentor.DataAccessLayer.Repository.Interfaces;

namespace MyMentor.API
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
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], 
                    b => b.MigrationsAssembly("MyMentor.API")));

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Auto Mapper 
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            var applicationUrl = Configuration["ApplicationUrl"].TrimEnd('/');

            //// Adds IdentityServer.
            //services.AddIdentityServer()
            //    // The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
            //    // This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
            //    // See http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.
            //    .AddDeveloperSigningCredential()
            //    .AddInMemoryPersistedGrants()
            //    // To configure IdentityServer to use EntityFramework (EF) as the storage mechanism for configuration data (rather than using the in-memory implementations),
            //    // see https://identityserver4.readthedocs.io/en/release/quickstarts/8_entity_framework.html
            //    .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            //    .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
            //    .AddInMemoryClients(IdentityServerConfig.GetClients())
            //    .AddAspNetIdentity<ApplicationUser>()
            //    .AddProfileService<ProfileService>();

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = applicationUrl;
            //        options.SupportedTokens = SupportedTokens.Jwt;
            //        options.RequireHttpsMetadata = false; // Note: Set to true in production
            //        options.ApiName = IdentityServerConfig.ApiName;
            //    });

            // Add cors
            services.AddCors();

            // Repositories
            services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
            services.AddScoped<IAccountManager, AccountManager>();

            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            HelperMethods.ConfigureLogger(loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Configure Cors
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            // app.UseHttpsRedirection();
            
            // app.UseIdentityServer();

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.DocumentTitle = "Swagger UI - Construct Gaming";
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{IdentityServerConfig.ApiFriendlyName} V1");
            //    c.OAuthClientId(IdentityServerConfig.SwaggerClientID);
            //    c.OAuthClientSecret("no_password"); //Leaving it blank doesn't work
            //});

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
