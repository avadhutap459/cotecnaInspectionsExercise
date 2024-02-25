using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Inspection.DbLayer
{
  public class InspectionsContextFactory : IDesignTimeDbContextFactory<InspectionDbContext>
  {
    public static string appDirectory = System.Environment.CurrentDirectory;
    public static string env = string.Empty;

    public InspectionDbContext CreateDbContext(string[] args)
    {
      string Path = Directory.GetCurrentDirectory();
      Path = Path.Substring(0, Path.LastIndexOf(("\\"))) + "\\Inspection.APILayer\\";
      var config = new ConfigurationBuilder().SetBasePath(Path).AddJsonFile("appsettings.json").Build();

      env = config.GetSection("Env").Value;

      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Path)
          .AddJsonFile($"appsettings.{env}.json")
          .Build();

      var dbContextOptionsBuilder = new DbContextOptionsBuilder<InspectionDbContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");
      dbContextOptionsBuilder
          .UseSqlServer(connectionString);

      return new InspectionDbContext(dbContextOptionsBuilder.Options);
    }
  }
}
