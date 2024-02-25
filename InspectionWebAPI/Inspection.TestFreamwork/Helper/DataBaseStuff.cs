using Dapper;
using Inspection.ServiceLayer.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspection.TestFreamwork.Helper
{
  public class DataBaseStuff
  {
    public static string connectionString { get; set; }
    static DataBaseStuff()
    {
      string Path = Directory.GetCurrentDirectory();
      Path = Path.Substring(0, Path.LastIndexOf(("\\")));
      Path = Path.Substring(0, Path.LastIndexOf(("\\")));
      Path = Path.Substring(0, Path.LastIndexOf(("\\")));
      Path = Path.Substring(0, Path.LastIndexOf(("\\"))) + "\\Inspection.APILayer\\";

      var config = new ConfigurationBuilder().SetBasePath(Path).AddJsonFile("appsettings.json").Build();

      string env = config.GetSection("Env").Value;

      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Path)
          .AddJsonFile($"appsettings.{env}.json")
          .Build();

      connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public static IDbConnection connection
    {
      get
      {
        return new SqlConnection(connectionString);
      }
    }


    public static List<ClsInspectionsBM> ReturnAllInspectionsBaseOnInspectorName(string InspectorName)
    {
      List<ClsInspectionsBM> lstInspection = new List<ClsInspectionsBM>();

      using (IDbConnection cn = connection)
      {
        cn.Open();

        lstInspection = cn.Query<ClsInspectionsBM>("select i.InspectionID,i.InspectionDate,i.Customer,i.Address,i.Observations,i.Status,j.InspectorId,j.InspectorName " +
          "from [dbo].[TxnInspection] i inner join  [dbo].[mstInspector] j on i.InspectorId = j.InspectorId where j.InspectorName = @InspectorName",
          new { InspectorName = InspectorName }).ToList();

      }

      return lstInspection;
    }



  }
}
