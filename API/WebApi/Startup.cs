using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Config;
using WebAPI.Context;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurando o acesso ao bd
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IRepository<ProductModel>, ProductsRepository>();
            services.AddScoped<IResponse<string>, Response>();
            services.AddScoped<IResponse<ProductModel>, ResponseObj>();
            services.AddScoped<IResponseList<ProductModel>, ResponseList >();


            services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddCors();
            services.AddControllers();
            services.AddSwaggerConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

            //app.UseAuthentication();            
            app.UseMvc();

            //usar swagger
            app.UseSwaggerConfiguration();
        }
    }
}