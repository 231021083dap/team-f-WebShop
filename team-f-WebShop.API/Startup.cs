using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Repositories;
using team_f_WebShop.API.Services;

namespace team_f_WebShop.API
{
    public class Startup
    {
        // for ANGULAR to work
        readonly string CORSRules = "_CORSRules";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            // for ANGULAR to work
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORSRules,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
                


            // added scopes
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();



            // Connection string "Default"
            services.AddDbContext<WebShopProjectContext>(
                a => a.UseSqlServer(Configuration.GetConnectionString("Default")));






            services.AddControllers();
            services.AddScoped<IcategoryRepository, categoryRepository>();

            services.AddDbContext<WebShopProjectContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddScoped<IcategoryService, categoryService>();
               
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "team_f_WebShop.API", Version = "v1" });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "team_f_WebShop.API v1"));
            }

            app.UseHttpsRedirection();

            // for ANGULAR to work
            app.UseCors(CORSRules);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
