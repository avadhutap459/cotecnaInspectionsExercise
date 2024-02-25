using Inspection.DbLayer;
using Inspection.ServiceLayer.EF.Interface;
using Inspection.ServiceLayer.EF.Service;
using Inspection.ServiceLayer.Interface;
using Inspection.ServiceLayer.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inspection.ServiceLayer.ServiceExtension
{
  public static class ServiceExtension
  {
    public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
    {
      string Path = Directory.GetCurrentDirectory();
      var config = new ConfigurationBuilder().SetBasePath(Path).AddJsonFile("appsettings.json").Build();

      string env = config.GetSection("Env").Value;

      IConfigurationRoot _configuration = new ConfigurationBuilder()
          .SetBasePath(Path)
          .AddJsonFile($"appsettings.{env}.json")
          .Build();
      var connectionString = _configuration.GetConnectionString("DefaultConnection");
      var connectionString_ = configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<InspectionDbContext>(options =>
      {
        options.UseSqlServer(connectionString);
      });
      services.AddScoped<IUnitOfWork, UnitofWorkRepo>();
      services.AddScoped<IInspection, ClsInspectionSVc>();

      return services;
    }
  }
}
