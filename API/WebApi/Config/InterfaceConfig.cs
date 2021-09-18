using Microsoft.Extensions.DependencyInjection;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Config
{
    public static class InterfaceConfig
    {
        public static void InterfaceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IRepository<ProductModel>, ProductsRepository>();
            services.AddScoped<IResponse<string>, Response>();
            services.AddScoped<IResponse<ProductModel>, ResponseObj>();
            services.AddScoped<IResponseList<ProductModel>, ResponseList>();
        }
    }
}
