using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PluralSightBook.Core.Identity;
using PluralSightBook.Core.Interfaces;
using PluralSightBook.Core.Services;
using PluralSightBook.Infrastructure.Data;
using PluralSightBook.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PluralSightBook.API
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
            ConfigureDbConnectionSettings(services);
            ConfigureIdentitySettings(services);

            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IFriendService, FriendsService>();
            services.AddTransient<IProfileRepository, EfProfileRepository>();
            services.AddTransient<IFriendRepository, EfFriendRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Employee API", Version = "V1" });
            });
        }

        private void ConfigureIdentitySettings(IServiceCollection services)
        {
            services.AddIdentity<PluralSightBookIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<PluralSightBookDbContext>()
                .AddDefaultTokenProviders();
        }

        private void ConfigureDbConnectionSettings(IServiceCollection services)
        {
            services.AddDbContext<PluralSightBookDbContext>(options =>
             options.UseSqlServer(
               Configuration.GetConnectionString("DevelopmentConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}