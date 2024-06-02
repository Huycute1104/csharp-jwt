using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.UnitOfwork;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Service;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<FStoreDBContext>(options => options.UseSqlServer(getConnection()));
            return services;
        }

   /*     public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();
            return services;
        }*/

        public static IServiceCollection addUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfwork, UnitOfwork>();
            return services;
        }

        public static string getConnection()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var str = config["ConnectionStrings:MyDB"];
            return str;
        }
    }

}
